using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRM.DTOs.customerDTOs
{
    public class SearchQueryCustomerDTO
    {
        [Display(Name = "Name")]
        public string? Name_Like { get; set; }
        [Display(Name = "Apellido")]

       public string? LastName_Like { get; set; }
        [Display(Name = "Pagina")]

        public int Skip { get; set; }
        [Display(Name = "CantReg x Pagina")]

        public int Take { get; set; }
        ///<summary>  
        ///1 = no se cuent los resultado de la busqueda
        ///2= cuents los resultado de la busqueda
        /// </summary>
        
        public byte SendRowCount { get; set; }
        
    }
}
