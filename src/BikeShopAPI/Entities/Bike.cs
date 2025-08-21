namespace BikeShopAPI.Entities
{
    public class Bike
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int ForkTravel { get; set; }
        public int RearTravel { get; set; }
        public int WaterInBidon { get; set; }

        public int ShopId { get; set; }
        public virtual BikeShop? BikeShop { get; set; }
    }
}
