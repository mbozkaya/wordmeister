using System;

namespace wordmeister_api.Model
{
    public class UserWord
    {
        public long UserId { get; set; }
        public long WordId { get; set; }

        public string Description { get; set; }
        public bool Learned { get; set; }
        public bool Showed { get; set; }
        public byte Point { get; set; }

        public virtual User User { get; set; }
        public virtual Word Word { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; } 
    } 
}
