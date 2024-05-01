using Booster.CodingTest.Library;

namespace BoosterCodeTest.Services
{
    public interface IWordStreamService
    {
        public Task<byte[]> GetWordStream();
        public Task ProcessedWords(string words);
        public List<char> SortCharectorByFrequency(string sentence);
        public List<string> SortStringByWordsLength(string sentence);
        public List<string> GetMostFrequentWords(string sentence, int number);
        public int CountWords(string str);

    }
}
