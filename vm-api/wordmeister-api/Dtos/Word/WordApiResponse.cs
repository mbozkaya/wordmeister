using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wordmeister_api.Dtos.Word
{
    public class WordApiResponse
    {
        public class ExampleDto
        {
            public string Word { get; set; }
            public List<string> Examples { get; set; }
        }
    }
}
