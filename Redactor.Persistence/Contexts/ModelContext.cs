using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Redactor.Domain.Entities;

namespace Redactor.Persistence.Contexts;

public partial class ModelContext : DbContext
{
    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Smartlinks> Smartlinks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Smartlinks>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("smartlinks_pk");

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
