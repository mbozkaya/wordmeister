using System;
using System.Collections.Generic;

namespace WordMigration.Entities
{
    public partial class Word
    {
        public int Id { get; set; }
        public int WordTypeId { get; set; }
        public string Text { get; set; }
        public string TranslatedText { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual WordType WordType { get; set; }
    }
}
