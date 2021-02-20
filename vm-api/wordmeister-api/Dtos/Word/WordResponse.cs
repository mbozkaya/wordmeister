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
            public int UserWordId { get; set; }
            public List<string> Sentences { get; set; }
            public string Word { get; set; }
            public string Description { get; set; }
        }
    }
}
