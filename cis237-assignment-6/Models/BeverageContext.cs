using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace cis237_assignment_6.Models;

public partial class BeverageContext : DbContext
{
    public BeverageContext()
    {
    }

    public BeverageContext(DbContextOptions<BeverageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beverage> Beverages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=barnesbrothers.net;Database=BeverageMVandermyde;User Id=mvandermyde;Password=password;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beverage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Beverage__3213E83F9B0BC071");

            entity.Property(e => e.Name).IsFixedLength();
            entity.Property(e => e.Pack).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
