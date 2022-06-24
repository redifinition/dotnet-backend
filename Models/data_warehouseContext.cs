using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataWarehouse.Models
{
    public partial class data_warehouseContext : DbContext
    {
        public data_warehouseContext()
        {
        }

        public data_warehouseContext(DbContextOptions<data_warehouseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TCleaningMovie> TCleaningMovies { get; set; } = null!;
        public virtual DbSet<TComment> TComments { get; set; } = null!;
        public virtual DbSet<TConsolidationMovie> TConsolidationMovies { get; set; } = null!;
        public virtual DbSet<TFatherAsin> TFatherAsins { get; set; } = null!;
        public virtual DbSet<TMovieTvAsin> TMovieTvAsins { get; set; } = null!;
        public virtual DbSet<TMovieWithRemovingTv> TMovieWithRemovingTvs { get; set; } = null!;
        public virtual DbSet<TMovieWithoutRemovingTvAsin> TMovieWithoutRemovingTvAsins { get; set; } = null!;
        public virtual DbSet<VMissingMovieTv> VMissingMovieTvs { get; set; } = null!;
        public virtual DbSet<VTvAsin> VTvAsins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("name=AmazonDataWarehouse:MySQLTraceabilityQueries", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<TCleaningMovie>(entity =>
            {
                entity.HasKey(e => e.Asin)
                    .HasName("PRIMARY");

                entity.ToTable("t_cleaning_movie");

                entity.Property(e => e.Asin)
                    .HasMaxLength(20)
                    .HasColumnName("asin");

                entity.Property(e => e.Actor)
                    .HasMaxLength(512)
                    .HasColumnName("actor");

                entity.Property(e => e.CommentNum).HasColumnName("comment_num");

                entity.Property(e => e.DbRaringScore).HasColumnName("db_raring_score");

                entity.Property(e => e.Director)
                    .HasMaxLength(512)
                    .HasColumnName("director");

                entity.Property(e => e.MainActor)
                    .HasMaxLength(512)
                    .HasColumnName("main_actor");

                entity.Property(e => e.MovieCategory)
                    .HasMaxLength(255)
                    .HasColumnName("movie_category");

                entity.Property(e => e.MovieEdition)
                    .HasMaxLength(255)
                    .HasColumnName("movie_edition");

                entity.Property(e => e.MovieFormat)
                    .HasMaxLength(255)
                    .HasColumnName("movie_format");

                entity.Property(e => e.MovieReleaseDate).HasColumnName("movie_release_date");

                entity.Property(e => e.MovieScore).HasColumnName("movie_score");

                entity.Property(e => e.MovieTitle)
                    .HasMaxLength(512)
                    .HasColumnName("movie_title");

                entity.Property(e => e.NearAsin)
                    .HasMaxLength(255)
                    .HasColumnName("near_asin");
            });

            modelBuilder.Entity<TComment>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PRIMARY");

                entity.ToTable("t_comment");

                entity.HasIndex(e => e.Asin, "comment_asin_index");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.Asin)
                    .HasMaxLength(20)
                    .HasColumnName("asin");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Overall).HasColumnName("overall");

                entity.Property(e => e.ReviewText).HasColumnName("review_text");

                entity.Property(e => e.ReviewTime)
                    .HasMaxLength(100)
                    .HasColumnName("review_time");

                entity.Property(e => e.ReviewerId)
                    .HasMaxLength(50)
                    .HasColumnName("reviewer_id");

                entity.Property(e => e.ReviewerName)
                    .HasMaxLength(1000)
                    .HasColumnName("reviewer_name");

                entity.Property(e => e.Style)
                    .HasMaxLength(1000)
                    .HasColumnName("style");

                entity.Property(e => e.Summary)
                    .HasMaxLength(1000)
                    .HasColumnName("summary");

                entity.Property(e => e.UnixReviewTime)
                    .HasMaxLength(50)
                    .HasColumnName("unix_review_time");

                entity.Property(e => e.Verified)
                    .HasMaxLength(10)
                    .HasColumnName("verified");

                entity.Property(e => e.Vote)
                    .HasMaxLength(100)
                    .HasColumnName("vote");
            });

            modelBuilder.Entity<TConsolidationMovie>(entity =>
            {
                entity.HasKey(e => e.Asin)
                    .HasName("PRIMARY");

                entity.ToTable("t_consolidation_movie");

                entity.Property(e => e.Asin)
                    .HasMaxLength(20)
                    .HasColumnName("asin");

                entity.Property(e => e.Actor)
                    .HasMaxLength(512)
                    .HasColumnName("actor");

                entity.Property(e => e.AsinCount).HasColumnName("asin_count");

                entity.Property(e => e.CommentNum).HasColumnName("comment_num");

                entity.Property(e => e.DbRaringScore)
                    .HasMaxLength(255)
                    .HasColumnName("db_raring_score");

                entity.Property(e => e.Director)
                    .HasMaxLength(512)
                    .HasColumnName("director");

                entity.Property(e => e.MainActor)
                    .HasMaxLength(512)
                    .HasColumnName("main_actor");

                entity.Property(e => e.MovieCategory)
                    .HasMaxLength(255)
                    .HasColumnName("movie_category");

                entity.Property(e => e.MovieEdition)
                    .HasMaxLength(255)
                    .HasColumnName("movie_edition");

                entity.Property(e => e.MovieFormat)
                    .HasMaxLength(255)
                    .HasColumnName("movie_format");

                entity.Property(e => e.MovieReleaseDate).HasColumnName("movie_release_date");

                entity.Property(e => e.MovieScore).HasColumnName("movie_score");

                entity.Property(e => e.MovieTitle)
                    .HasMaxLength(512)
                    .HasColumnName("movie_title");
            });

            modelBuilder.Entity<TFatherAsin>(entity =>
            {
                entity.HasKey(e => e.Asin)
                    .HasName("PRIMARY");

                entity.ToTable("t_father_asin");

                entity.Property(e => e.Asin)
                    .HasMaxLength(20)
                    .HasColumnName("asin");

                entity.Property(e => e.FatherAsin)
                    .HasMaxLength(20)
                    .HasColumnName("father_asin");
            });

            modelBuilder.Entity<TMovieTvAsin>(entity =>
            {
                entity.HasKey(e => e.Asin)
                    .HasName("PRIMARY");

                entity.ToTable("t_movie_tv_asin");

                entity.Property(e => e.Asin)
                    .HasMaxLength(20)
                    .HasColumnName("asin");
            });

            modelBuilder.Entity<TMovieWithRemovingTv>(entity =>
            {
                entity.HasKey(e => e.Asin)
                    .HasName("PRIMARY");

                entity.ToTable("t_movie_with_removing_tv");

                entity.Property(e => e.Asin)
                    .HasMaxLength(20)
                    .HasColumnName("asin");

                entity.Property(e => e.Actor)
                    .HasMaxLength(512)
                    .HasColumnName("actor");

                entity.Property(e => e.CommentNum).HasColumnName("comment_num");

                entity.Property(e => e.DbRaringScore).HasColumnName("db_raring_score");

                entity.Property(e => e.Director)
                    .HasMaxLength(512)
                    .HasColumnName("director");

                entity.Property(e => e.MainActor)
                    .HasMaxLength(512)
                    .HasColumnName("main_actor");

                entity.Property(e => e.MovieCategory)
                    .HasMaxLength(255)
                    .HasColumnName("movie_category");

                entity.Property(e => e.MovieEdition)
                    .HasMaxLength(255)
                    .HasColumnName("movie_edition");

                entity.Property(e => e.MovieFormat)
                    .HasMaxLength(255)
                    .HasColumnName("movie_format");

                entity.Property(e => e.MovieReleaseDate)
                    .HasMaxLength(255)
                    .HasColumnName("movie_release_date");

                entity.Property(e => e.MovieScore).HasColumnName("movie_score");

                entity.Property(e => e.MovieTitle)
                    .HasMaxLength(512)
                    .HasColumnName("movie_title");

                entity.Property(e => e.NearAsin)
                    .HasMaxLength(255)
                    .HasColumnName("near_asin");
            });

            modelBuilder.Entity<TMovieWithoutRemovingTvAsin>(entity =>
            {
                entity.HasKey(e => e.Asin)
                    .HasName("PRIMARY");

                entity.ToTable("t_movie_without_removing_tv_asin");

                entity.Property(e => e.Asin)
                    .HasMaxLength(20)
                    .HasColumnName("asin");
            });

            modelBuilder.Entity<VMissingMovieTv>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_missing_movie_tv");

                entity.Property(e => e.Asin)
                    .HasMaxLength(20)
                    .HasColumnName("asin");
            });

            modelBuilder.Entity<VTvAsin>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_tv_asin");

                entity.Property(e => e.Asin)
                    .HasMaxLength(20)
                    .HasColumnName("asin");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
