using System.Collections.Generic;

namespace wordmeister_api.Dtos.Word
{
    public class WordResponse
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public ICollection<SentenceDto> Sentences { get; set; }
    }
}
