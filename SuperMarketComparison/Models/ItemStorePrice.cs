using System.ComponentModel.DataAnnotations;

namespace SuperMarketComparison.Models
{
    public class ItemStorePrice
    {
        public int Id { get; set; }

        // FOREIGN KEYS
        // foreign key ItemId from Item
        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        // foreign key StoreId from Store
        [Required]
        public int StoreId { get; set; }
        public Store Store { get; set; }

        // Adicional fields PRICE, DATE
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime LastUpdate { get; set; }
    }
}
