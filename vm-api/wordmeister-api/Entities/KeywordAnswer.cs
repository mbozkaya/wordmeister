using System;
using System.Collections.Generic;

namespace wordmeister_api.Entities
{
    public partial class KeywordAnswer
    {
        public KeywordAnswer()
        {
            KeywordSessionAnswer = new HashSet<KeywordSessionAnswer>();
        }

        public int Id { get; set; }
        public int KeywordRegisterId { get; set; }
        public string Key { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual KeywordRegister KeywordRegister { get; set; }
        public virtual ICollection<KeywordSessionAnswer> KeywordSessionAnswer { get; set; }
    }
}
