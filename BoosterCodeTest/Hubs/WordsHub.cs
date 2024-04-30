using Microsoft.AspNetCore.SignalR;

namespace BoosterCodeTest.Hubs
{
    public class WordsHub:Hub
    {
        public async Task SendMessage(string label)
        { 
            await Clients.All.SendAsync("RecieveMessage",label);
        }
    }
}
