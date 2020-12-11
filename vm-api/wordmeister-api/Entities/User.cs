using System;
using System.Collections.Generic;

namespace wordmeister_api.Entities
{
    public partial class User
    {
        public User()
        {
            KeywordRegister = new HashSet<KeywordRegister>();
            KeywordSessionFirstUserNavigation = new HashSet<KeywordSession>();
            KeywordSessionSecondUserNavigation = new HashSet<KeywordSession>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid Guid { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<KeywordRegister> KeywordRegister { get; set; }
        public virtual ICollection<KeywordSession> KeywordSessionFirstUserNavigation { get; set; }
        public virtual ICollection<KeywordSession> KeywordSessionSecondUserNavigation { get; set; }
    }
}
