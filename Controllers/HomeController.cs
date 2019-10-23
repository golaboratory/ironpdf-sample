using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication3.Models;
using IronPdf;
using System.IO;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {

            var renderer = new IronPdf.HtmlToPdf()
            {
                PrintOptions = new PdfPrintOptions()
                {
                    FitToPaperWidth = true,
                    DPI = 300,
                    PaperSize = PdfPrintOptions.PdfPaperSize.A4,
                    PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait,
                    Zoom = 100,
                    EnableJavaScript = true,
                    InputEncoding = System.Text.Encoding.GetEncoding("UTF-8"),
                }
            };
            var path = System.IO.Path.GetTempFileName();

            renderer.RenderUrlAsPdf("https://www.yahoo.co.jp/").SaveAs(path);

            var stream = new System.IO.FileStream(path, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
