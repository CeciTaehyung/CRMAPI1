using CRM.DTOs.customerDTOs;
namespace CRMAppBlazor.Data
    
{
    public class CustomerService
    {
        readonly HttpClient _httpClientCRMAPI1;
        
        //Constructor que recibe una instancia de IHtppClienteFacotry para crear el clientes  de HTTP

        public CustomerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientCRMAPI1 = httpClientFactory.CreateClient("CRMAPI1");
        }

        //metodod para buscar clientes utilizando una solicitud HTTP POST

        public async Task<SearchResultCustomerDTO> Search(SearchQueryCustomerDTO searchQueryCustomerDTO)
        {
            var response = await _httpClientCRMAPI1.PostAsJsonAsync("/customer/search", searchQueryCustomerDTO);
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<SearchResultCustomerDTO>();
                return result ?? new SearchResultCustomerDTO();
            }
            return new SearchResultCustomerDTO(); //devolver un obejto vacio en caso de error o respuesta no exitosa

        }

        //metodo para obtener un cliente por su ID utilizando una solicitud HTTP o GET

        public async Task<GetIdResultCustomerDTO> GetById(int id)
        {
            var response = await _httpClientCRMAPI1.GetAsync("customer/" + id);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();
                return result?? new GetIdResultCustomerDTO();
            }

            return new GetIdResultCustomerDTO(); //devolver un obejto vacio 
        }

        //metodo para crear un cliente utilizando una solicitud POST
        public async Task<int> Create( createCustomerDTO createCustomerDTO)
        {
            int result = 0;
            var response = await _httpClientCRMAPI1.PostAsJsonAsync("/customer", createCustomerDTO);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;

        }
        //metodo para eliminar un cliente por su ID utilizando http delete
        public async Task<int> Delete(int id)
        {
            int result = 0;
            var response = await _httpClientCRMAPI1.DeleteAsync("/customer/" + id);
            if(response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (int.TryParse(responseBody, out result) == false)
                    result = 0;
            }
            return result;
        }
    }
}
