using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wordmeister_api.Dtos.Word
{
    public class WordApiResponse
    {
        public class BaseClass
        {
            public string Word { get; set; }
        }
        public class ExampleDto : BaseClass
        {
            public List<string> Examples { get; set; }
        }

        public class SynonymsDto : BaseClass
        {
            public List<string> Synonyms { get; set; }
        }

        public class DefinationsDto : BaseClass
        {
            public Definations Definations { get; set; } = new Definations();

        }

        public class Definations
        {
            public string Defination { get; set; }
            public string PartOfSpeech { get; set; }
        }

        public class AntonymsDto : BaseClass
        {
            public List<string> Antonyms { get; set; }

        }

        public class RyhmesDto : BaseClass
        {
            public Ryhmes Ryhmes { get; set; } = new Ryhmes();
        }

        public class Ryhmes
        {
            public List<string> All { get; set; }
        }

        public class PronunciationDto : BaseClass
        {
            public Pronunciation Pronunciation { get; set; }
        }

        public class Pronunciation
        {
            public string All { get; set; }
            public string Noun { get; set; }
            public string Verb { get; set; }
        }

        public class SyllablesDto : BaseClass
        {
            public Syllables Syllables { get; set; } = new Syllables();
        }

        public class Syllables
        {
            public int Count { get; set; }
            public List<string> List { get; set; }
        }

        public class FrequencyDto : BaseClass
        {
            public Frequency Frequency { get; set; } = new Frequency();
        }

        public class Frequency
        {
            public decimal Zipf { get; set; }
            public decimal PerMillion { get; set; }
            public decimal Diversity { get; set; }
        }

        public class WordDto : BaseClass
        {
            public Syllables Syllables { get; set; } = new Syllables();
            public Pronunciation Pronunciation { get; set; } = new Pronunciation();
            public decimal Frequency { get; set; }
            public List<Results> Results { get; set; } = new List<Results>();
        }

        public class Results : Definations
        {
            public List<string> Synonyms { get; set; }
            public List<string> TypeOf { get; set; }
            public List<string> HasTypes { get; set; }
            public List<string> Antonyms { get; set; }
            public List<string> Examples { get; set; }

        }
    }
}
