using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Rossko_test.Model;
using System.Web;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Diagnostics;
using Rossko_test.Model;
using Microsoft.EntityFrameworkCore;

namespace Rossko_test.Services
{
    public class MyOptionsArray : IOptionsArray
    {
        private readonly RosskoContext db;

        public MyOptionsArray(RosskoContext ctx)
        {
            db = ctx;
        }

        public string GetOptions(Char[] input)
        {

            Stopwatch s = Stopwatch.StartNew();
            OptionsArrayJsonModel model = new OptionsArrayJsonModel();
            var serializer = new DataContractJsonSerializer(typeof(OptionsArrayJsonModel));
            var serializerDb = new DataContractJsonSerializer(typeof(List<String>));
            var ms = new MemoryStream();
            //проверим входящие данные
            if (input.Count() < 1 || input.Count() > 8)
            {
                model.IsError = true;
                model.ErrorMessage = "An array must be between 1 and 8 charchers";
                s.Stop();
                model.WorkTime = s.Elapsed.ToString();
                serializer.WriteObject(ms, model);
                return Encoding.Default.GetString(ms.ToArray());
            }
            Regex rgx = new Regex("[a-zA-Z0-9]");
            foreach (var item in input)
            {
                if (!rgx.IsMatch(item.ToString()))
                {
                    model.IsError = true;
                    model.ErrorMessage = "Array is not valid";
                    s.Stop();
                    model.WorkTime = s.Elapsed.ToString();
                    serializer.WriteObject(ms, model);
                    return Encoding.Default.GetString(ms.ToArray());
                }
            }

            string inputArray = new string(input.OrderBy(p => p).ToArray());
            int test = db.OptionsArrays.Count();
            //проверим встречался ли нам такой случай
            if (db.OptionsArrays.Any(p => p.Keyword == inputArray))
            {
                var item = db.OptionsArrays.FirstOrDefault(p => p.Keyword == inputArray);
                return item.OptionsList;
            }

            model.Options = GeOptionsSortArray(new string(input));
            model.IsError = false;
            s.Stop();
            model.WorkTime = s.Elapsed.ToString();
            serializer.WriteObject(ms, model);
            OptionsArray optionArray = new OptionsArray
            {
                Keyword = new string(input.OrderBy(p => p).ToArray()),
                OptionsList = Encoding.Default.GetString(ms.ToArray())
            };
            db.OptionsArrays.Add(optionArray);
            db.SaveChanges();
            return optionArray.OptionsList;
        }

        //Список перестановок
        private Dictionary<string, string> _permutationsList;

        private void AddToList(char[] a)
        {
            var bufer = new StringBuilder("");
            for (int i = 0; i < a.Count(); i++)
            {
                bufer.Append(a[i]);
            }
            if (!_permutationsList.ContainsKey(bufer.ToString()))
            { 
                _permutationsList.Add(bufer.ToString(), bufer.ToString());
            }

        }


        private void RecOption(char[] a, int n)
        {
            for (var i = 0; i < n; i++)
            {
                var temp = a[n - 1];
                for (var j = n - 1; j > 0; j--)
                {
                    a[j] = a[j - 1];
                }
                a[0] = temp;
                if (i < n - 1)
                {
                    AddToList(a);
                }
                if (n > 0)
                {
                    RecOption(a, n - 1);
                }
            }
        }
        public Dictionary<string, string> GetOptionsList(string str)
        {
            _permutationsList = new Dictionary<string, string>();
            _permutationsList.Add(str, str);
            RecOption(str.ToArray(), str.Length);
            return _permutationsList;
        }
        public List<string> GeOptionsSortArray(string str)
        {
            return GetOptionsList(str).Keys.OrderBy(p => p).ToList();

        }

        
    }
}
