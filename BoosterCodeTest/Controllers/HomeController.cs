using BoosterCodeTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoosterCodeTest.Controller
{
    public class HomeController : ControllerBase
    {
        private readonly IWordStreamService _wordStreamService;
        public HomeController(IWordStreamService wordStreamService)
        {
            _wordStreamService = wordStreamService;
        }
        public IActionResult Index()
        {
            return new ViewResult();
        }

        public IActionResult Start()
        {
            return new OkResult();
        }
    }
}
