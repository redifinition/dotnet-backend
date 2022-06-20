using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class DataWarehouseContext : DbContext
    {
        public DataWarehouseContext()
        {
        }

        public DataWarehouseContext(DbContextOptions<DataWarehouseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActorActor> ActorActors { get; set; } = null!;
        public virtual DbSet<ActorMovie> ActorMovies { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<DirectorMovie> DirectorMovies { get; set; } = null!;
        public virtual DbSet<Format> Formats { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieScore> MovieScores { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Time> Times { get; set; } = null!;
        public virtual DbSet<TimeMovie> TimeMovies { get; set; } = null!;
        public virtual DbSet<ViewActorActor> ViewActorActors { get; set; } = null!;
        public virtual DbSet<ViewActorCooperationTime> ViewActorCooperationTimes { get; set; } = null!;
        public virtual DbSet<ViewActorDirector> ViewActorDirectors { get; set; } = null!;
        public virtual DbSet<ViewActorDirectorCooperationTime> ViewActorDirectorCooperationTimes { get; set; } = null!;
        public virtual DbSet<ViewActorName> ViewActorNames { get; set; } = null!;
        public virtual DbSet<ViewCategoryName> ViewCategoryNames { get; set; } = null!;
        public virtual DbSet<ViewDirectorCooperationTime> ViewDirectorCooperationTimes { get; set; } = null!;
        public virtual DbSet<ViewDirectorName> ViewDirectorNames { get; set; } = null!;
        public virtual DbSet<ViewMovieFact> ViewMovieFacts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("name=AmazonDataWarehouse:MySQLConnectionString", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<ActorActor>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("actor_actor");

                entity.Property(e => e.FirstActorName)
                    .HasMaxLength(255)
                    .HasColumnName("first_actor_name");

                entity.Property(e => e.MovieCount).HasColumnName("movie_count");

                entity.Property(e => e.SecondActorName)
                    .HasMaxLength(255)
                    .HasColumnName("second_actor_name");
            });

            modelBuilder.Entity<ActorMovie>(entity =>
            {
                entity.HasKey(e => new { e.ActorName, e.MovieId, e.IsMainActor })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("actor_movie");

                entity.HasIndex(e => e.ActorName, "actor_name");

                entity.HasIndex(e => e.MovieId, "movie_id3");

                entity.Property(e => e.ActorName).HasColumnName("actor_name");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.IsMainActor).HasColumnName("is_main_actor");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.ActorMovies)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("movie_id3");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => new { e.CategoryName, e.MovieId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("category");

                entity.HasIndex(e => e.CategoryName, "category_name");

                entity.HasIndex(e => e.MovieId, "movie_id4");

                entity.Property(e => e.CategoryName).HasColumnName("category_name");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("movie_id4");
            });

            modelBuilder.Entity<DirectorMovie>(entity =>
            {
                entity.HasKey(e => new { e.DirectorName, e.MovieId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("director_movie");

                entity.HasIndex(e => e.MovieId, "movie_id");

                entity.Property(e => e.DirectorName).HasColumnName("director_name");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.DirectorMovies)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("movie_id");
            });

            modelBuilder.Entity<Format>(entity =>
            {
                entity.ToTable("format");

                entity.Property(e => e.FormatId)
                    .ValueGeneratedNever()
                    .HasColumnName("format_id");

                entity.Property(e => e.FormatName)
                    .HasMaxLength(255)
                    .HasColumnName("format_name");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movie");

                entity.HasIndex(e => e.FormatId, "format_id");

                entity.HasIndex(e => e.TimeId, "time_id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.CommentNum).HasColumnName("comment_num");

                entity.Property(e => e.FormatId).HasColumnName("format_id");

                entity.Property(e => e.MovieAsin)
                    .HasMaxLength(255)
                    .HasColumnName("movie_asin")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.MovieEdition)
                    .HasMaxLength(255)
                    .HasColumnName("movie_edition")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.MovieEditionNum)
                    .HasColumnName("movie_edition_num")
                    .HasComment("电影的版本数量");

                entity.Property(e => e.MovieName)
                    .HasMaxLength(500)
                    .HasColumnName("movie_name");

                entity.Property(e => e.MovieScore).HasColumnName("movie_score");

                entity.Property(e => e.TimeId).HasColumnName("time_id");

                entity.Property(e => e.TimeStr)
                    .HasColumnType("datetime")
                    .HasColumnName("time_str");

                entity.HasOne(d => d.Format)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.FormatId)
                    .HasConstraintName("format_id");

                entity.HasOne(d => d.Time)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.TimeId)
                    .HasConstraintName("time_id");
            });

            modelBuilder.Entity<MovieScore>(entity =>
            {
                entity.HasKey(e => e.MovieId)
                    .HasName("PRIMARY");

                entity.ToTable("movie_score");

                entity.HasIndex(e => e.MovieScore1, "movie_score");

                entity.HasIndex(e => e.PositiveCommentRating, "positive");

                entity.Property(e => e.MovieId)
                    .ValueGeneratedNever()
                    .HasColumnName("movie_id");

                entity.Property(e => e.MovieScore1).HasColumnName("movie_score");

                entity.Property(e => e.NegativeCommentRating).HasColumnName("negative_comment_rating");

                entity.Property(e => e.PositiveCommentRating).HasColumnName("positive_comment_rating");

                entity.HasOne(d => d.Movie)
                    .WithOne(p => p.MovieScoreNavigation)
                    .HasForeignKey<MovieScore>(d => d.MovieId)
                    .HasConstraintName("movie_id11");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => new { e.ReviewId, e.MovieId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("review");

                entity.HasIndex(e => e.MovieId, "movie_id1");

                entity.Property(e => e.ReviewId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("review_id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.Helpfulness).HasColumnName("helpfulness");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.MovieAsin)
                    .HasMaxLength(255)
                    .HasColumnName("movie_asin");

                entity.Property(e => e.ReviewScore)
                    .HasPrecision(10)
                    .HasColumnName("review_score");

                entity.Property(e => e.ReviewSummary)
                    .HasMaxLength(255)
                    .HasColumnName("review_summary");

                entity.Property(e => e.ReviewText).HasColumnName("review_text");

                entity.Property(e => e.ReviewTime).HasColumnName("review_time");

                entity.Property(e => e.ReviewerName)
                    .HasMaxLength(255)
                    .HasColumnName("reviewer_name");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("movie_id1");
            });

            modelBuilder.Entity<Time>(entity =>
            {
                entity.ToTable("time");

                entity.HasIndex(e => new { e.Year, e.Month, e.Day, e.Season, e.Weekday }, "compositeIndex");

                entity.Property(e => e.TimeId).HasColumnName("time_id");

                entity.Property(e => e.Day).HasColumnName("day");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Season).HasColumnName("season");

                entity.Property(e => e.TimeStr)
                    .HasColumnType("datetime")
                    .HasColumnName("time_str");

                entity.Property(e => e.Weekday).HasColumnName("weekday");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<TimeMovie>(entity =>
            {
                entity.HasKey(e => e.MovieId)
                    .HasName("PRIMARY");

                entity.ToTable("time_movie");

                entity.Property(e => e.MovieId)
                    .ValueGeneratedNever()
                    .HasColumnName("movie_id");

                entity.Property(e => e.TimeStr)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("time_str");

                entity.HasOne(d => d.Movie)
                    .WithOne(p => p.TimeMovie)
                    .HasForeignKey<TimeMovie>(d => d.MovieId)
                    .HasConstraintName("movie_id10");
            });

            modelBuilder.Entity<ViewActorActor>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_actor_actor");

                entity.Property(e => e.Actor1)
                    .HasMaxLength(255)
                    .HasColumnName("actor1");

                entity.Property(e => e.Actor2)
                    .HasMaxLength(255)
                    .HasColumnName("actor2");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");
            });

            modelBuilder.Entity<ViewActorCooperationTime>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_actor_cooperation_time");

                entity.Property(e => e.ActorName1)
                    .HasMaxLength(255)
                    .HasColumnName("actor_name1");

                entity.Property(e => e.ActorName2)
                    .HasMaxLength(255)
                    .HasColumnName("actor_name2");

                entity.Property(e => e.CooperTime).HasColumnName("cooper_time");
            });

            modelBuilder.Entity<ViewActorDirector>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_actor_director");

                entity.Property(e => e.ActorName)
                    .HasMaxLength(255)
                    .HasColumnName("actor_name");

                entity.Property(e => e.DirectorName)
                    .HasMaxLength(255)
                    .HasColumnName("director_name");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");
            });

            modelBuilder.Entity<ViewActorDirectorCooperationTime>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_actor_director_cooperation_time");

                entity.Property(e => e.ActorName)
                    .HasMaxLength(255)
                    .HasColumnName("actor_name");

                entity.Property(e => e.DirectorName)
                    .HasMaxLength(255)
                    .HasColumnName("director_name");

                entity.Property(e => e.MovieCount).HasColumnName("movie_count");
            });

            modelBuilder.Entity<ViewActorName>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_actor_name");

                entity.Property(e => e.ActorName)
                    .HasMaxLength(255)
                    .HasColumnName("actor_name");
            });

            modelBuilder.Entity<ViewCategoryName>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_category_name");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .HasColumnName("category_name");
            });

            modelBuilder.Entity<ViewDirectorCooperationTime>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_director_cooperation_time");

                entity.Property(e => e.FirstDirectorName)
                    .HasMaxLength(255)
                    .HasColumnName("first_director_name");

                entity.Property(e => e.MovieCount).HasColumnName("movie_count");

                entity.Property(e => e.SecondDirectorName)
                    .HasMaxLength(255)
                    .HasColumnName("second_director_name");
            });

            modelBuilder.Entity<ViewDirectorName>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("view_director_name");

                entity.Property(e => e.DirectorName)
                    .HasMaxLength(255)
                    .HasColumnName("director_name");
            });

            modelBuilder.Entity<ViewMovieFact>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_movie_fact");

                entity.Property(e => e.ActorName)
                    .HasMaxLength(255)
                    .HasColumnName("actor_name");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .HasColumnName("category_name");

                entity.Property(e => e.CommentNum).HasColumnName("comment_num");

                entity.Property(e => e.Day).HasColumnName("day");

                entity.Property(e => e.DirectorName)
                    .HasMaxLength(255)
                    .HasColumnName("director_name");

                entity.Property(e => e.FormatName)
                    .HasMaxLength(255)
                    .HasColumnName("format_name");

                entity.Property(e => e.IsMainActor).HasColumnName("is_main_actor");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.MovieAsin)
                    .HasMaxLength(255)
                    .HasColumnName("movie_asin")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.MovieEdition)
                    .HasMaxLength(255)
                    .HasColumnName("movie_edition")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.MovieName)
                    .HasMaxLength(500)
                    .HasColumnName("movie_name");

                entity.Property(e => e.MovieScore).HasColumnName("movie_score");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
