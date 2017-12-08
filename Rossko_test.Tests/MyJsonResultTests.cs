using Xunit;
using Rossko_test.Services;
using Rossko_test.Model;

namespace Rossko_test.Tests
{
    public class MyJsonResultTests
    {
        private readonly MyJsonResult myJsonResult;

        public MyJsonResultTests()
        {
            myJsonResult = new MyJsonResult();
        }

        [Fact]
        public void JsonResultIsNotNull()
        {
            //Arrange
            OptionsArrayJsonModel model = new OptionsArrayJsonModel();

            //Act
            var result = myJsonResult.GetJsonResult(model);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void JsonResultTypeTest()
        {
            //Arrange
            OptionsArrayJsonModel model = new OptionsArrayJsonModel();

            //Act
            var result = myJsonResult.GetJsonResult(model);

            //Assert
            Assert.IsType<string>(result);
        }
    }
}
