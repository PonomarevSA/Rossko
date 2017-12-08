using System.Linq;
using Rossko_test.Interfaces;
using System.Text.RegularExpressions;

namespace Rossko_test.Services
{
    public class MyValidate : IValidate
    {
        public (bool, string) Validate(char[] input)
        {
            //допустимая длина
            if (input.Count() < 1 || input.Count() > 8)
            {
                return (true, "An array must be between 1 and 8 charchers");
            }
            //допустимые символы
            Regex regex = new Regex("[a-zA-Z0-9]");
            foreach (var item in input)
            {
                if (!regex.IsMatch(item.ToString()))
                {
                    return (true, "Array is not valid");
                }
            }
            return (false, "");
        }
    }
}
