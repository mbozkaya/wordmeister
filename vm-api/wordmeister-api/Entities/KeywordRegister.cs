using System;
using System.Collections.Generic;

namespace wordmeister_api.Entities
{
    public partial class KeywordRegister
    {
        public KeywordRegister()
        {
            KeywordAnswer = new HashSet<KeywordAnswer>();
            KeywordSession = new HashSet<KeywordSession>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<KeywordAnswer> KeywordAnswer { get; set; }
        public virtual ICollection<KeywordSession> KeywordSession { get; set; }
    }
}
