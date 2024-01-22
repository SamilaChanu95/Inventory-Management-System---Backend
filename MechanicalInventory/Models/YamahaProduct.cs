namespace MechanicalInventory.Models
{
    public class YamahaProduct
    {
        public int Id { get; set; }
        public string? YamahaCode { get; set; }
        public string? ProductName { get; set; }
        public string? OriginCountry { get; set; }
        public DateTime? ManufacturerYear { get; set; }
        public DateTime? PackedOn { get; set; }
        public int Quantity { get; set; }
        public int QualityLevel { get; set; }
        public Decimal SellingPrice { get; set; }
        public Decimal PurchasePrice { get; set; }
    }
}
