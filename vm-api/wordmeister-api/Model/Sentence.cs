using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace wordmeister_api.Model
{
    public class Sentence : BaseModel
    {
        public string Text { get; set; }
        public long WordId { get; set; }
        [ForeignKey("WordId")]
        public Word Word { get; set; }
    }
}
