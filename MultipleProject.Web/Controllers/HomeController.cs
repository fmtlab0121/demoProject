using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MultipleProject.Libraries;
using MultipleProject.Models;
using MultipleProject.Web.Models;
using Newtonsoft.Json;

namespace MultipleProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                _logger.LogDebug("Rendering the Index View");
                List<WeatherForecast> wf = null;
                ViewBag.Comment = _config["Welcome"];
                var response = await WebCall.GetApiDataAsync(_config["BackendServer"], "WeatherForecast");

                if (response != null)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    wf = JsonConvert.DeserializeObject<List<WeatherForecast>>(responseString);
                }
                return View(wf);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }

        }

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
