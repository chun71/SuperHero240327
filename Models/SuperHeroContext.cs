using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperHero240327.Models;

public partial class SuperHeroContext : DbContext
{
    public SuperHeroContext(DbContextOptions<SuperHeroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Character> Character { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Characte__3214EC27B8ACF1D6");

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Place).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
