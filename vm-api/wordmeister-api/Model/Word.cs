using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wordmeister_api.Model
{
    public class Word : BaseModel
    {
        public string Text { get; set; }
        private List<Word> sentences { get; set; }
        public virtual List<Word> Sentences
        {
            get
            {
                return sentences;
            }
            set
            {
                sentences = value;
            }
        }
    }
}
