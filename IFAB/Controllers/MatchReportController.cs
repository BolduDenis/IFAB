using IFAB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using IFAB.Models;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace IFAB.Controllers
{
    public class MatchReportController : Controller
    {
        public ActionResult Random()
        {
            var matchReport = new MatchReport() { MatchId = 1};
            return View(matchReport);
        }

       
    }

 }

