using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;

namespace TagCloud
{
    public class SimpleWordsCounter: IWordsCounter
    {
        private readonly Dictionary<string, int> countedWords = new Dictionary<string, int>();

        public IReadOnlyDictionary<string, int> CountedWords => countedWords;

        public void UpdateWith(string text)
        {
            var words = text
                .ToLower() 
                .Split(" \t\n\r.,?!:;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
                IncOrAddToCounter(word);
        }

        private void IncOrAddToCounter(string word)
        {
            if (countedWords.ContainsKey(word))
                countedWords[word] += 1; 
            else
                countedWords[word] = 1;
        }
    }
}