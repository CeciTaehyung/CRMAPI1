using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRM.DTOs.customerDTOs;

namespace CRM.AppWebMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpCRMAPI1;

        //Constructor que recibe una instancia 

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpCRMAPI1 = httpClientFactory.CreateClient("CRMAPI1");

        }

        //Metodo para mostrar la lista de clientes 

        public async Task<AcceptedAtActionResult> Index(SearchQueryCustomerDTO searchQueryCustomerDTO, int CountRow = 0)
        {
            //configuarar los valores
            if (searchQueryCustomerDTO.SendRowCount == 0)
                searchQueryCustomerDTO.SendRowCount = 2;
            if (searchQueryCustomerDTO.Take == 0)
                searchQueryCustomerDTO.Take = 10;

            var result = new SearchQueryCustomerDTO();

            //realizar una solicitud de HTTP
            var response = await _httpCRMAPI1.PostAsync("/customer/search", SearchQueryCustomerDTO);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReasFromJsonAsync<SearchResultCustomerDTO>();

            result = result != null ? result : new SearchQueryCustomerDTO();
        }


    }
}
