using Microsoft.AspNetCore.SignalR;

namespace BoosterCodeTest.Hubs
{
    public class WordsHub:Hub
    {
        public async Task Send(string topic, string message)
        { 
            await Clients.All.SendAsync(topic,message);
        }
    }
}
