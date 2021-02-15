using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wordmeister_api.Model
{
    public class UserSetting : BaseModel
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public byte Type { get; set; }
        public bool Enable { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
