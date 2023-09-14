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
                    .HasName("PK__calendar__584C13446FE779A6");

                entity.Property(e => e.endTime).HasColumnType("datetime");

                entity.Property(e => e.name).HasMaxLength(100);

                entity.Property(e => e.startTime).HasColumnType("datetime");

                entity.HasOne(d => d.member)
                    .WithMany(p => p.calendar)
                    .HasForeignKey(d => d.member_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__calendar__member__08B54D69");
            });

            modelBuilder.Entity<leaveOrder>(entity =>
            {
                entity.HasKey(e => e.leaveOrder_id)
                    .HasName("PK__leaveOrd__18AB28A67D5775D9");

                entity.Property(e => e.applyTime).HasColumnType("datetime");

                entity.Property(e => e.endTime).HasColumnType("datetime");

                entity.Property(e => e.reason).HasMaxLength(100);

                entity.Property(e => e.startTime).HasColumnType("datetime");

                entity.HasOne(d => d.member)
                    .WithMany(p => p.leaveOrder)
                    .HasForeignKey(d => d.member_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__leaveOrde__membe__05D8E0BE");
            });

            modelBuilder.Entity<member>(entity =>
            {
                entity.HasKey(e => e.member_id)
                    .HasName("PK__member__B29B8534571FCB67");

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
                    .HasName("PK__memberDa__5811618CA662025E");

                entity.Property(e => e.email).HasMaxLength(50);

                entity.Property(e => e.img_url).HasMaxLength(300);

                entity.Property(e => e.name).HasMaxLength(30);

                entity.Property(e => e.phone).HasMaxLength(15);

                entity.HasOne(d => d.member)
                    .WithMany(p => p.memberData)
                    .HasForeignKey(d => d.member_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__memberDat__membe__02FC7413");
            });

            modelBuilder.Entity<worktime>(entity =>
            {
                entity.HasKey(e => e.worktime_id)
                    .HasName("PK__worktime__3593708FEA84FB48");

                entity.Property(e => e.endTime).HasColumnType("datetime");

                entity.Property(e => e.startTime).HasColumnType("datetime");

                entity.HasOne(d => d.member)
                    .WithMany(p => p.worktime)
                    .HasForeignKey(d => d.member_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__worktime__member__0B91BA14");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}