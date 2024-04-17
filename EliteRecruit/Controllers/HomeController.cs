using EliteRecruit.Interfaces;
using EliteRecruit.Models;
using EliteRecruit.Models.Identity;
using EliteRecruit.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EliteRecruit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentRepository _studentRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager, ILogger<HomeController> logger, IStudentRepository studentRepository)
        {
            _userManager = userManager;
            _logger = logger;
            _studentRepository = studentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var top5Students = await _studentRepository.GetTop5StudentsByGPA();
            var viewModel = new StudentViewModel { Top5Students = (List<Student>)top5Students };
            return View(viewModel);
        }

        public async Task<IActionResult> UserInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        public async Task<IActionResult> Carousel()
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
