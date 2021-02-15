using Microsoft.EntityFrameworkCore;
using wordmeister_api.Model;

namespace wordmeister_api.Entity
{
    public partial class WordmeisterContext : DbContext
    {
        public WordmeisterContext(DbContextOptions<WordmeisterContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserWord>().HasKey(e => new { e.UserId, e.WordId});
            OnModelCreatingPartial(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<UserWord> UserWords { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<UploadFile> UploadFiles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<UserInformation> UserInformations { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
    }
}
