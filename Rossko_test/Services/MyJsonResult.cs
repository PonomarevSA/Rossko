using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rossko_test.Interfaces;
using Rossko_test.Model;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace Rossko_test.Services
{
    public class MyJsonResult : IJsonResult
    {
        public string GetJsonResult(OptionsArrayJsonModel model)
        {
            var serializer = new DataContractJsonSerializer(typeof(OptionsArrayJsonModel));
            var ms = new MemoryStream();
            serializer.WriteObject(ms, model);
            return Encoding.Default.GetString(ms.ToArray());
        }
    }
}
