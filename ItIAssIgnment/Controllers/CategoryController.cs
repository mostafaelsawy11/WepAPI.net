using ItIAssIgnment.DTO;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices services;
        public CategoryController(ICategoryServices services)
        {
            this.services = services;
        }
        [HttpGet("{id:int}")]
        public IActionResult CategoryWithProduct(int id)
        {
            CategoryWithProducts CWP = new CategoryWithProducts();
            Category c = services.GetById(id);
            CWP.Name = c.Name;
            foreach(var item in c.Products)
            {
                CWP.products.Add(new ProductName { Id = item.Id, Name = item.Name });
            }
            return Ok(CWP);
        }
    }
}
