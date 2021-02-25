﻿using System;
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
            public bool IsLearned { get; set; }
            public List<Definations> Definations { get; set; }
            public Prononciation Prononciations { get; set; }
            public decimal Frequency { get; set; }
        }

        public class Definations
        {
            public long Id { get; set; }
            public string Defination { get; set; }
            public string Type { get; set; }

        }

        public class Prononciation
        {
            public string Verb { get; set; }
            public string Noun { get; set; }
            public string All { get; set; }
        }

        public class UserWordSetting
        {
            public bool IsIncludedLearned { get; set; }
            public bool IsIncludedFavorite { get; set; }
            public bool IsIncludedPoint { get; set; }
            public decimal Point { get; set; }
            public string Order { get; set; }
            public string OrderBy { get; set; }
        }
    }
}
