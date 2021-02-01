using System;
using System.Collections.Generic;

namespace wordmeister_api.Model
{
    public class User :BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid Guid { get; set; }
        public bool Status { get; set; }
        
        public virtual ICollection<UserWord> UserWords{ get; set; } 
    }
}
