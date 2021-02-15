using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wordmeister_api.Model
{
    public class UserInformation : BaseModel
    {
        public int UserId { get; set; }
        public byte FirstLanguage { get; set; }
        public byte SecondLanguage { get; set; }
        public virtual User User { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string Phone { get; set; }
        public string SlackToken { get; set; }
    }
}
