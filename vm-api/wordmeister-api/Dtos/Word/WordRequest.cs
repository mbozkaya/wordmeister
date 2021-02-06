using System.Collections.Generic;

namespace wordmeister_api.Dtos.Word
{
    public class IdDto
    {
        public long Id { get; set; }
    }
    public class WordRequest : IdDto
    {
        public string Text { get; set; }
        public string Description { get; set; }
    }

    public class IdListDto
    {
        public List<long> Ids { get; set; }
    }
}
