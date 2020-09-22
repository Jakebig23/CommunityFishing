using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CommunityFishing.Models
{
    public partial class CommunityFishingContext : DbContext
    {
        public CommunityFishingContext()
        {
        }

        public CommunityFishingContext(DbContextOptions<CommunityFishingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FishInfo> FishInfo { get; set; }
        public virtual DbSet<FishRecipes> FishRecipes { get; set; }
        public virtual DbSet<PublicBlog> PublicBlog { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<Userfish> Userfish { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=CommunityFishing;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FishInfo>(entity =>
            {
                entity.HasKey(e => e.FishId)
                    .HasName("PK__FishInfo__F82A5BF9F105D1B0");

                entity.HasIndex(e => e.FishName)
                    .HasName("AK_FishName")
                    .IsUnique();

                entity.Property(e => e.FishId).HasColumnName("FishID");

                entity.Property(e => e.FishName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LegalLength)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LegalLimit)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.SeasonEnd).HasColumnType("datetime");

                entity.Property(e => e.SeasonStart).HasColumnType("datetime");
            });

            modelBuilder.Entity<FishRecipes>(entity =>
            {
                entity.HasKey(e => e.FishRecipeId)
                    .HasName("PK__FishReci__5659BC1E0E3089D6");

                entity.Property(e => e.FishRecipeId).HasColumnName("FishRecipeID");

                entity.Property(e => e.FishName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RecipePhotoPath)
                    .HasColumnName("RecipePhoto_Path")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.FishNameNavigation)
                    .WithMany(p => p.FishRecipes)
                    .HasPrincipalKey(p => p.FishName)
                    .HasForeignKey(d => d.FishName)
                    .HasConstraintName("FK__FishRecip__FishN__534D60F1");
            });

            modelBuilder.Entity<PublicBlog>(entity =>
            {
                entity.HasKey(e => e.BlogId)
                    .HasName("PK__PublicBl__54379E5017D48A50");

                entity.Property(e => e.BlogId).HasColumnName("BlogID");

                entity.Property(e => e.BlogDate)
                    .HasColumnName("blogDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.BlogTitle)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.UserFishId).HasColumnName("UserFishID");

                entity.HasOne(d => d.UserFish)
                    .WithMany(p => p.PublicBlog)
                    .HasForeignKey(d => d.UserFishId)
                    .HasConstraintName("FK__PublicBlo__UserF__5629CD9C");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.ProfileId)
                    .HasName("PK__UserProf__290C888404E2E913");

                entity.HasIndex(e => e.UserAccountId)
                    .HasName("AK_UserAccountId")
                    .IsUnique();

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.UserAccountId)
                    .IsRequired()
                    .HasMaxLength(1);
            });

            modelBuilder.Entity<Userfish>(entity =>
            {
                entity.Property(e => e.UserFishId).HasColumnName("UserFishID");

                entity.Property(e => e.CatchDate).HasColumnType("datetime");

                entity.Property(e => e.FishLength)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FishName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserAccountId).HasMaxLength(1);

                entity.Property(e => e.UserFishPhotoPath)
                    .HasColumnName("UserFishPhoto_Path")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.FishNameNavigation)
                    .WithMany(p => p.Userfish)
                    .HasPrincipalKey(p => p.FishName)
                    .HasForeignKey(d => d.FishName)
                    .HasConstraintName("FK__Userfish__FishNa__5070F446");

                entity.HasOne(d => d.UserAccount)
                    .WithMany(p => p.Userfish)
                    .HasPrincipalKey(p => p.UserAccountId)
                    .HasForeignKey(d => d.UserAccountId)
                    .HasConstraintName("FK__Userfish__UserAc__4F7CD00D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
