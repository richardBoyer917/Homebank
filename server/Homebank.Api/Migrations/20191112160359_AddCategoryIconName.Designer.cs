﻿// <auto-generated />
using System;
using Homebank.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Homebank.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191112160359_AddCategoryIconName")]
    partial class AddCategoryIconName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Homebank.Api.Domain.Entities.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Budgeted");

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("MonthForBudget");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("Homebank.Api.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("IconName");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Homebank.Api.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("Inflow");

                    b.Property<bool>("IsBankTransaction");

                    b.Property<bool>("IsInflowForBudgeting");

                    b.Property<string>("Memo");

                    b.Property<decimal>("OutFlow");

                    b.Property<string>("Payee");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Date", "Payee", "Memo", "Inflow", "OutFlow")
                        .IsUnique();

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Homebank.Api.Domain.Entities.Budget", b =>
                {
                    b.HasOne("Homebank.Api.Domain.Entities.Category", "Category")
                        .WithMany("Budgets")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("Homebank.Api.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Homebank.Api.Domain.Entities.Category", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CategoryId");
                });
#pragma warning restore 612, 618
        }
    }
}
