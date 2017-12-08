using System;
using System.Collections.Generic;
using Xunit;
using Rossko_test.Services;
using Rossko_test.Interfaces;
using Rossko_test.Model;
using Rossko_test.Core;
using Moq;

namespace Rossko_test.Tests
{
    public class MyOptionsArrayTests
    {
        private readonly Mock<IRosskoRepository> mockRepository;
        private readonly Mock<IJsonResult> mockJson;
        private readonly Mock<IValidate> mockValidate;
        private readonly Mock<IOptionResult> mockOptionResult;

        public MyOptionsArrayTests()
        {
            mockRepository = new Mock<IRosskoRepository>();
            mockJson = new Mock<IJsonResult>();
            mockValidate = new Mock<IValidate>();
            mockOptionResult = new Mock<IOptionResult>();
        }

        [Fact]
        private void OptionArrayInsertDatabase()
        {
            //Arrange
            string keyword = "12";
            mockRepository.Setup(repo => repo.AnyByKeyword(keyword))
                .Returns(false);
            mockJson.Setup(p => p.GetJsonResult(It.IsAny<OptionsArrayJsonModel>()))
                .Returns(GetJsonResult());
            Char[] input = new Char[] { '1', '2' };
            mockValidate.Setup(p => p.Validate(input))
                .Returns((false, ""));
            mockOptionResult.Setup(p => p.GeOptionsSortArray(keyword))
                .Returns(GetListObject());
            MyOptionsArray myOptionsArray 
                = new MyOptionsArray(mockRepository.Object, mockValidate.Object, mockJson.Object, mockOptionResult.Object);

            //Act
            var result = myOptionsArray.GetOptions(input);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }

        [Fact]
        private void OptionArraySelectDatabase()
        {
            //Arrange
            string keyword = "12";
            mockRepository.Setup(repo => repo.AnyByKeyword(keyword))
                .Returns(true);
            mockRepository.Setup(repo => repo.GetByKeyword(keyword))
                .Returns(GetOptionsArray);
            mockJson.Setup(p => p.GetJsonResult(It.IsAny<OptionsArrayJsonModel>()))
                .Returns(GetJsonResult());
            Char[] input = new Char[] { '1', '2' };
            mockValidate.Setup(p => p.Validate(input))
                .Returns((false, ""));
            mockOptionResult.Setup(p => p.GeOptionsSortArray(keyword))
                .Returns(GetListObject());
            MyOptionsArray myOptionsArray
                = new MyOptionsArray(mockRepository.Object, mockValidate.Object, mockJson.Object, mockOptionResult.Object);

            //Act
            var result = myOptionsArray.GetOptions(input);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }

        [Fact]
        private void OptionArrayValidateError()
        {
            //Arrange
            mockJson.Setup(p => p.GetJsonResult(It.IsAny<OptionsArrayJsonModel>()))
                .Returns(GetJsonNotValidResult());
            Char[] input = new Char[] { '1', '2' };
            mockValidate.Setup(p => p.Validate(input))
                .Returns((true, ""));
            MyOptionsArray myOptionsArray
                = new MyOptionsArray(mockRepository.Object, mockValidate.Object, mockJson.Object, mockOptionResult.Object);

            //Act
            var result = myOptionsArray.GetOptions(input);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }

        private string GetJsonResult()
        {
            return "{\"ErrorMessage\":null,\"IsError\":false,\"Options\":[\"12\",\"21\"],\"WorkTime\":\"00:00:01.2736347\"}";
        }

        private string GetJsonNotValidResult()
        {
            return "{\"ErrorMessage\":\"An array must be between 1 and 8 charchers\",\"IsError\":true,\"Options\":null,\"WorkTime\":\"00:00:00.0191756\"}";
        }

        private List<string> GetListObject()
        {
            List<string> result = new List<string>
            {
                "12",
                "21"
            };
            return result;
        }

        private OptionsArray GetOptionsArray()
        {
            return new OptionsArray()
            {
                Keyword = "12",
                OptionsList = GetJsonResult()
            };
        }
    }
}
