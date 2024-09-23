using ItIAssIgnment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ItIAssIgnment.Services
{
    public class CategoryServices : ICategoryServices
    {
        private ApplicationDbContext context;
        public CategoryServices(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Category GetById(int id)
        {
            Category c = context.Categories.Include(e=>e.Products).FirstOrDefault(e=>e.Id==id);
            return c;
            
        }
    }
}
