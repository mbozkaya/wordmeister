using System.Collections.Generic;

namespace wordmeister_api.Model
{
    public class Word : BaseModel
    {
        public string Text { get; set; }
        public virtual List<Sentence> Sentences { get; set; }
        public virtual ICollection<UserWord> UserWords { get; set; }
    }
}
