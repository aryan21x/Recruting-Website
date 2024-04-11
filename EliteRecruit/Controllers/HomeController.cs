using EliteRecruit.Interfaces;
using EliteRecruit.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EliteRecruit.Repository;
using EliteRecruit.ViewModels;

namespace EliteRecruit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentRepository _studentRepository;

        public HomeController(ILogger<HomeController> logger, IStudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var top5Students = await _studentRepository.GetTop5StudentsByGPA();
            var viewModel = new StudentViewModel { Top5Students = (List<Student>)top5Students };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
