using Microsoft.AspNetCore.Mvc.RazorPages;
using FruitWebApp.Models;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace FruitWebApp.Pages
{
    public class IndexModel : PageModel
    {
        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Add the data model and bind the form data to the page model properties
        // Enumerable since an array is expected as a response
        [BindProperty]
        public IEnumerable<FruitModel> FruitModels { get; set; }

        // Begin GET operation code
        //OnGet()
        public async Task OnGet()
        {
          var httpclient = _httpClientFactory.CreateClient("FruitAPI");

          using HttpResponseMessage httpResponse = await httpclient.GetAsync("");

          if(httpResponse.IsSuccessStatusCode){
            using var content = await httpResponse.Content.ReadAsStreamAsync(); 
            FruitModels = await JsonSerializer.DeserializeAsync<IEnumerable<FruitModel>>(content);
          }
        }
        
        // End GET operation code
    }
}

