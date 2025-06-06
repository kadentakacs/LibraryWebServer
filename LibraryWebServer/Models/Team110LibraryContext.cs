﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebServer.Models;

public partial class Team110LibraryContext : DbContext
{
    public Team110LibraryContext()
    {
    }

    public Team110LibraryContext(DbContextOptions<Team110LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CheckedOut> CheckedOut { get; set; }

    public virtual DbSet<Inventory> Inventory { get; set; }

    public virtual DbSet<Patrons> Patrons { get; set; }

    public virtual DbSet<Phones> Phones { get; set; }

    public virtual DbSet<Titles> Titles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=atr.eng.utah.edu;user id=u1267912;password=pwd;database=Team110Library", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.8-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<CheckedOut>(entity =>
        {
            entity.HasKey(e => e.Serial).HasName("PRIMARY");

            entity.HasIndex(e => e.CardNum, "CardNum");

            entity.Property(e => e.Serial)
                .ValueGeneratedNever()
                .HasColumnType("int(10) unsigned");
            entity.Property(e => e.CardNum).HasColumnType("int(10) unsigned");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Serial).HasName("PRIMARY");

            entity.HasIndex(e => e.Isbn, "ISBN");

            entity.Property(e => e.Serial).HasColumnType("int(10) unsigned");
            entity.Property(e => e.Isbn)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("ISBN");
        });

        modelBuilder.Entity<Patrons>(entity =>
        {
            entity.HasKey(e => e.CardNum).HasName("PRIMARY");

            entity.Property(e => e.CardNum).HasColumnType("int(10) unsigned");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Phones>(entity =>
        {
            entity.HasKey(e => new { e.CardNum, e.Phone })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.CardNum).HasColumnType("int(10) unsigned");
            entity.Property(e => e.Phone)
                .HasMaxLength(8)
                .IsFixedLength();
        });

        modelBuilder.Entity<Titles>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PRIMARY");

            entity.Property(e => e.Isbn)
                .HasMaxLength(14)
                .IsFixedLength()
                .HasColumnName("ISBN");
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
