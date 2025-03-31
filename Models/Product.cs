using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace gestionproduit.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Range(0, 1000)]
        public int Stock { get; set; }

        public string? ImagePath { get; set; }
        // ✅ Soft Delete flag
        public bool IsActive { get; set; } = true;


    }
}
