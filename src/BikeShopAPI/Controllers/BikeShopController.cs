using Microsoft.AspNetCore.Mvc;
using BikeShopAPI.Entities;
using System.Diagnostics;

namespace BikeShopAPI.Controllers
{
    [Route("api/bikeshop")]
    [ApiController]

    public class BikeShopController : ControllerBase
    {
        private readonly ILogger<BikeShopController> _logger;

        public BikeShopController(ILogger<BikeShopController> logger)
        {
            _logger = logger;
        }

        private static readonly List<BikeShop> BikeShops = new()
        {
            new BikeShop
            {
                Id = 1,
                Name = "Fast Wheels",
                Description = "Your one-stop shop for high-speed bikes.",
                Category = "Road",
                HasDelivery = true,
                AddressId = 101,
                Status = ShopStatus.Closed,
                Bikes = new List<Bike>
                {
                    new() {
                        Id = 1,
                        Brand = "Cannondale",
                        Model = "Synapse",
                        Description = "A road bike that's light, stiff, fast and surprisingly comfortable.",
                        Price = 2000,
                        ForkTravel = 0,
                        RearTravel = 0,
                        WaterInBidon = 400,
                        ShopId = 1
                    },
                    new() {
                        Id = 2,
                        Brand = "Specialized",
                        Model = "Roubaix",
                        Description = "A performance road bike that's both comfortable and fast.",
                        Price = 2500,
                        ForkTravel = 0,
                        RearTravel = 0,
                        WaterInBidon = 750,
                        ShopId = 1
                    }
                }
            },
            new BikeShop
            {
                Id = 2,
                Name = "Eco Riders",
                Description = "Environmentally friendly bikes for the eco-conscious rider.",
                Category = "Electric",
                HasDelivery = true,
                AddressId = 102,
                Status = ShopStatus.Open,
                Bikes = new List<Bike>
                {
                    new() {
                        Id = 3,
                        Brand = "Gazelle",
                        Model = "CityZen",
                        Description = "A comfortable and fast electric bike for city riding.",
                        Price = 3000,
                        ForkTravel = 0,
                        RearTravel = 0,
                        WaterInBidon = 1000,
                        ShopId = 2
                    },
                    new() {
                        Id = 4,
                        Brand = "Riese & Müller",
                        Model = "Supercharger2",
                        Description = "A high-performance electric bike for long-distance riding.",
                        Price = 4000,
                        ForkTravel = 0,
                        RearTravel = 0,
                        WaterInBidon = 1300,
                        ShopId = 2
                    }
                }
            },
            new BikeShop
            {
                Id = 3,
                Name = "City Cruisers",
                Description = "Perfect bikes for the urban jungle.",
                Category = "Urban",
                HasDelivery = false,
                AddressId = 103,
                Status = ShopStatus.Renovating,
                Bikes = new List<Bike>
                {
                    new() {
                        Id = 5,
                        Brand = "VanMoof",
                        Model = "S3",
                        Description = "A stylish and smart bike for city riding.",
                        Price = 1500,
                        ForkTravel = 0,
                        RearTravel = 0,
                        WaterInBidon = 1500,
                        ShopId = 3
                    },
                    new() {
                        Id = 6,
                        Brand = "Tern",
                        Model = "GSD",
                        Description = "A compact and powerful e-bike for city riding.",
                        Price = 2000,
                        ForkTravel = 0,
                        RearTravel = 0,
                        WaterInBidon = 1200,
                        ShopId = 3
                    }
                }
            }
        };

        [HttpGet]
        public ActionResult<List<BikeShop>> GetAll()
        {
            _logger.LogInformation("GET all 🚲🚲🚲 NO PARAMS 🚲🚲🚲");

            return Ok(BikeShops);
        }

        [HttpGet("{id}")]
        public ActionResult<BikeShop> GetById(int id)
        {
            var bikeshop = BikeShops.Find(p => p.Id == id);

            if (bikeshop == null)
            {
                return NotFound();
            }

            return Ok(bikeshop);
        }

        [HttpPost]
        public ActionResult<BikeShop> Create(BikeShop bikeShop)
        {
            BikeShops.Add(bikeShop);
            return CreatedAtAction(nameof(GetById), new { id = bikeShop.Id }, bikeShop);
        }

        [HttpPost("{id}/status")]
        public ActionResult UpdateShopStatus(int id, ShopStatus newStatus)
        {
            var bikeShop = BikeShops.Find(b => b.Id == id);
            if (bikeShop != null)
            {
                switch (newStatus)
                {
                    case ShopStatus.Open:
                        if (bikeShop.Status == ShopStatus.Renovating)
                        {
                            return BadRequest("Cannot reopen, shop is currently renovating.");
                        }
                        break;

                    case ShopStatus.Closed:
                        if (bikeShop.Status == ShopStatus.Renovating)
                        {
                            return BadRequest("Cannot close, shop is currently renovating.");
                        }
                        break;

                    case ShopStatus.Renovating:
                        if (bikeShop.Status == ShopStatus.Open)
                        {
                            return BadRequest("Shop must be closed before starting renovations.");
                        }
                        break;

                    default:
                        return BadRequest("Unknown or unsupported shop status.");
                }

                bikeShop.Status = newStatus;

                return Ok($"Bike shop status updated to {newStatus}.");
            }
            else
            {
                return NotFound("Bike shop not found.");
            }
        }

        [HttpPost("{shopId}/bikes/{bikeId}/takeRide/{rideLength}")]
        public ActionResult TakeRide(int shopId, int bikeId, int rideLength)
        {
            var bikeShop = BikeShops.Find(b => b.Id == shopId);

            var bike = bikeShop?.FindBikeById(bikeId);

            if (bike == null)
            {
                return NotFound("Bike not found in the specified bike shop.");
            }

            int waterConsumptionPerKm = 50;

            for (int i = 0; i < rideLength; i++)
            {
                if (bike.WaterInBidon == 0)
                {
                    throw new Exception("Bike stopped, rider dehydrated due to lack of water.");
                }
                else
                {
                    bike.WaterInBidon -= waterConsumptionPerKm;
                }
            }

            return Ok($"Bike rode {rideLength} kilometers. Water left in bidon: {bike.WaterInBidon} ml.");
        }

        [HttpPost("{shopId}/bikes/{bikeId}/bikeMalfunction")]
        public ActionResult BikeMalfunction(int shopId, int bikeId)
        {
            var bikeShop = BikeShops.Find(b => b.Id == shopId);

            var bike = bikeShop?.FindBikeById(bikeId);

            if (bike == null)
            {
                return NotFound("Bike not found in the specified bike shop.");
            }

            // Simulate a malfunction causing recursion on the bicycle 
            BikeMalfunction(shopId, bikeId);

            return Ok($"Recovered from bicycle malfunction.");
        }

        [HttpPost("{shopId}/bikes/{bikeId}/calculateRange")]
        public ActionResult CalculateRange(int shopId, int bikeId)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            List<int> primes = CalculatePrimes(shopId + bikeId, 300000);

            stopwatch.Stop();
            Console.WriteLine($"Found {primes.Count} prime numbers.");
            Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds / 1000.0} seconds");

            return Ok($"Calculated range.");
        }

        public static List<int> CalculatePrimes(int start, int end)
        {
            List<int> primes = new List<int>();
            for (int number = start; number <= end; number++)
            {
                if (IsPrime(number))
                {
                    primes.Add(number);
                }
            }
            return primes;
        }

        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
