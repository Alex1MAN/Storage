using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string Description { get; set; }


    }
}
