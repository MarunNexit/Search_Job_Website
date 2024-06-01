﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web_search_job.Data;

#nullable disable

namespace Web_search_job.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240508170501_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Job", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<int>("creater_user_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("date_approving")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date_ending")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("date_last_editing")
                        .HasColumnType("datetime2");

                    b.Property<int>("employer_id")
                        .HasColumnType("int");

                    b.Property<string>("industry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("job_background_img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("job_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("job_img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("job_industry_categoty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("job_salary_currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("job_salary_max")
                        .HasColumnType("int");

                    b.Property<int?>("job_salary_min")
                        .HasColumnType("int");

                    b.Property<int>("job_tags_marks_id")
                        .HasColumnType("int");

                    b.Property<int>("job_tags_pros_id")
                        .HasColumnType("int");

                    b.Property<string>("job_title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("number_candidates")
                        .HasColumnType("int");

                    b.Property<int>("number_view")
                        .HasColumnType("int");

                    b.Property<int?>("requirement_id")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
