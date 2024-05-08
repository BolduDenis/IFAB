using Microsoft.AspNetCore.Mvc;

namespace IFAB.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Quiz()
        {
            return View();
        }
    }
}
