namespace MechanicalInventory.Models.RateLimiter
{
    public class FixedWindowOptions
    {
        public dynamic? FixedWindow{ get; set; }
        public string? PolicyName { get; set; }
        public int PermitLimit { get; set; }
        public int QueueLimit { get; set; }
        public int WindowTime { get; set; }
    }
}
