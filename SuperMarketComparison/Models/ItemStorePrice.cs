using System.ComponentModel.DataAnnotations;

namespace SuperMarketComparison.Models
{
    public class ItemStorePrice
    {
        public int Id { get; set; }

        // FOREIGN KEYS
        // foreign key ItemId from Item
        public int ItemId { get; set; }
        public Item Item { get; set; }

        // foreign key StoreId from Store
        public int StoreId { get; set; }
        public Store Store { get; set; }

        // Adicional fields PRICE, DATE
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastUpdate { get; set; }
    }
}
