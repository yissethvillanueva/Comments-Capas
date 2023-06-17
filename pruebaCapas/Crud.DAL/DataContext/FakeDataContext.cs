using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Crud.DAL.DataContext;

public partial class FakeDataContext : DbContext
{
    public FakeDataContext()
    {
    }

    public FakeDataContext(DbContextOptions<FakeDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        => optionsBuilder.UseSqlServer("Server=(local); DataBase=FakeData; User Id= sa; Password= 123; Encrypt= false; TrustServerCertificate= true; Integrated Security=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Comments_Id");

            entity.ToTable("Comments", "WebSite");

            entity.Property(e => e.Attended)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(120)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
