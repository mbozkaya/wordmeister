using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace wordmeister_api.Model
{
    public class UserWord:BaseModel
    {
        public long UserId { get; set; }
        public long WordId { get; set; }
        public string Description { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("WordId")]
        public virtual Word Word { get; set; }
    }
}
