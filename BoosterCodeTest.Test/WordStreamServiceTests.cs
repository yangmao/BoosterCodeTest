using BoosterCodeTest.Hubs;
using BoosterCodeTest.Services;
using Microsoft.AspNetCore.SignalR;
using Moq;

namespace BoosterCodeTest.Test
{
    public class WordStreamServiceTests
    {
        IWordStreamService _wordStreamService;
        IWordStreamService _wordStreamService2;
        Mock<IHubContext<WordsHub>> _hubMock;
        string sentence;
        
        [SetUp]
        public void Setup()
        {
            _hubMock = new Mock<IHubContext<WordsHub>>();
            _wordStreamService = new WordStreamService();
            _wordStreamService2 = new WordStreamService(_hubMock.Object);
            sentence = "this is a test sentence.";
        }
        [Test]
        public void ProcessedWords()
        {
            Mock<IHubClients> mockClients = new Mock<IHubClients>();
            Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();
            mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);
            _hubMock.Setup(x => x.Clients).Returns(() => mockClients.Object);
            
            _wordStreamService2.ProcessedWords(sentence);
            mockClients.Verify(clients => clients.All, Times.Once);
        }
        [Test]
        public void SortCharectorByFrequency_Passin_Sentence_Return_Given_List_Chars()
        {
            var result = _wordStreamService.SortCharectorByFrequency(sentence);
            char[] expect = new char[8] { 't', 's', 'e', 'i', 'n', 'h', 'a', 'c' };
            Assert.AreEqual(result.Count, 8);
            Assert.AreEqual(expect,result);
        }

        [Test]
        public void SortStringByWordsLength_Passin_Sentence_Return_given_List_Chars()
        {
            var result = _wordStreamService.SortStringByWordsLength(sentence);
            string[] expect = new string[5] { "a", "is", "this", "test", "sentence" };
            Assert.AreEqual(result.Count, 5);
            Assert.AreEqual(expect, result);
        }

        [Test]
        public void Get10MostFrequentWords_Passin_Sentence_Return_given_List_Chars()
        {
            var result = _wordStreamService.GetMostFrequentWords(sentence,3);
            string[] expect = new string[3] { "this", "is", "a"};
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(expect, result);
        }

        [Test]
        public void Get3MostFrequentWords_Passin_Sentence_Return_Words_Number()
        {
            var result = _wordStreamService.CountWords(sentence);
            Assert.AreEqual(result, 5);
        }
    }
}