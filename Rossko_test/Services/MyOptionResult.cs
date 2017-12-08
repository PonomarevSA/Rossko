using System.Collections.Generic;
using System.Linq;
using Rossko_test.Interfaces;
using System.Text;

namespace Rossko_test.Services
{
    public class MyOptionResult : IOptionResult
    {
        //Список перестановок
        private Dictionary<string, string> permutationsList;

        private void AddToList(char[] input)
        {
            var bufer = new StringBuilder("");
            for (int i = 0; i < input.Count(); i++)
            {
                bufer.Append(input[i]);
            }
            if (!permutationsList.ContainsKey(bufer.ToString()))
            {
                permutationsList.Add(bufer.ToString(), bufer.ToString());
            }
        }


        private void RecOption(char[] input, int length)
        {
            for (var i = 0; i < length; i++)
            {
                var temp = input[length - 1];
                for (var j = length - 1; j > 0; j--)
                {
                    input[j] = input[j - 1];
                }
                input[0] = temp;
                if (i < length - 1)
                {
                    AddToList(input);
                }
                if (length > 0)
                {
                    RecOption(input, length - 1);
                }
            }
        }

        private Dictionary<string, string> GetOptionsList(string input)
        {
            permutationsList = new Dictionary<string, string>();
            permutationsList.Add(input, input);
            RecOption(input.ToArray(), input.Length);
            return permutationsList;
        }

        public List<string> GeOptionsSortArray(string input)
        {
            return GetOptionsList(input).Keys.OrderBy(p => p).ToList();
        }
    }
}
