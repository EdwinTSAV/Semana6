using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Finanzas.Models;
using Microsoft.AspNetCore.Authorization;

namespace Finanzas.Controllers
{
    public class HomeController : Controller
    {
        private List<Country> pais = new List<Country>
        {
            new Country {Id = 1, Name = "Siberia", IdContinent = 1},
            new Country {Id = 2, Name = "Manchuria", IdContinent = 1},
            new Country {Id = 3, Name = "Argentina", IdContinent = 2},
            new Country {Id = 4, Name = "Peru", IdContinent = 2},
            new Country {Id = 5, Name = "Congo", IdContinent = 3},
            new Country {Id = 6, Name = "Madagascar", IdContinent = 3},
            new Country {Id = 7, Name = "Kerguelen", IdContinent = 4},
            new Country {Id = 8, Name = "Islas Crozet", IdContinent = 4},
            new Country {Id = 9, Name = "Madre Rusia", IdContinent = 5},
            new Country {Id = 10, Name = "Ucrania", IdContinent = 5},
            new Country {Id = 11, Name = "Australia", IdContinent = 6},
            new Country {Id = 12, Name = "Micronesia", IdContinent = 6}
        };
        private List<City> ciudad = new List<City>
        {
            new City {Id = 1, Name = "Tobolsk", IdCountry = 1},
            new City {Id = 2, Name = "Vladivostok", IdCountry = 2},
            new City {Id = 3, Name = "Buenos Aires", IdCountry = 3},
            new City {Id = 4, Name = "Peru", IdCountry = 4},
            new City {Id = 5, Name = "Matadi", IdCountry = 5},
            new City {Id = 6, Name = "Antananarivo", IdCountry = 6},
            new City {Id = 7, Name = "Kerguelen", IdCountry = 7},
            new City {Id = 8, Name = "Isla de los Cerdos", IdCountry = 8},
            new City {Id = 9, Name = "Moscu", IdCountry = 9},
            new City {Id = 10, Name = "Leopolis", IdCountry = 10},
            new City {Id = 11, Name = "Sidney", IdCountry = 11},
            new City {Id = 12, Name = "Palikir", IdCountry = 12}
        };

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult _continente(int IdContinent)
        {
            var paisp = pais.Where(o => o.IdContinent == IdContinent).ToList();
            return View(paisp);
        }
        // otra forma de mandar datos para manipularlos despues con Jason: // en ese caso, se manipula desde el index
        [HttpGet]
        public IActionResult _ciudad(int idCountry)
        {
            var ciud = ciudad.Where(o => o.IdCountry == idCountry).ToList();
            return Json(ciud);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
