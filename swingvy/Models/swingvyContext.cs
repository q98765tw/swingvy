﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace swingvy.Models
{
    public partial class swingvyContext : DbContext
    {
        public swingvyContext()
        {
        }

        public swingvyContext(DbContextOptions<swingvyContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-T9SKD9C;Database=swingvy;Trusted_Connection=True;TrustServerCertificate=true;");
        }

        public virtual DbSet<calendar> calendar { get; set; }
        public virtual DbSet<leaveOrder> leaveOrder { get; set; }
        public virtual DbSet<member> member { get; set; }
        public virtual DbSet<memberData> memberData { get; set; }
        public virtual DbSet<worktime> worktime { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<calendar>(entity =>
            {
                entity.HasKey(e => e.calendar_id)
                    .HasName("PK__calendar__584C1344A5072E3C");

                entity.Property(e => e.endTime).HasColumnType("datetime");

                entity.Property(e => e.name).HasMaxLength(100);

                entity.Property(e => e.startTime).HasColumnType("datetime");

                entity.HasOne(d => d.member)
                    .WithMany(p => p.calendar)
                    .HasForeignKey(d => d.member_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__calendar__member__3F466844");
            });

            modelBuilder.Entity<leaveOrder>(entity =>
            {
                entity.HasKey(e => e.leaveOrder_id)
                    .HasName("PK__leaveOrd__18AB28A687EB1153");

                entity.Property(e => e.applyTime).HasColumnType("datetime");

                entity.Property(e => e.endTime).HasColumnType("datetime");

                entity.Property(e => e.reason).HasMaxLength(100);

                entity.Property(e => e.startTime).HasColumnType("datetime");

                entity.HasOne(d => d.member)
                    .WithMany(p => p.leaveOrder)
                    .HasForeignKey(d => d.member_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__leaveOrde__membe__3C69FB99");
            });

            modelBuilder.Entity<member>(entity =>
            {
                entity.HasKey(e => e.member_id)
                    .HasName("PK__member__B29B8534A1B0EBBA");

                entity.Property(e => e.account)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<memberData>(entity =>
            {
                entity.HasKey(e => e.memberData_id)
                    .HasName("PK__memberDa__5811618C385105A0");

                entity.Property(e => e.email).HasMaxLength(50);

                entity.Property(e => e.img_url).HasMaxLength(300);

                entity.Property(e => e.name).HasMaxLength(30);

                entity.Property(e => e.phone).HasMaxLength(15);

                entity.HasOne(d => d.member)
                    .WithMany(p => p.memberData)
                    .HasForeignKey(d => d.member_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__memberDat__membe__398D8EEE");
            });

            modelBuilder.Entity<worktime>(entity =>
            {
                entity.HasKey(e => e.worktime_id)
                    .HasName("PK__worktime__3593708F23BC1E5F");

                entity.Property(e => e.endTime).HasColumnType("datetime");

                entity.Property(e => e.startTime).HasColumnType("datetime");

                entity.HasOne(d => d.member)
                    .WithMany(p => p.worktime)
                    .HasForeignKey(d => d.member_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__worktime__member__4222D4EF");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}