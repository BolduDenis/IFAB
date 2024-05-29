using Microsoft.AspNetCore.Mvc;
using IFAB.Models;
using System.Collections.Generic;
using System.Linq;


namespace IFAB.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            QuizViewModel quizViewModel = new QuizViewModel
            {
                Questions = new List<Questions>
                {

                    new Questions { Text = "What happens if a goalkeeper handles a backpass?", Answer = "" },
                    new Questions { Text = "What are the correct goal dimensions?", Answer = "" },
                    new Questions { Text = "All players must wear...", Answer = "" },
                    new Questions { Text = "What happens if a defender player plays the ball with the hand in the penalty area?", Answer = "" },
                    new Questions { Text = "What is the disciplinary sanction for a coach who is entering the field of play in an agressive manner?", Answer = "" },
                }
            };
            var viewModel = quizViewModel;
            return View(viewModel);

        }

        [HttpPost]
        public IActionResult Submit(QuizViewModel viewModel)
        {
            var correctAnswers = new List<string> { "Indirect free kick", "7.32 x 2.44", "Studden boots, shin guards and numbered shirts", "Penalty kick", "Red Card" };
            int score = 0;

            for (int i = 0; i < viewModel.Questions.Count; i++)
            {
                if (viewModel.Questions[i].Answer !=null &&  viewModel.Questions[i].Answer.Equals(correctAnswers[i], StringComparison.OrdinalIgnoreCase))
                {
                    score++;
                }
            }

            ViewBag.Score = score;
            ViewBag.Total = viewModel.Questions.Count;
            return View("Results");
        }
    }  
}