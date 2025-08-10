using System.ComponentModel.DataAnnotations;

namespace SuperMarketComparison.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.Date)]
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    }
}
