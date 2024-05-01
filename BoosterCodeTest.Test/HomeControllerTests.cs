using BoosterCodeTest.Controller;
using BoosterCodeTest.Services;
using Moq;

namespace BoosterCodeTest.Test
{
    public class HomeControllerTests
    {
        Mock<IWordStreamService> _wordStreamServiceMock;

        [SetUp]
        public void Setup()
        {
            _wordStreamServiceMock = new Mock<IWordStreamService>();
            _wordStreamServiceMock.Setup(x => x.ProcessedWords(It.IsAny<string>()));
        }
        [Test]
        public void Start_Tests_Pass()
        {
            var controller = new HomeController(_wordStreamServiceMock.Object);
            controller.Start();
        }
    }
}
