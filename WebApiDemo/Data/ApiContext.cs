using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApiDemo.Models;

namespace WebApiDemo.Data;

public partial class ApiContext : DbContext
{
    public ApiContext()
    {
    }

    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<TitleAuthor> TitleAuthors { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=API; Persist Security Info=True; User ID=sa; Password=123456; Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("address");
            entity.Property(e => e.AuFname)
                .HasMaxLength(10)
                .HasColumnName("au_fname");
            entity.Property(e => e.AuLname)
                .HasMaxLength(50)
                .HasColumnName("au_lname");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.PubName)
                .HasMaxLength(50)
                .HasColumnName("pub_name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.Notes)
                .HasMaxLength(200)
                .HasColumnName("notes");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.PubId).HasColumnName("pub_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title1)
                .HasMaxLength(150)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.Pub).WithMany(p => p.Titles)
                .HasForeignKey(d => d.PubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Titles_Publishers");
        });

        modelBuilder.Entity<TitleAuthor>(entity =>
        {
            entity.ToTable("TitleAuthor");

            entity.HasIndex(e => new { e.TitleId, e.AuthorId, e.PubDate }, "IX_TitleAuthor").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PubDate)
                .HasColumnType("datetime")
                .HasColumnName("pub_date");
            entity.Property(e => e.Royaltyper).HasColumnName("royaltyper");
            entity.Property(e => e.Sort).HasColumnName("sort");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TitleId).HasColumnName("title_id");

            entity.HasOne(d => d.Author).WithMany(p => p.TitleAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitleAuthor_Authors");

            entity.HasOne(d => d.Title).WithMany(p => p.TitleAuthors)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitleAuthor_Titles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
