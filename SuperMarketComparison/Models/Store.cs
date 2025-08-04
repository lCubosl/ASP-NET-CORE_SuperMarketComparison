namespace SuperMarketComparison.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<ItemStorePrice> Prices { get; set; }
    }
}
