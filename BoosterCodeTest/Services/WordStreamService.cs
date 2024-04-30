using Booster.CodingTest.Library;
using BoosterCodeTest.Hubs;
using Microsoft.AspNetCore.SignalR;
using NLipsum.Core;
using System.Text;

namespace BoosterCodeTest.Services
{
    public class WordStreamService:IWordStreamService
    {
        private readonly string _words;
        private WordStream _stream;
        private readonly IHubContext<WordsHub> _hubcontext;
        public WordStreamService(IHubContext<WordsHub> hubcontext) 
        { 
            _words = LipsumGenerator.Generate(1);
            _stream = new WordStream();
            _hubcontext = hubcontext;
        }

        public async Task<byte[]> GetWordStream() {

            byte[] buffer = Encoding.ASCII.GetBytes(_words);
            await _stream.ReadAsync(buffer,0,64);
            return buffer;
        }

        public async Task GetTotalNumberOfWords(byte[] buffer)
        {
            var result = Encoding.Default.GetString(buffer);
            var number = CountWords(result);

            await _hubcontext.Clients.All.SendAsync("SendMessage","",number);
        }

        
        private int CountWords(string str)
        {
            if (str.Length == 0)
            {
                return 0;
            }
            int wordCount = 0;
            int state = 0;


            foreach (char c in str)
            {

                if (c == '\\')
                {
                    continue;
                }


                if (char.IsLetterOrDigit(c))
                {

                    if (state == 0)
                    {
                        wordCount++;
                        state = 1;
                    }
                }
                else
                {
                    state = 0;
                }
            }

            return wordCount;
        }
    }
}
