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
    [Migration("20240513120013_VirtualChanges")]
    partial class VirtualChanges
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

                    b.HasIndex("user_id");

                    b.ToTable("Audit");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.CommentToEmployer", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("comment_stars")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("comment_text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("comment_title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("comments_to_employer_created_at")
                        .HasColumnType("datetime2");

                    b.Property<int>("employer_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("employer_id");

                    b.HasIndex("user_id");

                    b.ToTable("CommentToEmployer");
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

                    b.HasIndex("company_tags_list");

                    b.ToTable("CompanyTags");
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

                    b.ToTable("CompanyTagsList");
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

                    b.Property<int>("location_company_id")
                        .HasColumnType("int");

                    b.Property<int?>("number_workers")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("location_company_id");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Filters", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("filter_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("filter_type_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("filter_type_id");

                    b.ToTable("Filters");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.FiltersTypes", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("filter_type_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("FiltersTypes");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Industry", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("industry_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Industry");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Job", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<int?>("creater_user_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("date_approving")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date_ending")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("date_last_editing")
                        .HasColumnType("datetime2");

                    b.Property<int>("employer_id")
                        .HasColumnType("int");

                    b.Property<int>("industry_job_id")
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

                    b.Property<int?>("location_job_id")
                        .HasColumnType("int");

                    b.Property<int>("number_candidates")
                        .HasColumnType("int");

                    b.Property<int>("number_view")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("creater_user_id");

                    b.HasIndex("industry_job_id");

                    b.HasIndex("location_job_id");

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

                    b.HasIndex("job_id")
                        .IsUnique()
                        .HasFilter("[job_id] IS NOT NULL");

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

                    b.Property<int>("Jobid")
                        .HasColumnType("int");

                    b.Property<int>("job_id")
                        .HasColumnType("int");

                    b.Property<int?>("job_tags_pros_list")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Jobid");

                    b.HasIndex("job_tags_pros_list");

                    b.ToTable("JobTagsPros");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.JobTagsProsList", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("job_tags_pros_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("JobTagsProsList");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Location", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<string>("location_city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location_country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location_region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Locations");
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

            modelBuilder.Entity("Web_search_job.DatabaseClasses.UserInfo", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("id"), 1L, 1);

                    b.Property<DateTime>("action_created_at")
                        .HasColumnType("datetime2");

                    b.Property<int?>("profile_id")
                        .HasColumnType("int");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("UsersInfo");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Audit", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.CommentToEmployer", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.Employer", "Employer")
                        .WithMany("CommentToEmployer")
                        .HasForeignKey("employer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_search_job.DatabaseClasses.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employer");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.CompanyTags", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.CompanyTagsList", null)
                        .WithMany("CompanyTags")
                        .HasForeignKey("CompanyTagsListid");

                    b.HasOne("Web_search_job.DatabaseClasses.Employer", "Employer")
                        .WithMany("CompanyTags")
                        .HasForeignKey("Employerid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_search_job.DatabaseClasses.CompanyTags", "CompanyTagsList")
                        .WithMany()
                        .HasForeignKey("company_tags_list");

                    b.Navigation("CompanyTagsList");

                    b.Navigation("Employer");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Employer", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.Location", "Location")
                        .WithMany()
                        .HasForeignKey("location_company_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Filters", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.FiltersTypes", "FilterType")
                        .WithMany("Filters")
                        .HasForeignKey("filter_type_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FilterType");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Job", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("creater_user_id");

                    b.HasOne("Web_search_job.DatabaseClasses.Industry", "Industry")
                        .WithMany("Jobs")
                        .HasForeignKey("industry_job_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_search_job.DatabaseClasses.Location", "Location")
                        .WithMany("Jobs")
                        .HasForeignKey("location_job_id");

                    b.Navigation("Industry");

                    b.Navigation("Location");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.JobRequirement", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.Job", "Job")
                        .WithOne("JobRequirements")
                        .HasForeignKey("Web_search_job.DatabaseClasses.JobRequirement", "job_id");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.JobTagsMarks", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.Job", "Job")
                        .WithMany("JobTagsMarks")
                        .HasForeignKey("job_id");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.JobTagsPros", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.Job", "Job")
                        .WithMany("JobTagsPros")
                        .HasForeignKey("Jobid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_search_job.DatabaseClasses.JobTagsProsList", "JobTagsProsList")
                        .WithMany("JobTagsPros")
                        .HasForeignKey("job_tags_pros_list");

                    b.Navigation("Job");

                    b.Navigation("JobTagsProsList");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Report", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.Employer", "Employer")
                        .WithMany("Report")
                        .HasForeignKey("employer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_search_job.DatabaseClasses.Job", "Job")
                        .WithMany("Reports")
                        .HasForeignKey("job_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_search_job.DatabaseClasses.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employer");

                    b.Navigation("Job");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.SavedJob", b =>
                {
                    b.HasOne("Web_search_job.DatabaseClasses.Job", "Job")
                        .WithMany("SavedJobs")
                        .HasForeignKey("job_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_search_job.DatabaseClasses.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.CompanyTagsList", b =>
                {
                    b.Navigation("CompanyTags");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Employer", b =>
                {
                    b.Navigation("CommentToEmployer");

                    b.Navigation("CompanyTags");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.FiltersTypes", b =>
                {
                    b.Navigation("Filters");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Industry", b =>
                {
                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Job", b =>
                {
                    b.Navigation("JobRequirements");

                    b.Navigation("JobTagsMarks");

                    b.Navigation("JobTagsPros");

                    b.Navigation("Reports");

                    b.Navigation("SavedJobs");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.JobTagsProsList", b =>
                {
                    b.Navigation("JobTagsPros");
                });

            modelBuilder.Entity("Web_search_job.DatabaseClasses.Location", b =>
                {
                    b.Navigation("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
