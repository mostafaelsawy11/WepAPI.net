using ItIAssIgnment.Models;

namespace ItIAssIgnment.Services
{
    public interface IProductServices
    {
        public List<Product> GetAll();
        public Product GetById (int id);
        public void update(int id ,Product product);   
        public void deleteById(int id);
        public void AddProduct(Product product);
    }
}
