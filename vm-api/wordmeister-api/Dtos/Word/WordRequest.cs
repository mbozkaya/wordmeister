using System.Collections.Generic;

namespace wordmeister_api.Dtos.Word
{
    public class IdDto
    {
        public long Id { get; set; }
    }
    public class WordRequest : IdDto
    {
        public class Add : IdDto
        {
            public string Text { get; set; }
            public string Description { get; set; }
        }

        public class WordCard
        {
            public int CurrentIndex { get; set; } = 1;
            public bool IsRandom { get; set; }
        }

        public class WordPoint
        {
            public int UserWordId { get; set; }
            public int Point { get; set; }
        }

        public class WordFavorite
        {
            public int UserWordId { get; set; }
            public bool IsFavorite { get; set; }
        }

        public class Learned
        {
            public int UserWordId { get; set; }
            public bool IsLearned { get; set; }
        }

        public class CustomSentence
        {
            public int UserWordId { get; set; }
            public string Sentence { get; set; }
            public bool IsPrivate { get; set; }
        }
    }

    public class IdListDto
    {
        public List<long> Ids { get; set; }
    }
}
