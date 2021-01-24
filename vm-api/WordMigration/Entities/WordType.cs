using System;
using System.Collections.Generic;

namespace WordMigration.Entities
{
    public partial class WordType
    {
        public WordType()
        {
            Word = new HashSet<Word>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Word> Word { get; set; }
    }
}
