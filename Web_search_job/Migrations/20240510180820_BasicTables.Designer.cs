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
    [Migration("20240510180820_BasicTables")]
    partial class BasicTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Audit", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("action_created_at")
                        .HasColumnType("datetime2");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Audit");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.CityList", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("city_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("CityList");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.CompanyTags", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<int?>("CompanyTagsListid")
                        .HasColumnType("int");

                    b.Property<int>("Employerid")
                        .HasColumnType("int");

                    b.Property<int?>("JobTagsProsListid")
                        .HasColumnType("int");

                    b.Property<int?>("company_tags_list")
                        .HasColumnType("int");

                    b.Property<int>("employer_id")
                        .HasColumnType("int");

                    b.Property<string>("type_tag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("CompanyTagsListid");

                    b.HasIndex("Employerid");

                    b.HasIndex("JobTagsProsListid");

                    b.HasIndex("company_tags_list");

                    b.ToTable("CompanyTagsPros");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.CompanyTagsList", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("company_tags_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("CompanyTagsProsList");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Employer", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("company_background_img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("company_checked")
                        .HasColumnType("bit");

                    b.Property<string>("company_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("company_img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("company_industry_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("company_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("company_short_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("company_status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("company_url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("company_year_experience")
                        .HasColumnType("int");

                    b.Property<DateTime>("employer_created_at")
                        .HasColumnType("datetime2");

                    b.Property<int?>("number_workers")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.IndustryList", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("industry_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("IndustryList");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Job", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<int>("city_id")
                        .HasColumnType("int");

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

                    b.Property<int>("industry_id")
                        .HasColumnType("int");

                    b.Property<string>("job_background_img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("job_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("job_img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("job_salary_currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("job_salary_max")
                        .HasColumnType("int");

                    b.Property<int?>("job_salary_min")
                        .HasColumnType("int");

                    b.Property<string>("job_title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("number_candidates")
                        .HasColumnType("int");

                    b.Property<int>("number_view")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.JobRequirement", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("experience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("job_id")
                        .HasColumnType("int");

                    b.Property<string>("language_level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("language_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("JobRequirements");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.JobTagsMarks", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<int?>("job_id")
                        .HasColumnType("int");

                    b.Property<bool>("tag_hot")
                        .HasColumnType("bit");

                    b.Property<bool>("tag_new")
                        .HasColumnType("bit");

                    b.Property<bool>("tag_recommend")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("job_id");

                    b.ToTable("JobTagsMarks");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.JobTagsPros", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<int?>("tag_1_id")
                        .HasColumnType("int");

                    b.Property<int?>("tag_2_id")
                        .HasColumnType("int");

                    b.Property<int?>("tag_3_id")
                        .HasColumnType("int");

                    b.Property<int?>("tag_4_id")
                        .HasColumnType("int");

                    b.Property<int?>("tag_5_id")
                        .HasColumnType("int");

                    b.Property<int?>("tag_6_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("JobTagsPros");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.JobTagsProsList", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("company_tags_pros_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("JobTagsProsList");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Report", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<int>("employer_id")
                        .HasColumnType("int");

                    b.Property<int>("job_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("report_created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("report_target")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("employer_id");

                    b.HasIndex("job_id");

                    b.HasIndex("user_id");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.SavedJob", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<int>("job_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("saved_job_created_at")
                        .HasColumnType("datetime2");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("job_id");

                    b.HasIndex("user_id");

                    b.ToTable("SavedJobs");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.User", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<DateTime>("action_created_at")
                        .HasColumnType("datetime2");

                    b.Property<int?>("id_usernfo")
                        .HasColumnType("int");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.CompanyTags", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.CompanyTagsList", null)
                        .WithMany("CompanyTagsPros")
                        .HasForeignKey("CompanyTagsListid");

                    b.HasOne("Web_search_job.DatabaseClasses.Employer", "Employer")
                        .WithMany("CompanyTags")
                        .HasForeignKey("Employerid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_search_job.DatabaseClasses.JobTagsProsList", null)
                        .WithMany("CompanyTagsPros")
                        .HasForeignKey("JobTagsProsListid");

                    b.HasOne("Web_search_job.DatabaseClasses.CompanyTags", "CompanyTagsList")
                        .WithMany()
                        .HasForeignKey("company_tags_list");

                    b.Navigation("CompanyTagsList");

                    b.Navigation("Employer");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.JobTagsMarks", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.Job", "Job")
                        .WithMany("JobTagsMarks")
                        .HasForeignKey("job_id");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Report", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.Employer", "Employer")
                        .WithMany("Report")
                        .HasForeignKey("employer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_search_job.DatabaseClasses.Job", "Job")
                        .WithMany("Report")
                        .HasForeignKey("job_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_search_job.DatabaseClasses.User", "User")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employer");

                    b.Navigation("Job");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.SavedJob", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.Job", "Job")
                        .WithMany("SavedJobs")
                        .HasForeignKey("job_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_search_job.DatabaseClasses.User", "User")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.CompanyTagsList", b =>
                {
                    b.Navigation("CompanyTagsPros");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Employer", b =>
                {
                    b.Navigation("CompanyTags");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Job", b =>
                {
                    b.Navigation("JobTagsMarks");

                    b.Navigation("Report");

                    b.Navigation("SavedJobs");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.JobTagsProsList", b =>
                {
                    b.Navigation("CompanyTagsPros");
                });
#pragma warning restore 612, 618
        }
    }
}
