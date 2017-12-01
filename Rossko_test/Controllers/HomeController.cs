using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rossko_test.Services;

namespace Rossko_test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptionsArray _myService;

        public HomeController(IOptionsArray serv)
        {
            _myService = serv;
        }
        public string Index(Char[] input)
        {
            return _myService.GetOptions(input);
        }
    }
}