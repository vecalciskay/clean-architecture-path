using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InjectionPattern.Models;
using InjectionPattern.PatternObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InjectionPattern.Controllers
{
    public class HomeController : Controller
    {
        private IDateTimeService _dateTimeService;

        public HomeController(IDateTimeService datetimeService)
        {
            _dateTimeService = datetimeService;
        }

        public IActionResult Index(int id)
        {
            if (id > 0)
            {
                _dateTimeService = new StandardDateTimeService();
            }

            Persona p = new Persona();
            p.Nombre = "Vladimir Calderon";            
            p.HoyDia = _dateTimeService.GetDateTime();
            p.WithInjection = id <= 0 ? true : false;

            return View("~/Pages/Index.cshtml", p);
        }
    }
}
