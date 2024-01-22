﻿namespace MechanicalInventory.Models
{
    public class KtmProduct
    {
        public int Id { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? ManufacturerCountry { get; set; }
        public DateTime? ManufacturerYear { get; set; }
        public int Quantity { get; set; }
        public int QualityLevel { get; set; }
        public Decimal SellingPrice { get; set; }
        public Decimal PurchasePrice { get; set; }
    }
}
