using Microsoft.EntityFrameworkCore;
using wordmeister_api.Model;

namespace wordmeister_api.Entity
{
    public class WordmeisterContext : DbContext
    {
        public WordmeisterContext(DbContextOptions<WordmeisterContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserWord>().HasKey(e => new { e.UserId, e.WordId});
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<UserWord> UserWords { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<UploadFile> UploadFiles { get; set; }
    }
}
