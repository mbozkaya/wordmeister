using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace wordmeister_api.Entities
{
    public partial class WordMeisterDbContext : DbContext
    {
        public WordMeisterDbContext()
        {
        }

        public WordMeisterDbContext(DbContextOptions<WordMeisterDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<KeywordAnswer> KeywordAnswer { get; set; }
        public virtual DbSet<KeywordRegister> KeywordRegister { get; set; }
        public virtual DbSet<KeywordSession> KeywordSession { get; set; }
        public virtual DbSet<KeywordSessionAnswer> KeywordSessionAnswer { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=localhost;initial catalog=WordMeister;User Id=sa;Password=!wordmeister123;MultipleActiveResultSets=True;App=EntityFramework");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KeywordAnswer>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.OriginalKey)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.KeywordRegister)
                    .WithMany(p => p.KeywordAnswer)
                    .HasForeignKey(d => d.KeywordRegisterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KeywordAnswer_KeywordRegister");
            });

            modelBuilder.Entity<KeywordRegister>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.KeywordRegister)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KeywordRegister_User");
            });

            modelBuilder.Entity<KeywordSession>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FirstUserNavigation)
                    .WithMany(p => p.KeywordSessionFirstUserNavigation)
                    .HasForeignKey(d => d.FirstUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KeywordSession_User");

                entity.HasOne(d => d.KeywordRegister)
                    .WithMany(p => p.KeywordSession)
                    .HasForeignKey(d => d.KeywordRegisterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KeywordSession_KeywordRegister");

                entity.HasOne(d => d.SecondUserNavigation)
                    .WithMany(p => p.KeywordSessionSecondUserNavigation)
                    .HasForeignKey(d => d.SecondUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KeywordSession_User1");
            });

            modelBuilder.Entity<KeywordSessionAnswer>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.KeywordAnswer)
                    .WithMany(p => p.KeywordSessionAnswer)
                    .HasForeignKey(d => d.KeywordAnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KeywordSessionAnswer_KeywordAnswer");

                entity.HasOne(d => d.KeywordSession)
                    .WithMany(p => p.KeywordSessionAnswer)
                    .HasForeignKey(d => d.KeywordSessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KeywordSessionAnswer_KeywordSession");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
