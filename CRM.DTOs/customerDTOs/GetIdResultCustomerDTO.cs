using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CRM.DTOs.customerDTOs
{
    public class GetIdResultCustomerDTO
    {
        public int Id { get; set;}

        [Display(Name = "Nombre")]

        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        
        public string LastName { get; set; }

        [Display(Name = "Direccion")]

        public string? Address { get; set; }

    }
}
