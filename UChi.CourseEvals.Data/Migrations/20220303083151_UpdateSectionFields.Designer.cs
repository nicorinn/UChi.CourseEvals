﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UChi.CourseEvals.Data;

#nullable disable

namespace UChi.CourseEvals.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220303083151_UpdateSectionFields")]
    partial class UpdateSectionFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UChi.CourseEvals.Domain.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("AverageSentiment")
                        .HasColumnType("double precision")
                        .HasColumnName("average_sentiment");

                    b.Property<string>("ChartData")
                        .HasColumnType("jsonb")
                        .HasColumnName("chart_data");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("department");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_courses");

                    b.ToTable("courses", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AverageSentiment = 0.69999999999999996,
                            ChartData = "{}",
                            Department = "CMSC",
                            Number = 16200,
                            Title = "Honors Introduction to Computer Science II"
                        });
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
                        .HasName("pk_professors");

                    b.ToTable("professors", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Fred Chong"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Hank Hoffmann"
                        });
                });

            modelBuilder.Entity("UChi.CourseEvals.Domain.Entities.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ChartData")
                        .HasColumnType("jsonb")
                        .HasColumnName("chart_data");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("integer")
                        .HasColumnName("professor_id");

                    b.Property<int>("Quarter")
                        .HasColumnType("integer")
                        .HasColumnName("quarter");

                    b.Property<double>("Sentiment")
                        .HasColumnType("double precision")
                        .HasColumnName("sentiment");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("Id")
                        .HasName("pk_sections");

                    b.HasIndex("CourseId")
                        .HasDatabaseName("ix_sections_course_id");

                    b.HasIndex("ProfessorId")
                        .HasDatabaseName("ix_sections_professor_id");

                    b.ToTable("sections", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChartData = "{}",
                            CourseId = 1,
                            Number = 1,
                            ProfessorId = 1,
                            Quarter = 1,
                            Sentiment = 0.80000000000000004,
                            Year = 2022
                        },
                        new
                        {
                            Id = 2,
                            ChartData = "{}",
                            CourseId = 1,
                            Number = 2,
                            ProfessorId = 2,
                            Quarter = 1,
                            Sentiment = 0.80000000000000004,
                            Year = 2022
                        });
                });

            modelBuilder.Entity("UChi.CourseEvals.Domain.Entities.Section", b =>
                {
                    b.HasOne("UChi.CourseEvals.Domain.Entities.Course", "Course")
                        .WithMany("Sections")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sections_courses_course_id");

                    b.HasOne("UChi.CourseEvals.Domain.Entities.Instructor", "Instructor")
                        .WithMany("Sections")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sections_professors_professor_id");

                    b.Navigation("Course");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("UChi.CourseEvals.Domain.Entities.Course", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("UChi.CourseEvals.Domain.Entities.Instructor", b =>
                {
                    b.Navigation("Sections");
                });
#pragma warning restore 612, 618
        }
    }
}
