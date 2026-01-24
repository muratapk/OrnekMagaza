using MagazaApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrnekMagaza.Models;

namespace MagazaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly BaglanDb _context;
        public ProductController(BaglanDb context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            //ürünlerin lisesini döndürüyor
            //product tablosundaki tüm verileri çekiyor
            return Ok(products);
        }
        [HttpGet("id")]
        public IActionResult GetProductid(int id)
        {
            var product = _context.Products.Find(id);
            return Ok(product);
        }
        [HttpPost]
        public IActionResult CreateProduct(Products product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {   
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Products updatedProduct)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            product.ProductName = updatedProduct.ProductName;
            product.UnitPrice = updatedProduct.UnitPrice;
            product.CategoryID = updatedProduct.CategoryID;
            product.ProductImage = updatedProduct.ProductImage;

            _context.SaveChanges();
            return Ok(product);
        }
    }
}
