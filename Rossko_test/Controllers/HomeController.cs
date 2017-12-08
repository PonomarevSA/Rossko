using System;
using Microsoft.AspNetCore.Mvc;
using Rossko_test.Interfaces;

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