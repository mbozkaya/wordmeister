using System;
using System.Collections.Generic;

namespace WordMigration.Entities
{
    public partial class KeywordSession
    {
        public KeywordSession()
        {
            KeywordSessionAnswer = new HashSet<KeywordSessionAnswer>();
        }

        public int Id { get; set; }
        public int KeywordRegisterId { get; set; }
        public int FirstUser { get; set; }
        public int SecondUser { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual User FirstUserNavigation { get; set; }
        public virtual KeywordRegister KeywordRegister { get; set; }
        public virtual User SecondUserNavigation { get; set; }
        public virtual ICollection<KeywordSessionAnswer> KeywordSessionAnswer { get; set; }
    }
}
