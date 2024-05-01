using Booster.CodingTest.Library;
using BoosterCodeTest.Hubs;
using BoosterCodeTest.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using NLipsum.Core;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace BoosterCodeTest.Services
{
    public class WordStreamService:IWordStreamService
    {
        private readonly string? _words;
        private WordStream? _stream;
        private readonly IHubContext<WordsHub>? _hubcontext;

        public WordStreamService() { }
        public WordStreamService(IHubContext<WordsHub> hubcontext) 
        { 
            _words = LipsumGenerator.Generate(1);
            _stream = new WordStream();
            _hubcontext = hubcontext;
        }

        public async Task<byte[]> GetWordStream() {

            byte[] buffer = Encoding.ASCII.GetBytes(_words);
            _stream.Read(buffer,0,64);
            return buffer;
        }

        public async Task ProcessedWords(string words)
        {
            var sortedWords = SortStringByWordsLength(words);
            var processedWords = new ProcessedWords()
            {
                NumberOfWords = CountWords(words),
                NumberOfCharectors = Regex.Matches(words, ".|").Count,
                SmallestWords = sortedWords.Take(5).ToList(),
                LargestWords = sortedWords.Skip(sortedWords.Count - 5).ToList(),
                MostFrequent10Words = GetMostFrequentWords(words,10),
                CharectorOrderbyFrequency = SortCharectorByFrequency(words)

            };
            await _hubcontext.Clients.All.SendAsync("RecieveMessage", processedWords);
        }

        public List<char> SortCharectorByFrequency(string sentence)
        {
            return Regex.Replace(sentence, "[^a-zA-Z0-9]", String.Empty)
                  .GroupBy(x => x)
                  .Select(x => new {
                      KeyField = x.Key,
                      Count = x.Count()
                  })
                  .OrderByDescending(x => x.Count)
                  .Select(x => x.KeyField).ToList();
        }
        
        public List<string> SortStringByWordsLength(string sentence)
        {
            var punctuation = sentence.Where(Char.IsPunctuation).Distinct().ToArray();
            var words = sentence.Split().Select(x => x.Trim(punctuation));
            return  words.Where(x=>x !="").OrderBy(x => x.Length).Distinct().ToList();
        }

        public List<string> GetMostFrequentWords(string sentence, int number)
        {
            return sentence
                  .Split(' ')
                  .GroupBy(x => x)
                  .Select(x => new {
                      KeyField = x.Key,
                      Count = x.Count()
                  })
                  .OrderByDescending(x => x.Count)
                  .Take(number).Select(x => x.KeyField).ToList();
        }
        public  int CountWords(string str)
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
