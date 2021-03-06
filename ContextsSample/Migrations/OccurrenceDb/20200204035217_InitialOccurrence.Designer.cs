﻿// <auto-generated />
using System;
using ContextsSample.Modules.Occurrences.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContextsSample.Migrations.OccurrenceDb
{
    [DbContext(typeof(OccurrenceDbContext))]
    [Migration("20200204035217_InitialOccurrence")]
    partial class InitialOccurrence
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContextsSample.Modules.Occurrences.Models.OccurrenceModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("CreatedDate")
                        .HasColumnType("DATETIME2(7)");

                    b.Property<string>("Description")
                        .HasColumnName("Description")
                        .HasColumnType("NVARCHAR(500)")
                        .HasMaxLength(500);

                    b.Property<Guid>("What")
                        .HasColumnName("What")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("Who")
                        .HasColumnName("Who")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.HasKey("Id");

                    b.ToTable("Occurrence","many_contexts");
                });
#pragma warning restore 612, 618
        }
    }
}
