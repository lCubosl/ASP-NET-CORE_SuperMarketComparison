namespace SuperMarketComparison.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        // FOREIGN KEYS
        // foreign key ItemStorePriceId from ItemStorePrice
        public int ItemStorePriceId { get; set; }
        public ItemStorePrice? ItemStorePrice { get; set; }

        // foreign key to cart
        public int CartId { get; set; }
        public Cart? Cart { get; set; }

        public bool IsChecked { get; set; } = false;
    }
}
