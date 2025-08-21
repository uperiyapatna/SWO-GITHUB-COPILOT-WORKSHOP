namespace BikeShopAPI.Entities
{
    public class BikeShop
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool HasDelivery { get; set; }
        public int AddressId { get; set; }
        public virtual List<Bike>? Bikes { get; set; }
        public ShopStatus Status { get; set; }

        public Bike FindBikeById(int id)
        {
            return Bikes?.Find(b => b.Id == id);
        }
    }
}
