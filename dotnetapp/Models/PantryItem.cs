namespace dotnetapp.Models
{
    public class PantryItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int StockItem { get; set; }
        public int Price { get; set; }
        public string ExpDate { get; set; }
    }
}
