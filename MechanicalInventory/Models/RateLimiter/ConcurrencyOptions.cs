namespace MechanicalInventory.Models.RateLimiter
{
    public class ConcurrencyOptions
    {
        public string? PolicyName { get; set; }
        public int PermitLimit { get; set; }
        public int QueueLimit { get; set; }
    }
}
