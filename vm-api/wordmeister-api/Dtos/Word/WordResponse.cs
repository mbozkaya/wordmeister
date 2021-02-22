using System;
using System.Collections.Generic;

namespace wordmeister_api.Dtos.Word
{
    public class WordResponse
    {
        public class Word
        {
            public long Id { get; set; }
            public string Text { get; set; }
            public string Description { get; set; }
            public ICollection<SentenceDto> Sentences { get; set; }
            public DateTime CreatedDate { get; set; }
        }
        
        public class WordCard
        {
            public long UserWordId { get; set; }
            public List<string> Sentences { get; set; }
            public string Word { get; set; }
            public string Description { get; set; }
            public bool IsOver { get; set; }
            public bool IsFavorite { get; set; }
            public byte Point { get; set; }
            public int WordCount { get; set; }
            public int CurrentIndex { get; set; }
        }
    }
}
