using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovieFan.Models
{
    public partial class moviefanContext : DbContext
    {
        public moviefanContext()
        {
        }

        public moviefanContext(DbContextOptions<moviefanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<UserLikeMovie> UserLikeMovie { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__categori__72E12F1BB371D7FB")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Movies>(entity =>
            {
                entity.ToTable("movies");

                entity.HasIndex(e => new { e.Title, e.Release })
                    .HasName("UQ_movie")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Picture)
                    .IsRequired()
                    .HasColumnName("picture")
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('default.jpg')");

                entity.Property(e => e.RatingId).HasColumnName("rating_id");

                entity.Property(e => e.Release)
                    .HasColumnName("release")
                    .HasColumnType("date");

                entity.Property(e => e.Synopsis)
                    .HasColumnName("synopsis")
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_category");

                entity.HasOne(d => d.Rating)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.RatingId)
                    .HasConstraintName("FK_rating");
            });

            modelBuilder.Entity<Ratings>(entity =>
            {
                entity.ToTable("ratings");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__ratings__72E12F1B87F4CBCF")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserLikeMovie>(entity =>
            {
                entity.ToTable("user_like_movie");

                entity.HasIndex(e => new { e.UserId, e.MovieId })
                    .HasName("UQ_like")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.HasSeen).HasColumnName("hasSeen");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.Stars).HasColumnName("stars");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.UserLikeMovie)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movie_like");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLikeMovie)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_like");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__users__AB6E616417AB46EB")
                    .IsUnique();

                entity.HasIndex(e => new { e.Firstname, e.Lastname })
                    .HasName("UQ_user")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
