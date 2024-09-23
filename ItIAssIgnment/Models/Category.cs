using System.ComponentModel.DataAnnotations;

namespace ItIAssIgnment.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(100,2000)]
        public int Salary { get; set; }
        public virtual List<Product>? Products { get; set; }
    }
}
