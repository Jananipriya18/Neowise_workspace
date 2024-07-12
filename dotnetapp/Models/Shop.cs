namespace dotnetapp.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int StockItem { get; set; }
        public int Price { get; set; }
        public string MfDate { get; set; }
        public string CompanyName { get; set; }
    }
}
