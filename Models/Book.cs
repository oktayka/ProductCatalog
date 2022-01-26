namespace ProductCatalog.Models
{
    public class Book : Product
    {
        public string Author { get; set; }
        public string Publisher { get; set; }

        public string Genre { get; set; }

        public int PublishingYear { get; set; }

    }
}