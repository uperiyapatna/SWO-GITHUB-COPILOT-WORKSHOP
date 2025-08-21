using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using BikeShopAPI.Controllers;
using BikeShopAPI.Entities;
using Xunit;

namespace BikeShopAPI.Test.Controllers
{
    public class BikeShopControllerTest
    {
        private readonly BikeShopController _controller;
        private readonly Mock<ILogger<BikeShopController>> _loggerMock;

        public BikeShopControllerTest()
        {
            _loggerMock = new Mock<ILogger<BikeShopController>>();
            _controller = new BikeShopController(_loggerMock.Object);
        }

        [Fact]
        public void GetAll_ReturnsAllItems()
        {
            var result = _controller.GetAll().Result as OkObjectResult;

            var items = Assert.IsType<List<BikeShop>>(result.Value);
            Assert.NotEmpty(items);
        }

        [Fact]
        public void GetById_ReturnsBikeShop()
        {
            var result = _controller.GetById(1).Result as OkObjectResult;
            Assert.IsType<BikeShop>(result.Value);
        }

    }
}
