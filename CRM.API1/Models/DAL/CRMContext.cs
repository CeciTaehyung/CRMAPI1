//Importa el espacio de nombres necesarios para DbContext.
using Microsoft.EntityFrameworkCore;
using CRM.API1.Models.EN;

//Define la clase CRMContext que hereda de DbContext.
namespace CRM.API1.Models.DAL
{
    public class CRMContext : DbContext
    {
        //Constructor que toma DbContextOptions como parámetro para configurar la conexion a la base de dato.

        public CRMContext(DbContextOptions<CRMContext> options) : base(options)
        { 
        }

        //Define un DbSet llamado "Customers" que corresponda una tabla de clientes en la base de datos.
         
        public DbSet<Customer> Customers { get; set; }
    }
}
