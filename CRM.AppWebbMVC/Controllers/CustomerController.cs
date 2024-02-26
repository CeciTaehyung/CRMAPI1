using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRM.AppWebbMVC.Controllers;
using CRM.DTOs.customerDTOs;

namespace CRM.AppWebbMVC.Controllers
{
    public class CustomerController : Controller
    {

        private readonly HttpClient _httpClienteCRMAPI1;

        //Constructor que recibe una instancia de IHttpClienteFactory para crear el cliente HTP
        
        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClienteCRMAPI1 = httpClientFactory.CreateClient("CRMAP1");
        }

        //Metdo para obtener la lista de cliente

        public async Task<ActionResult> Index(SearchQueryCustomerDTO searchQueryCustomerDTO, int CountRow = 0)
        {
            //Configurar de valores por defecto de los clientes
            if (searchQueryCustomerDTO.SendRowCount == 0)
                searchQueryCustomerDTO.SendRowCount = 2;
            if (searchQueryCustomerDTO.Take == 0)
                searchQueryCustomerDTO.Take = 10;

            var result = new SearchResultCustomerDTO();

            //Realizar una solicitud    POST para buscr clientes en el servicio web
            var response = await _httpClienteCRMAPI1.PostAsJsonAsync("/customer/search", searchQueryCustomerDTO);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<SearchResultCustomerDTO>();

            result = result != null ? result : new SearchResultCustomerDTO();

            //Configurar de los valores para la vista
            if (result.CountRow == 0 && searchQueryCustomerDTO.SendRowCount == 1)
                result.CountRow = CountRow;

            ViewBag.CountRow = result.CountRow;
            searchQueryCustomerDTO.SendRowCount = 0;
            ViewBag.SearchQuery = searchQueryCustomerDTO;

            return View(result);
        }

        //Metodo para mostrar los detalles del cliente
        public async Task<IActionResult> Details(int id)
        {
            var result = new GetIdResultCustomerDTO();

            //realizar una solicitud HTTP GET para obtener los detalles de cliente por ID
            var response = await _httpClienteCRMAPI1.GetAsync("/customer/" + id);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();

            return View(result ?? new GetIdResultCustomerDTO());
        }

        //Metodo para mostrar el formulrio de creacion de un cliente

        public ActionResult Creating()
        {
            return View();
        }

        //Metodo para procesae la creacion de un cliente
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(createCustomerDTO createCustomerDTO)
        {
            try
            {
                var response = await _httpClienteCRMAPI1.PostAsJsonAsync("/customer", createCustomerDTO);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al ingresar";
                return View();

            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        //Metodo para mostrar el formulario de edicion de un cliente

        public async Task<IActionResult> Edit(int id)
        {
            var result = new GetIdResultCustomerDTO();
            var response = await _httpClienteCRMAPI1.GetAsync("/customer/" + id);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();

            return View(new EditCustomerDTO(result ?? new GetIdResultCustomerDTO()));
        }

        //metodo para procesar la edicion
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit( int id, EditCustomerDTO editCustomerDTO)
        {
            try
            {
                //realizar una solicitud HTTP PUT para ed un cliente
                var response = await _httpClienteCRMAPI1.PostAsJsonAsync("/customer", editCustomerDTO);

                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar editar";
                    return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
        //metodo para mostrar la pagina de confirmacion de eliminacion de cliente

        public async Task<IActionResult> Delete(int id)
        {
            var result = new GetIdResultCustomerDTO();
            var response = await _httpClienteCRMAPI1.GetAsync("/customer/" + id);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();

            return View(result ?? new GetIdResultCustomerDTO());
        }

        //metodo para procesar la eliminacion de un cliente
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int id, GetIdResultCustomerDTO getIdResultCustomerDTO)
        {
            try
            {
                //realizar la solicitud HTTP DELTE
                var response = await _httpClienteCRMAPI1.DeleteAsync("/customer/" + id);

                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Error = "erris al intentar eliminar registros";
                return View(getIdResultCustomerDTO);
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(getIdResultCustomerDTO);
            }
        }

    }
}
