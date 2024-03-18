using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FruitWebApp.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Diagnostics;


namespace FruitWebApp.Pages
{
    public class AddModel : PageModel
    {
        // IHttpClientFactory set using dependency injection 
        private readonly IHttpClientFactory _httpClientFactory;

        public AddModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        // Add the data model and bind the form data to the page model properties
        [BindProperty]
        public FruitModel FruitModels { get; set; }

        // Begin POST operation code
        //OnPost()
        public async Task<IActionResult> OnPost()
        {
            //convert the .net objects into json objects
          var jsonContent = new StringContent(JsonSerializer.Serialize(FruitModels),
          Encoding.UTF8,"application/json"
          );

          //create the httpclient
          var httpclient = _httpClientFactory.CreateClient("FruitAPI");

          using HttpResponseMessage httpResponse = await httpclient.PostAsync("",jsonContent);

          if(httpResponse.IsSuccessStatusCode){
            TempData["success"]="Data was successfully posted";
            return RedirectToPage("Index");
          }
          else{
            TempData["failure"]="Operation was not successfull";
            return RedirectToPage("Index");
          }
        }
        
        // End POST operation code
    }
}

