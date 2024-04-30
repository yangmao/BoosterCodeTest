using BoosterCodeTest.Hubs;
using BoosterCodeTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;


namespace BoosterCodeTest.Controller
{
    public class HomeController : ControllerBase
    {
        private readonly IWordStreamService _wordStreamService;
        private readonly IHubContext<WordsHub> _hubcontext;
        private byte[] _streamBuffer;
        public HomeController(IWordStreamService wordStreamService, IHubContext<WordsHub> hubcontext)
        {
            _wordStreamService = wordStreamService;
            _streamBuffer  = new byte[64];
            _hubcontext = hubcontext;
        }
        public IActionResult Index()
        {
            return new ViewResult();
        }

        public async Task<IActionResult> Start()
        {
            _streamBuffer = await _wordStreamService.GetWordStream();
            await _hubcontext.Clients.All.SendAsync("SendMessage", "d", "dd");
            await _wordStreamService.GetTotalNumberOfWords(_streamBuffer);
            return Ok(_streamBuffer);
        }
    }
}
