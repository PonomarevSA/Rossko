using System.Collections.Generic;
using Xunit;
using Rossko_test.Services;

namespace Rossko_test.Tests
{
    public class MyOptionResultTests
    {
        private readonly MyOptionResult myOptionResult;

        public MyOptionResultTests()
        {
            myOptionResult = new MyOptionResult();
        }

        [Fact]
        private void OptionResultIsNotNull()
        {
            //Arrange
            string input = "12";

            //Act
            var result = myOptionResult.GeOptionsSortArray(input);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        private void OptionResultIsRight()
        {
            //Arrange
            string input = "12";
            List<string> correctResult = new List<string>() { "12", "21" };

            //Act
            var result = myOptionResult.GeOptionsSortArray(input);

            //Assert
            Assert.Equal(correctResult, result);
        }
    }
}
