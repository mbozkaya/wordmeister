using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wordmeister_api.Model;

namespace wordmeister_api.Entity
{
    public class WordmeisterContext : DbContext
    {
        public WordmeisterContext(DbContextOptions<WordmeisterContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<UserWord> UserWords { get; set; }
        public DbSet<Sentence> Sentences { get; set; }

    }
}
