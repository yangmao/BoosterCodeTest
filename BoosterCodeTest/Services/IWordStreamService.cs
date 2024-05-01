﻿using Booster.CodingTest.Library;

namespace BoosterCodeTest.Services
{
    public interface IWordStreamService
    {
        public Task<byte[]> GetWordStream();
        public Task ProcessedWords(string words);
    }
}
