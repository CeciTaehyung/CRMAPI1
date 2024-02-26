using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRM.DTOs.customerDTOs
{
    public class SearchResultCustomerDTO
    {
        public int CountRow { get; set; }

        public List<customerDTO> Data { get; set; }
       
        public class customerDTO
        {
            public int Id { get; set; }

            [Display(Name ="Name")]

            public string Name { get; set; }

            [Display(Name = "Apellido")]

            public string LastName { get; set; }

            [Display(Name ="Direccion")]

            public string? Address { get; set; }
        }
    }
}
