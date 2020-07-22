using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomLoggerProvider.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace CustomLoggerProvider.Controllers
{
public class HomeController : Controller
{
    private readonly ILogger _logger;

    public HomeController(ILogger<HomeController> logger) => _logger = logger;

    public IActionResult Index()
    {
            //throw new NullReferenceException();
            _logger.LogInformation("Chamando a página inicial do site");
            _logger.LogWarning("LogWarning");
            _logger.LogCritical("LogCritical");
            _logger.LogError("LogError");
            _logger.LogTrace("LogTrace");


            //throw new Exception();


            return View();
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
