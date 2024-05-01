using BoosterCodeTest.Models;
using Microsoft.AspNetCore.SignalR;

namespace BoosterCodeTest.Hubs
{
    public class WordsHub:Hub
    {
        public async Task Notify(ProcessedWords processedWords)
        { 
            await Clients.All.SendAsync("RecieveMessage", processedWords);
        }
    }
}
