﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace KarmaApi.Models
{
    public partial class KarmaContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public KarmaContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public KarmaContext(DbContextOptions<KarmaContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Karma> Karmas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetSection("SqlConnection").Get<string>());
                // Use when running outside of docker
                // optionsBuilder.UseSqlServer("Server=localhost,5434;Database=Karma;User ID=sa;Password=Yukon900;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Karma>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Karma");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
