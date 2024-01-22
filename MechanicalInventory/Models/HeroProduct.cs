namespace MechanicalInventory.Models
{
    public class HeroProduct
    {
        public int Id { get; set; }
        public string? HeroCode { get; set; }
        public string? ProductName { get; set; }
        public DateTime? ManufacturerYear { get; set; }
        public int Quantity { get; set; }
        public int QualityLevel { get; set; }
        public Decimal SellingPrice { get; set; }
        public Decimal PurchasePrice { get; set; }
    }
}

