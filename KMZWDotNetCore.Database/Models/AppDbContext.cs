using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KMZWDotNetCore.Database.Models;

public partial class AppDbContext : DbContext
{
  
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    } 

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    //private readonly SqlConnectionStringBuilder _sqlConnectionString = new SqlConnectionStringBuilder()
    //{
    //    DataSource = ".",
    //    InitialCatalog = "DotNetTrainingBatch5",
    //    UserID = "sa",
    //    Password = "sasa@123",
    //    TrustServerCertificate = true,

    //};

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseSqlServer(_sqlConnectionString.ConnectionString);
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogContent).IsUnicode(false);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
