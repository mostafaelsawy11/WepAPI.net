using ItIAssIgnment.Models;

namespace ItIAssIgnment.Services
{
    public class ProductServices:IProductServices
    {
        private readonly ApplicationDbContext context;
        public ProductServices(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<Product> GetAll()
        {
            List<Product> products = context.Products.ToList();
            return products;
        }
        
        public Product GetById(int id)
        {
            Product product = context.Products.FirstOrDefault(x => x.Id == id);

            return product;
        }
        public void update( int id ,Product product)
        {
            Product oldPro = context.Products.FirstOrDefault(product => product.Id == id);
            if (oldPro != null)
            {
                oldPro.Name= product.Name;
                oldPro.Description = product.Description;
                oldPro.Price = product.Price;
                context.SaveChanges();
            }
        }
        public void deleteById(int id)
        {
            Product product= context.Products.FirstOrDefault(p => p.Id == id);
            context.Products.Remove(product);
            context.SaveChanges();
        }
        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }
    }
}
