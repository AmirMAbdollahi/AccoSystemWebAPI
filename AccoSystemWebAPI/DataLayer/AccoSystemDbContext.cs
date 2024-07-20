﻿using Microsoft.EntityFrameworkCore;

namespace AccoSystemWebAPI.DataLayer;

public partial class AccoSystemDbContext : DbContext
{
    public AccoSystemDbContext()
    {
    }

    public AccoSystemDbContext(DbContextOptions<AccoSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accounting> Accountings { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accounting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Accountion_PK");

            entity.ToTable("Accounting");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(800)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Accountings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Accountion_Customers_FK");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("Customers_PK");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Addrese)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
