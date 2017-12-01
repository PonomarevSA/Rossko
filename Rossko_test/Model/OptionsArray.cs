using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Rossko_test.Model
{
    public class OptionsArray
    {
        [Key]
        public string Keyword { get; set; }

        public string OptionsList { get; set; }
    }
}
