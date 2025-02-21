namespace LibrarySTIVE.Models
{
    public class CompleteOrderModel
    {
        public OrderSummaryModel OrderSummary { get; set; }
        public ArticleModel Article { get; set; }
        public ClientModel Client { get; set; }
    }
}