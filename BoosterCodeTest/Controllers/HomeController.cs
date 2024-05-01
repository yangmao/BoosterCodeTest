using BoosterCodeTest.Hubs;
using BoosterCodeTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text;
using System;


namespace BoosterCodeTest.Controller
{
    public class HomeController : ControllerBase
    {
        private readonly IWordStreamService _wordStreamService;
        private byte[] _streamBuffer;
        public HomeController(IWordStreamService wordStreamService)
        {
            _wordStreamService = wordStreamService;
            _streamBuffer  = new byte[64];
        }
        public IActionResult Index()
        {
            return new ViewResult();
        }

        public async Task<IActionResult> Start()
        {
            _streamBuffer = await _wordStreamService.GetWordStream();
            var result = Encoding.Default.GetString(_streamBuffer);
            await _wordStreamService.ProcessedWords(result);
            return Ok(_streamBuffer);
        }
    }
}
