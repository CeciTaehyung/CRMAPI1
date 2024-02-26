using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRM.DTOs.customerDTOs
{
    public class createCustomerDTO
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Nombre no puede tener mas de 50 caracteres.")]

        public string Nombre { get; set; }

        [Display(Name= "Apellido")]
        [Required (ErrorMessage = "El campo Apellido es obligatorio.")]
        [MaxLength (59, ErrorMessage = "El campo apellido no puede contener mas de 50 caracteres.")]

        public string LastName { get; set; }

        [Display(Name = "Direccion")]
        [MaxLength(225, ErrorMessage = "El campo Direccion se puede tener  mas de 250 caracteres")]

        public string? Address { get; set; }
    }
}
