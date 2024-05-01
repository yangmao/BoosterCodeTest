namespace BoosterCodeTest.Models
{
    public class ProcessedWords
    {
        public int NumberOfWords { get; set; }
        public int NumberOfCharectors { get; set; }
        public  List<string> LargestWords { get; set; }
        public List<string> SmallestWords { get; set; }
        public List<string> MostFrequent10Words { get; set; }
        public List<char> CharectorOrderbyFrequency { get; set; }
    }
}
