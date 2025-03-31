namespace gestionproduit.Models
{
    public class Productt
    {
        public int Id { get; set; }
        public string Title { get; set; } // ✅ "title" in API corresponds to "Name"
        public decimal Price { get; set; }
        public string Image { get; set; } // ✅ "image" in API corresponds to "ImagePath"

    }

}
