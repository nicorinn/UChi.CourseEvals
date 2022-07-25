﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UChi.CourseEvals.Data;

#nullable disable

namespace UChi.CourseEvals.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InstructorSection", b =>
                {
                    b.Property<int>("InstructorsId")
                        .HasColumnType("integer")
                        .HasColumnName("instructors_id");

                    b.Property<int>("SectionsId")
                        .HasColumnType("integer")
                        .HasColumnName("sections_id");

                    b.HasKey("InstructorsId", "SectionsId")
                        .HasName("pk_instructor_section");

                    b.HasIndex("SectionsId")
                        .HasDatabaseName("ix_instructor_section_sections_id");

                    b.ToTable("instructor_section", (string)null);
                });

            modelBuilder.Entity("UChi.CourseEvals.Domain.Entities.ApiKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expiration_date");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("key");

                    b.Property<DateTime?>("LastUsed")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_used");

                    b.Property<long>("RequestCount")
                        .HasColumnType("bigint")
                        .HasColumnName("request_count");

                    b.Property<int>("Scope")
                        .HasColumnType("integer")
                        .HasColumnName("scope");

                    b.HasKey("Id")
                        .HasName("pk_api_keys");

                    b.ToTable("api_keys", (string)null);
                });

            modelBuilder.Entity("UChi.CourseEvals.Domain.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_courses");

                    b.ToTable("courses", (string)null);
                });

            modelBuilder.Entity("UChi.CourseEvals.Domain.Entities.CourseNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("department");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.HasKey("Id")
                        .HasName("pk_course_numbers");

                    b.HasIndex("CourseId")
                        .HasDatabaseName("ix_course_numbers_course_id");

                    b.ToTable("course_numbers", (string)null);
                });

            modelBuilder.Entity("UChi.CourseEvals.Domain.Entities.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_instructors");

                    b.ToTable("instructors", (string)null);
                });

            modelBuilder.Entity("UChi.CourseEvals.Domain.Entities.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    b.Property<int>("EnrolledCount")
                        .HasColumnType("integer")
                        .HasColumnName("enrolled_count");

                    b.Property<double?>("EvaluatedFairly")
                        .HasColumnType("double precision")
                        .HasColumnName("evaluated_fairly");

                    b.Property<double?>("HelpfulOutsideOfClass")
                        .HasColumnType("double precision")
                        .HasColumnName("helpful_outside_of_class");

                    b.Property<int?>("HoursWorked")
                        .HasColumnType("integer")
                        .HasColumnName("hours_worked");

                    b.Property<bool>("IsVirtual")
                        .HasColumnType("boolean")
                        .HasColumnName("is_virtual");

                    b.Property<string>("Keywords")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("jsonb")
                        .HasDefaultValue("[]")
                        .HasColumnName("keywords");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<int>("Quarter")
                        .HasColumnType("integer")
                        .HasColumnName("quarter");

                    b.Property<int>("RespondentCount")
                        .HasColumnType("integer")
                        .HasColumnName("respondent_count");

                    b.Property<double>("Sentiment")
                        .HasColumnType("double precision")
                        .HasColumnName("sentiment");

                    b.Property<double?>("StandardsForSuccess")
                        .HasColumnType("double precision")
                        .HasColumnName("standards_for_success");

                    b.Property<string>("Url")
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.Property<double?>("UsefulFeedback")
                        .HasColumnType("double precision")
                        .HasColumnName("useful_feedback");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("Id")
                        .HasName("pk_sections");

                    b.HasIndex("CourseId")
                        .HasDatabaseName("ix_sections_course_id");

                    b.ToTable("sections", (string)null);
                });

            modelBuilder.Entity("InstructorSection", b =>
                {
                    b.HasOne("UChi.CourseEvals.Domain.Entities.Instructor", null)
                        .WithMany()
                        .HasForeignKey("InstructorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_instructor_section_instructors_instructors_id");

                    b.HasOne("UChi.CourseEvals.Domain.Entities.Section", null)
                        .WithMany()
                        .HasForeignKey("SectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_instructor_section_sections_sections_id");
                });

            modelBuilder.Entity("UChi.CourseEvals.Domain.Entities.CourseNumber", b =>
                {
                    b.HasOne("UChi.CourseEvals.Domain.Entities.Course", "Course")
                        .WithMany("CourseNumbers")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_course_numbers_courses_course_id");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("UChi.CourseEvals.Domain.Entities.Section", b =>
                {
                    b.HasOne("UChi.CourseEvals.Domain.Entities.Course", "Course")
                        .WithMany("Sections")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sections_courses_course_id");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("UChi.CourseEvals.Domain.Entities.Course", b =>
                {
                    b.Navigation("CourseNumbers");

                    b.Navigation("Sections");
                });
#pragma warning restore 612, 618
        }
    }
}
