using System;
using Xunit;
using Rossko_test.Interfaces;
using Rossko_test.Controllers;
using Moq;


namespace Rossko_test.Tests
{
    public class HomeControllerTests
    {
        private readonly Mock<IOptionsArray> mock;
        private readonly HomeController homeController;

        public HomeControllerTests()
        {
            mock = new Mock<IOptionsArray>();
            mock.Setup(p => p.GetOptions(It.IsAny<Char[]>()))
                .Returns(GetJsonResult());
            homeController = new HomeController(mock.Object);
        }

        [Fact]
        private void ResultIsNotNull()
        {
            //Arrange
            Char[] input = new Char[] { '1', '2' };

            //Act
            var result = homeController.Index(input);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        private void ResultTypeTest()
        {
            //Arrange
            Char[] input = new Char[] { '1', '2' };
            

            //Act
            var result = homeController.Index(input);

            //Assert
            Assert.IsType<string>(result);
        }

        private string GetJsonResult()
        {
            return "{\"ErrorMessage\":null,\"IsError\":false,\"Options\":[\"12\",\"21\"],\"WorkTime\":\"00:00:01.2736347\"}";
        }
    }
}
