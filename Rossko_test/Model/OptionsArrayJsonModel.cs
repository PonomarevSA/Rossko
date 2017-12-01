using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Rossko_test.Model
{
    [DataContract]
    public class OptionsArrayJsonModel
    {
        //public string Input { get; set; }
        //варианты перестановок
        [DataMember]
        public List<string> Options { get; set; }
        //время выполнения
        [DataMember]
        public string WorkTime { get; set; }
        //была ли ошибка при выполнение
        [DataMember]
        public bool IsError { get; set; }
        //текст ошибки
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
