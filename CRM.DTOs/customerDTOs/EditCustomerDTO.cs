using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRM.DTOs.customerDTOs
{
    public class EditCustomerDTO
    {
        public EditCustomerDTO(GetIdResultCustomerDTO getResultCustomerDTO)
        {
            Id = getResultCustomerDTO.Id;
            Name = getResultCustomerDTO.Nombre;
            LastName = getResultCustomerDTO.LastName;
            Address = getResultCustomerDTO.Address;
        }

        public EditCustomerDTO()
        {
            Name = string.Empty;
            LastName = string.Empty;

        }
        [Required(ErrorMessage ="El campo Id es obligatorio")]

        public int Id { get; set; }

        [Display(Name ="Nombre")]
        [Required(ErrorMessage ="el campo nombre es obligatorio")]
        [MaxLength(50, ErrorMessage ="el apelllido no puede contener mas 50 ")]

        public string Name { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El coampo apellido es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo apellido no puede tener mas de 50 caracteres")]

        public string LastName { get; set; }

        [Display(Name = "Direccion")]
        [MaxLength(225, ErrorMessage ="El campo direccion no puede tener mas de 255")]

        public string? Address { get; set; }
    }
}
