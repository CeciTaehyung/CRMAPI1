//Importar los espacios de nombres necesarios.
using CRMAPI1.Models.EN;
using Microsoft.EntityFrameworkCore;
//Define la clase CustomerDal que se utiliza para interactuar
//con los datos de los clientes en la base de dato.

namespace CRM.API1.Models.DAL
{

    public class CostumerDAL
    {
        readonly CRMContext _context;
            
        //constructor que recibe un objeto CRMContext para interactuar con la base de dato.

        public CostumerDAL(CRMContext cRMContext)
        {
            _context = cRMContext;
        }

        //metodo para crea un nuevo cliente en la base de datos.
        
        public async Task<int>Create(Customer costumer)
        {
            try
            {
                _context.Add(costumer);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //metodo para obtener un cliente por su ID.
        
        public async Task<Customer> GetById(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(s =>s.Id == id);
            return customer != null ? customer : new Customer();
        }
        //metodo para editar un cliente en la base de datos.

        public async Task<int> Edit(Customer customer)
        {
            int result = 0;
            var customerUpdate = await GetById(customer.Id);
            if (customerUpdate.Id !=0)
            {
                //actualiza los datos del cliente.
                customerUpdate.Name = customer.Name;
                customerUpdate.LastName = customer.Address;
                result = await _context.SaveChangesAsync();
            }
            return result;
        }
        //Metodo para eliminar un cliente a la base de datos por su ID.
        public async Task<int>Delete(int id)
        {
            int result = 0;
            var customerDelete = await GetById(id);
                if(customerDelete.Id > 0)
                {
                    //elimina el cliente de la base de datos
                    _context.SaveChangesAsync();

                }
            return result;
        }

        //metodo privado para contruir una consulta para buscar clientes con filtro.
        private IQueryable<Customer> Query(Customer customer)
        {
            var query = _context.Customers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(customer.Name)) 
            query = query.Where(s => s.Name.Contains(customer.Name));
            if (!string.IsNullOrWhiteSpace(customer.LastName))
                query = query.Where(s => s.LastName.Contains(customer.LastName));
            return query;
        }
        //metodo parra contar la cantidad de resultador de busquedas con filtro.
        public async Task<int> CountSearcha (Customer customer)
        {
            return await Query(customer).CountAsync();
        }
        //metodo para buscas a los clientes con filtros, paginacion y ordenamiento.
        public  async Task<List<Customer>> Search(Customer customer, int take = 10, int skip = 0)
        {
            take = take == 0 ? 10 : take;
            var query = Query(customer);
            query = query.OrderByDescending(s => s.Id).Skip(skip).Take(take);
            return await query.ToListAsync();
        }

    } 

   
}
