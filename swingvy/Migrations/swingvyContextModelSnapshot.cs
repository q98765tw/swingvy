﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using swingvy.Models;

#nullable disable

namespace swingvy.Migrations
{
    [DbContext(typeof(swingvyContext))]
    partial class swingvyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("swingvy.Models.calendar", b =>
                {
                    b.Property<int>("calendar_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("calendar_id"));

                    b.Property<DateTime?>("endTime")
                        .HasColumnType("datetime");

                    b.Property<int>("member_id")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("startTime")
                        .HasColumnType("datetime");

                    b.HasKey("calendar_id")
                        .HasName("PK__calendar__584C1344A5072E3C");

                    b.HasIndex("member_id");

                    b.ToTable("calendar");
                });

            modelBuilder.Entity("swingvy.Models.leaveOrder", b =>
                {
                    b.Property<int>("leaveOrder_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("leaveOrder_id"));

                    b.Property<DateTime?>("applyTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("endTime")
                        .HasColumnType("datetime");

                    b.Property<int>("head")
                        .HasColumnType("int");

                    b.Property<int>("member_id")
                        .HasColumnType("int");

                    b.Property<string>("reason")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("startTime")
                        .HasColumnType("datetime");

                    b.Property<int>("state")
                        .HasColumnType("int");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("leaveOrder_id")
                        .HasName("PK__leaveOrd__18AB28A687EB1153");

                    b.HasIndex("member_id");

                    b.ToTable("leaveOrder");
                });

            modelBuilder.Entity("swingvy.Models.member", b =>
                {
                    b.Property<int>("member_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("member_id"));

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("account")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("member_id")
                        .HasName("PK__member__B29B8534A1B0EBBA");

                    b.ToTable("member");
                });

            modelBuilder.Entity("swingvy.Models.memberData", b =>
                {
                    b.Property<int>("memberData_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("memberData_id"));

                    b.Property<string>("email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("head")
                        .HasColumnType("int");

                    b.Property<string>("img_url")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("member_id")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("position")
                        .HasColumnType("int");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("memberData_id")
                        .HasName("PK__memberDa__5811618C385105A0");

                    b.HasIndex("member_id");

                    b.ToTable("memberData");
                });

            modelBuilder.Entity("swingvy.Models.worktime", b =>
                {
                    b.Property<int>("worktime_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("worktime_id"));

                    b.Property<DateTime?>("endTime")
                        .HasColumnType("datetime");

                    b.Property<int>("member_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("startTime")
                        .HasColumnType("datetime");

                    b.Property<int>("state")
                        .HasColumnType("int");

                    b.HasKey("worktime_id")
                        .HasName("PK__worktime__3593708F23BC1E5F");

                    b.HasIndex("member_id");

                    b.ToTable("worktime");
                });

            modelBuilder.Entity("swingvy.Models.calendar", b =>
                {
                    b.HasOne("swingvy.Models.member", "member")
                        .WithMany("calendar")
                        .HasForeignKey("member_id")
                        .IsRequired()
                        .HasConstraintName("FK__calendar__member__3F466844");

                    b.Navigation("member");
                });

            modelBuilder.Entity("swingvy.Models.leaveOrder", b =>
                {
                    b.HasOne("swingvy.Models.member", "member")
                        .WithMany("leaveOrder")
                        .HasForeignKey("member_id")
                        .IsRequired()
                        .HasConstraintName("FK__leaveOrde__membe__3C69FB99");

                    b.Navigation("member");
                });

            modelBuilder.Entity("swingvy.Models.memberData", b =>
                {
                    b.HasOne("swingvy.Models.member", "member")
                        .WithMany("memberData")
                        .HasForeignKey("member_id")
                        .IsRequired()
                        .HasConstraintName("FK__memberDat__membe__398D8EEE");

                    b.Navigation("member");
                });

            modelBuilder.Entity("swingvy.Models.worktime", b =>
                {
                    b.HasOne("swingvy.Models.member", "member")
                        .WithMany("worktime")
                        .HasForeignKey("member_id")
                        .IsRequired()
                        .HasConstraintName("FK__worktime__member__4222D4EF");

                    b.Navigation("member");
                });

            modelBuilder.Entity("swingvy.Models.member", b =>
                {
                    b.Navigation("calendar");

                    b.Navigation("leaveOrder");

                    b.Navigation("memberData");

                    b.Navigation("worktime");
                });
#pragma warning restore 612, 618
        }
    }
}
