namespace MechanicalInventory.Models
{
    public class TvsProduct
    {
        public int Id { get; set; }
        public string? TvsCode { get; set; }
        public string? ProductName { get; set; }
        public string? ManufacturerName { get; set; }
        public DateTime? ManufacturerYear { get; set; }
        public int Quantity { get; set; }
        public int QualityLevel { get; set; }
        public Decimal SellingPrice { get; set; }
        public Decimal PurchasePrice { get; set; }
    }
}
