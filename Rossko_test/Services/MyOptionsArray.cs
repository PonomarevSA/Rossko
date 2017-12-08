using System;
using System.Linq;
using Rossko_test.Model;
using System.Diagnostics;
using Rossko_test.Core;
using Rossko_test.Interfaces;

namespace Rossko_test.Services
{
    public class MyOptionsArray : IOptionsArray
    {
        private readonly IRosskoRepository repository;
        private readonly IValidate validator;
        private readonly IJsonResult jsonResult;
        private readonly IOptionResult optionResult;

        public MyOptionsArray(IRosskoRepository repository, IValidate validator,
                IJsonResult jsonResult, IOptionResult optionResult)
        {
            this.repository = repository;
            this.validator = validator;
            this.jsonResult = jsonResult;
            this.optionResult = optionResult;
        }

        public string GetOptions(Char[] input)
        {
            Stopwatch s = Stopwatch.StartNew();
            string inputArray = new string(input.OrderBy(p => p).ToArray());
            OptionsArrayJsonModel model = new OptionsArrayJsonModel() { IsError = false };
            OptionsArray optionArray;
            //проверим входящие данные
            var (isError, errorMessage) = validator.Validate(input);
            if (isError)
            {
                model.IsError = isError;
                model.ErrorMessage = errorMessage;
                s.Stop();
                model.WorkTime = s.Elapsed.ToString();
                optionArray = new OptionsArray
                {
                    Keyword = inputArray,
                    OptionsList = jsonResult.GetJsonResult(model)
                };
                return jsonResult.GetJsonResult(model);
            }
            //проверим встречался ли нам такой случай
            if (repository.AnyByKeyword(inputArray))
            {
                var item = repository.GetByKeyword(inputArray);
                return item.OptionsList;
            }
            model.Options = optionResult.GeOptionsSortArray(inputArray);
            s.Stop();
            model.WorkTime = s.Elapsed.ToString();
            optionArray = new OptionsArray
            {
                Keyword = inputArray,
                OptionsList = jsonResult.GetJsonResult(model)
            };
            repository.Insert(optionArray);
            return optionArray.OptionsList;
        }
    }
}
