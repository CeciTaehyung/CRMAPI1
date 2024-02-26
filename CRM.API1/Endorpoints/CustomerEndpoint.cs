using CRM.API1.Models.DAL;
using CRM.DTOs.customerDTOs;
using CRMAPI1.Models.EN;

namespace CRM.API1.Endorpoints

{
    public static class CustomerEndpoint
    {
        //metodo para configur los endepoints relacionados con los clientes
        public static void AddCustomerEndopoints(this WebApplication app)
        {
            //Metodo para configurar los endpoints relacionados con los clientes.
            app.MapPost("/customer/search", async (SearchQueryCustomerDTO customerDTO, CostumerDAL CostumerDAL) =>
            {
                //crear un objeto de tipo POST para buscar clientes
                var costumer = new Customer
                {
                    Name = customerDTO.Name_Like != null ? customerDTO.Name_Like : string.Empty,
                    LastName = customerDTO.LastName_Like != null ? customerDTO.LastName_Like : string.Empty
                };

                //Inicializar una lista de clientes y una variable para contar las filas
                var customers = new List<Customer>();
                int countRow = 0;

                //verificar si se debe de enviar la cantidad de filas
                if (customerDTO.SendRowCount == 2)
                {
                    //realizar una busqueda de clientes sin contar las filas
                    customers = await CostumerDAL.Search(costumer, skip: customerDTO.Skip, take: customerDTO.Take);
                    if (customers.Count > 0)
                        countRow = await CostumerDAL.CountSearcha(costumer);
                }
                else
                {
                    //realizar una bsqueda  de clientes sin contar filas
                    customers = await CostumerDAL.Search(costumer, skip: customerDTO.Skip, take: customerDTO.Take);
                }

                //crear un objeto searchresultCustomerDTO para almecenar datos
                var customerResult = new SearchResultCustomerDTO
                {
                    Data = new List<SearchResultCustomerDTO.customerDTO>(),
                    CountRow = countRow,
                };
                //mapear los resultados de obejtos Customer y agregarlos
                customers.ForEach(s => {
                    customerResult.Data.Add(new SearchResultCustomerDTO.customerDTO
                    { 
                     
                        Id = s.Id,
                        Name = s.Name,
                        LastName= s.LastName,
                        Address = s.Address
                    });
                });

                //Devolver los resultados
                return customerResult;
            });

            //configurar un endopoint de tipo Get para obtener un cliente por ID
            app.MapGet("/customer/{id}" , async (int id, CostumerDAL customerDAL) =>
            {
                //Obtener un cliente por ID
                var customer = await customerDAL.GetById(id);

                //crear un objeto GetIdResultDTO para alamcenar el resultador
                var customerResult = new GetIdResultCustomerDTO
                {
                    Id = customer.Id,
                    Nombre =customer.Name,
                    LastName = customer.LastName,
                    Address = customer.Address
                };

                //verificar si se encontro un cliente 
                if (customerResult.Id >0)
                    return Results.Ok(customerResult);
                else
                    return Results.NotFound(customerResult);
            });

            //configurar un endporint de tipo POST
            app.MapPost("/customer", async (createCustomerDTO customerDTO, CostumerDAL customerDAL) =>
            {
                //crear u objeto customer
                var customer = new Customer
                {
            
                    Name = customerDTO.Nombre,
                    LastName = customerDTO.LastName,
                    Address = customerDTO.Address
                };

                //Intentar editar el cliente y devolver
                int result = await customerDAL.Create(customer);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });
            //configurar un endporint de tipo PUT para editar un ciente existente
            app.MapPut("/customer", async (EditCustomerDTO customerDTO, CostumerDAL customerDAL) =>
            {
                //crear un objeto 'customer' a partir de los datos proporcionados
                var customer = new Customer
                {
                    Id = customerDTO.Id,
                    Name = customerDTO.Name,
                    LastName = customerDTO.LastName,
                    Address = customerDTO.Address
                };

                //Intentar editar el cliente y devolver el resultado correspondiente
                int result = await customerDAL.Edit(customer);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });

            //Configuara un endpoint DELETE
            app.MapDelete("/customer/{id}", async (int id, CostumerDAL customerDAL) =>
            {
                //intentar eliminar el cliente y devolver el resultado
                int result = await customerDAL.Delete(id);
                if (result != 0)
                    return Results.Ok(result);
                return Results.StatusCode(500);

            });
        }
    }
}
 
            
