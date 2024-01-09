namespace MechanicalInventory.Models
{
    public class BajajProduct
    {
        public int Id { get; set; }
        public string? BajajCode { get; set; }
        public string? ProductName { get; set; }
        public DateTime? ManufacturerYear { get; set; }
        public int Quantity { get; set; }
        public int QualityLevel { get; set; }
        public Decimal SellingPrice { get; set; }
        public Decimal PurchasePrice { get; set; }
    }
}
