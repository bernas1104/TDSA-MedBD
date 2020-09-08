﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TDSA_MedBDInfra.Context;

namespace TDSA_MedBDDomain.Migrations
{
    [ExcludeFromCodeCoverage]
    [DbContext(typeof(TDSAMedBDContext))]
    partial class TDSAMedBDContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TDSAMedBDDomain.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("character varying(11)")
                        .HasMaxLength(11);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Crm")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnName("deleted_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Cpf");

                    b.HasAlternateKey("Crm");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("TDSAMedBDDomain.Models.DoctorSpecialty", b =>
                {
                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<int>("SpecialtyId")
                        .HasColumnType("integer");

                    b.HasKey("DoctorId", "SpecialtyId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("DoctorSpecialties");
                });

            modelBuilder.Entity("TDSAMedBDDomain.Models.Specialty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnName("deleted_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Specialties");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Clínico Geral",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("TDSAMedBDDomain.Models.DoctorSpecialty", b =>
                {
                    b.HasOne("TDSAMedBDDomain.Models.Doctor", "Doctor")
                        .WithMany("Specialties")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TDSAMedBDDomain.Models.Specialty", "Specialty")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
