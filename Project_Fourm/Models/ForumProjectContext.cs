using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Project_.Models;
using Project_Forum.Data;

namespace Project_Forum.Models;

public partial class ForumProjectContext : Project_ForumContext
{

  
    

    public ForumProjectContext(DbContextOptions<ForumProjectContext> options) : base(options)
    {
        
    }





    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostTag> PostTags { get; set; }

    public virtual DbSet<Respond> Responds { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("ConnectionString");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Posts__DD0C73BAE06A7BAE");

            entity.Property(e => e.PostId).HasColumnName("postID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PostContent)
                .HasColumnType("text")
                .HasColumnName("post_content");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPost");
        });

        modelBuilder.Entity<PostTag>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.PostId).HasColumnName("postID");
            entity.Property(e => e.TagName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tag_name");

            entity.HasOne(d => d.Post).WithMany()
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_PostTag_Post");

            entity.HasOne(d => d.TagNameNavigation).WithMany()
                .HasForeignKey(d => d.TagName)
                .HasConstraintName("FK_PostTag_Tag");
        });

        modelBuilder.Entity<Respond>(entity =>
        {
            entity.HasKey(e => e.RespondId).HasName("PK__Responds__BF073EE37B0CD52E");

            entity.Property(e => e.RespondId).HasColumnName("respondID");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PostId).HasColumnName("postID");
            entity.Property(e => e.RepondContent)
                .HasColumnType("text")
                .HasColumnName("repond_content");
            entity.Property(e => e.UserId).HasColumnName("userID");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagName).HasName("PK__Tags__E298655D46BAA930");

            entity.Property(e => e.TagName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tag_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CDF9402B66B");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.AvatarFilePath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("avatar_file_path");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .HasColumnName("email");
            entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
            entity.Property(e => e.Passwd)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("passwd");
            entity.Property(e => e.Username)
                .HasMaxLength(36)
                .HasColumnName("username");

            entity.HasMany(d => d.PostsNavigation).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "PostUpvote",
                    r => r.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PostUpvotes_Post"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PostUpvotes_User"),
                    j =>
                    {
                        j.HasKey("UserId", "PostId");
                        j.ToTable("PostUpvotes");
                        j.IndexerProperty<int>("UserId").HasColumnName("userID");
                        j.IndexerProperty<int>("PostId").HasColumnName("postID");
                    });

            entity.HasMany(d => d.Responds).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "RespondUpvote",
                    r => r.HasOne<Respond>().WithMany()
                        .HasForeignKey("RespondId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RespondUpvotes_Respond"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RepondUpvotes_User"),
                    j =>
                    {
                        j.HasKey("UserId", "RespondId");
                        j.ToTable("RespondUpvotes");
                        j.IndexerProperty<int>("UserId").HasColumnName("userID");
                        j.IndexerProperty<int>("RespondId").HasColumnName("respondID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
