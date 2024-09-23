using ItIAssIgnment.Models;
using ItIAssIgnment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItIAssIgnment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices services;
        public ProductController(IProductServices services)
        {
            this.services = services;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = services.GetAll();
            return Ok(products);
        }
        [HttpGet("{id:int}",Name ="EmployeeDetails")]
        public IActionResult GetById(int id)
        {
            Product product = services.GetById(id);
            return Ok(product);
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id ,Product product)
        {
            if (ModelState.IsValid)
            {
                services.update(id, product);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            services.deleteById(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPost]
        public IActionResult createProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                services.AddProduct(product);
                string url = Url.Link("EmployeeDetails", new { id = product.Id });
                return Created(url, product);
            }
            return BadRequest(ModelState);
        }


    }
}
