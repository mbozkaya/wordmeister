using System;
using System.Collections.Generic;

namespace WordMigration.Entities
{
    public partial class KeywordSessionAnswer
    {
        public int Id { get; set; }
        public int KeywordSessionId { get; set; }
        public int KeywordAnswerId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual KeywordAnswer KeywordAnswer { get; set; }
        public virtual KeywordSession KeywordSession { get; set; }
    }
}
