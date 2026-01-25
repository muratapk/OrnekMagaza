using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrnekMagaza.Models;
using System.Net.Http;

namespace OrnekMagaza.Controllers
{
    public class ProductApiController : Controller
    {

       
            private readonly HttpClient _http;

            public ProductApiController(HttpClient http)
            {
                _http = http;
                _http.BaseAddress = new Uri("https://localhost:7125/api/");
            }

            public async Task<IActionResult> Index()
            {
                var products = await _http.GetFromJsonAsync<List<Products>>("Product");
                return View(products);
            }

            public IActionResult Create() => View();

            [HttpPost]
            public async Task<IActionResult> Create(Products product)
            {
                var response = await _http.PostAsJsonAsync("Product", product);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                return View(product);
            }

            public IActionResult Edit(int id)
            {
             Products product = new Products();
            //var product = await _http.GetFromJsonAsync<Products>("api/Product/" + id);
            //var product =_http.GetAsync(_http.BaseAddress + "api/Product/" + id).Result;
            HttpResponseMessage respone=_http.GetAsync(_http.BaseAddress + "Product/" + id).Result;
            if(respone.IsSuccessStatusCode)
            {
                string data= respone.Content.ReadAsStringAsync().Result;    
                product=JsonConvert.DeserializeObject<Products>(data);  

                return View(product);
            }

            return View();
            }

            [HttpPost]
            public IActionResult Edit(int id, Products product)
            {
                var response = _http.PutAsJsonAsync($"Product/{id}", product).Result;
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                return View(product);
            }

            public async Task<IActionResult> Delete(int id)
            {
                var response = await _http.DeleteAsync($"Product/{id}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                return NotFound();
            }
        }

    }

