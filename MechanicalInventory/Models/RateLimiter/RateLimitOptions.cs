namespace MechanicalInventory.Models.RateLimiter
{
    public class RateLimitOptions
    {
        public FixedWindowOptions? fixedWindowOptions { get; set; }
        public ConcurrencyOptions? concurrencyOptions { get; set; }

    }
}
