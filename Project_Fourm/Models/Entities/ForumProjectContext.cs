﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Project_Forum.Models.Entities;

public partial class ForumProjectContext : DbContext
{
    public ForumProjectContext()
    {
    }

    public ForumProjectContext(DbContextOptions<ForumProjectContext> options)
        : base(options)
    {
    }
    public DbSet<IdentityUserClaim<string>> AspNetUserClaims { get; set; }

    public virtual DbSet<ApplicationUser> AspNetUsers { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostUpvote> PostUpvotes { get; set; }

    public virtual DbSet<Respond> Responds { get; set; }

    public virtual DbSet<RespondUpvote> RespondUpvotes { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();


        optionsBuilder.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.DateOfBirth).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Posts__DD0C73BA42313553");

            entity.Property(e => e.PostId).HasColumnName("postID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PostContent)
                .HasColumnType("text")
                .HasColumnName("post_content");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPost");

            entity.HasMany(d => d.TagNames).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagName")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PostTag_Tag"),
                    l => l.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PostTag_Post"),
                    j =>
                    {
                        j.HasKey("PostId", "TagName");
                        j.ToTable("PostTags");
                        j.IndexerProperty<int>("PostId").HasColumnName("postID");
                        j.IndexerProperty<string>("TagName")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("tag_name");
                    });
        });

        modelBuilder.Entity<PostUpvote>(entity =>
        {
            entity.HasKey(e => e.PostUpvotesId).HasName("PK__PostUpvo__BF22F81D150F14B4");

            entity.HasIndex(e => new { e.PostId, e.UserId }, "unique_PostID_userID").IsUnique();

            entity.Property(e => e.PostUpvotesId).HasColumnName("PostUpvotesID");
            entity.Property(e => e.PostId).HasColumnName("postID");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Post).WithMany(p => p.PostUpvotes)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostUpvotes_Post");

            entity.HasOne(d => d.User).WithMany(p => p.PostUpvotes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostUpvotes_User");
        });

        modelBuilder.Entity<Respond>(entity =>
        {
            entity.HasKey(e => e.RespondId).HasName("PK__Responds__BF073EE303BBCE66");

            entity.Property(e => e.RespondId).HasColumnName("respondID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PostId).HasColumnName("postID");
            entity.Property(e => e.RepondContent)
                .HasColumnType("text")
                .HasColumnName("repond_content");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("userID");

            entity.HasOne(d => d.Post).WithMany(p => p.Responds)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RespondPost");

            entity.HasOne(d => d.User).WithMany(p => p.Responds)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRespond");
        });

        modelBuilder.Entity<RespondUpvote>(entity =>
        {
            entity.HasKey(e => e.RespondUpvotesId).HasName("PK__RespondU__99FD0050C29CE358");

            entity.HasIndex(e => new { e.RespondId, e.UserId }, "unique_respondID_userID").IsUnique();

            entity.Property(e => e.RespondUpvotesId).HasColumnName("RespondUpvotesID");
            entity.Property(e => e.RespondId).HasColumnName("respondID");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Respond).WithMany(p => p.RespondUpvotes)
                .HasForeignKey(d => d.RespondId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RespondUpvotes_Respond");

            entity.HasOne(d => d.User).WithMany(p => p.RespondUpvotes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RepondUpvotes_User");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagName).HasName("PK__Tags__E298655D73145147");

            entity.Property(e => e.TagName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tag_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}