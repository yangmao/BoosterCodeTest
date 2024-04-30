using Booster.CodingTest.Library;

namespace BoosterCodeTest.Services
{
    public interface IWordStreamService
    {
        public Task<byte[]> GetWordStream();
        public Task GetTotalNumberOfWords(string buffer);
        public Task GetTotalNumberOfCharactors(string words);
    }
}
