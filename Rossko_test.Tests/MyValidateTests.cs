using System;
using Xunit;
using Rossko_test.Services;

namespace Rossko_test.Tests
{
    public class MyValidateTests
    {
        private readonly MyValidate myValidate;

        public MyValidateTests()
        {
            myValidate = new MyValidate();
        }

        [Fact]
        private void LengthValidateTest()
        {
            //Arrange
            Char[] inputShort = new Char[] {  };
            Char[] inputLong = new Char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Char[] inputNormal1 = new Char[] { '1' };
            Char[] inputNormal2 = new Char[] { '1', '2', '3', '4', '5', '6', '7', '8' };

            //Act
            var (isErrorShor, errorMessageShort) = myValidate.Validate(inputShort);
            var (isErrorLong, errorMessageLong) = myValidate.Validate(inputLong);
            var (isErrorNormal1, errorMessageNormal1) = myValidate.Validate(inputNormal1);
            var (isErrorNormal2, errorMessageNormal2) = myValidate.Validate(inputNormal2);

            //Assert
            Assert.Equal(true, isErrorShor);
            Assert.Equal(true, isErrorLong);
            Assert.Equal(false, isErrorNormal1);
            Assert.Equal(false, isErrorNormal2);
            Assert.Equal("An array must be between 1 and 8 charchers", errorMessageShort);
            Assert.Equal("An array must be between 1 and 8 charchers", errorMessageLong);
            Assert.Equal("", errorMessageNormal1);
            Assert.Equal("", errorMessageNormal2);
        }

        [Fact]
        private void ValueValidateTest()
        {
            //Arrange
            Char[] inputBad = new Char[] { ',', '.', '1' };
            Char[] inputGood = new Char[] { '1', '2', '3' };

            //Act
            var (isErrorBad, errorMessageBad) = myValidate.Validate(inputBad);
            var (isErrorGood, errorMessageGood) = myValidate.Validate(inputGood);

            //Assert
            Assert.Equal(true, isErrorBad);
            Assert.Equal(false, isErrorGood);
            Assert.Equal("Array is not valid", errorMessageBad);
            Assert.Equal("", errorMessageGood);
        }
    }
}
