using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Callculator.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private readonly string settings = $"{Path.GetTempPath()}/settings.data";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("date")]
        public IActionResult Date(string date)
        {
            var dateTime = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var format = System.IO.File.Exists(settings)
                ? System.IO.File.ReadAllText(settings).Split(';')[0]
                : "dd/MM/yyyy";

            return Json(dateTime.ToString(format, CultureInfo.InvariantCulture));
        }

        [HttpGet("number")]
        public IActionResult Number(decimal number)
        {
            var result = Math.Round(number, 2, MidpointRounding.AwayFromZero)
                .ToString("N", CultureInfo.InvariantCulture);
            var format = System.IO.File.Exists(settings)
                ? System.IO.File.ReadAllText(settings).Split(';')[1]
                : "123,456,789.00";

            switch (format)
            {
                case "123.456.789,00":
                    result = result.Replace(',', ' ').Replace('.', ',').Replace(' ', '.');
                    break;

                case "123 456 789.00":
                    result = result.Replace(',', ' ');
                    break;

                case "123 456 789,00":
                    result = result.Replace(',', ' ').Replace('.', ',');
                    break;
            }

            return Json(result);
        }

        [HttpGet("currency")]
        public IActionResult Currency()
        {
            var result = System.IO.File.Exists(settings)
                ? System.IO.File.ReadAllText(settings).Split(';')[2]
                : "$ - US dollar";

            return Json(result.Split(' ').First());
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = new Settings
            {
                DateFormat = "dd/MMM/yyyy",
                NumberFormat = "123,456,789.00",
                Currency = "$ - US dollar"
            };

            if (System.IO.File.Exists(settings))
            {
                var values = System.IO.File.ReadAllText(settings).Split(';');

                result.DateFormat = values[0];
                result.NumberFormat = values[1];
                result.Currency = values[2];
            }

            return Json(result);
        }

        [HttpPost]
        public IActionResult Save(string dateFormat, string numberFormat, string currency)
        {
            System.IO.File.WriteAllText(settings, $"{dateFormat};{numberFormat};{currency}");
            return Ok();
        }

        class Settings
        {
            public string DateFormat { get; set; }
            public string NumberFormat { get; set; }
            public string Currency { get; set; }
        }
    }
}