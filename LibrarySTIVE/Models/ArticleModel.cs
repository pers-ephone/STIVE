namespace LibrarySTIVE.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Famille { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public int Prix { get; set; }
        public string Age { get; set; } = string.Empty;
        public int ArtQuantity { get; set; }
    }


    public class OrderSummaryModel
    {

        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal ShippingFee { get; set; } = 40;
    }
}