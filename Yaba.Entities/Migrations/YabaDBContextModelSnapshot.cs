﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Yaba.Common;
using Yaba.Entities;

namespace Yaba.Entities.Migrations
{
    [DbContext(typeof(YabaDBContext))]
    partial class YabaDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Yaba.Entities.Budget.BudgetEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("Yaba.Entities.Budget.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BudgetEntityId");

                    b.Property<Guid?>("GoalEntityId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("BudgetEntityId");

                    b.HasIndex("GoalEntityId")
                        .IsUnique()
                        .HasFilter("[GoalEntityId] IS NOT NULL");

                    b.ToTable("BudgetCategories");
                });

            modelBuilder.Entity("Yaba.Entities.Budget.EntryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid?>("CategoryEntityId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.HasIndex("CategoryEntityId");

                    b.ToTable("BudgetEntries");
                });

            modelBuilder.Entity("Yaba.Entities.Budget.ExpenseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid?>("BudgetEntityId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Recurrence");

                    b.HasKey("Id");

                    b.HasIndex("BudgetEntityId");

                    b.ToTable("ExpenseEntity");
                });

            modelBuilder.Entity("Yaba.Entities.Budget.GoalEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<int>("Recurrence");

                    b.HasKey("Id");

                    b.ToTable("BudgetGoals");
                });

            modelBuilder.Entity("Yaba.Entities.Budget.RecurringEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid?>("BudgetEntityId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Recurrence");

                    b.HasKey("Id");

                    b.HasIndex("BudgetEntityId");

                    b.ToTable("BudgetRecurrings");
                });

            modelBuilder.Entity("Yaba.Entities.Tab.ItemEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("Description")
                        .HasMaxLength(150);

                    b.Property<Guid>("TabEntityId");

                    b.HasKey("Id");

                    b.HasIndex("TabEntityId");

                    b.ToTable("TabItems");
                });

            modelBuilder.Entity("Yaba.Entities.Tab.TabEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance");

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.ToTable("Tabs");
                });

            modelBuilder.Entity("Yaba.Entities.Budget.CategoryEntity", b =>
                {
                    b.HasOne("Yaba.Entities.Budget.BudgetEntity", "BudgetEntity")
                        .WithMany("Categories")
                        .HasForeignKey("BudgetEntityId");

                    b.HasOne("Yaba.Entities.Budget.GoalEntity", "GoalEntity")
                        .WithOne("CategoryEntity")
                        .HasForeignKey("Yaba.Entities.Budget.CategoryEntity", "GoalEntityId");
                });

            modelBuilder.Entity("Yaba.Entities.Budget.EntryEntity", b =>
                {
                    b.HasOne("Yaba.Entities.Budget.CategoryEntity", "CategoryEntity")
                        .WithMany("Entries")
                        .HasForeignKey("CategoryEntityId");
                });

            modelBuilder.Entity("Yaba.Entities.Budget.ExpenseEntity", b =>
                {
                    b.HasOne("Yaba.Entities.Budget.BudgetEntity", "BudgetEntity")
                        .WithMany("Expenses")
                        .HasForeignKey("BudgetEntityId");
                });

            modelBuilder.Entity("Yaba.Entities.Budget.RecurringEntity", b =>
                {
                    b.HasOne("Yaba.Entities.Budget.BudgetEntity", "BudgetEntity")
                        .WithMany("Recurrings")
                        .HasForeignKey("BudgetEntityId");
                });

            modelBuilder.Entity("Yaba.Entities.Tab.ItemEntity", b =>
                {
                    b.HasOne("Yaba.Entities.Tab.TabEntity", "TabEntity")
                        .WithMany("TabItems")
                        .HasForeignKey("TabEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
