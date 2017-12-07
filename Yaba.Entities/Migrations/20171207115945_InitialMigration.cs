using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yaba.Entities.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BudgetGoals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Recurrence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetGoals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabItemCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabItemCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tabs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BudgetCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BudgetEntityId = table.Column<Guid>(nullable: true),
                    GoalEntityId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetCategories_Budgets_BudgetEntityId",
                        column: x => x.BudgetEntityId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetCategories_BudgetGoals_GoalEntityId",
                        column: x => x.GoalEntityId,
                        principalTable: "BudgetGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BudgetRecurrings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    BudgetEntityId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Recurrence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetRecurrings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetRecurrings_Budgets_BudgetEntityId",
                        column: x => x.BudgetEntityId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    BudgetEntityId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Recurrence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseEntity_Budgets_BudgetEntityId",
                        column: x => x.BudgetEntityId,
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TabItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CategoryEntityId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(maxLength: 150, nullable: true),
                    TabEntityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabItems_TabItemCategories_CategoryEntityId",
                        column: x => x.CategoryEntityId,
                        principalTable: "TabItemCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TabItems_Tabs_TabEntityId",
                        column: x => x.TabEntityId,
                        principalTable: "Tabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CategoryEntityId = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetEntries_BudgetCategories_CategoryEntityId",
                        column: x => x.CategoryEntityId,
                        principalTable: "BudgetCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCategories_BudgetEntityId",
                table: "BudgetCategories",
                column: "BudgetEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCategories_GoalEntityId",
                table: "BudgetCategories",
                column: "GoalEntityId",
                unique: true,
                filter: "[GoalEntityId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetEntries_CategoryEntityId",
                table: "BudgetEntries",
                column: "CategoryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetRecurrings_BudgetEntityId",
                table: "BudgetRecurrings",
                column: "BudgetEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseEntity_BudgetEntityId",
                table: "ExpenseEntity",
                column: "BudgetEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TabItems_CategoryEntityId",
                table: "TabItems",
                column: "CategoryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TabItems_TabEntityId",
                table: "TabItems",
                column: "TabEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetEntries");

            migrationBuilder.DropTable(
                name: "BudgetRecurrings");

            migrationBuilder.DropTable(
                name: "ExpenseEntity");

            migrationBuilder.DropTable(
                name: "TabItems");

            migrationBuilder.DropTable(
                name: "BudgetCategories");

            migrationBuilder.DropTable(
                name: "TabItemCategories");

            migrationBuilder.DropTable(
                name: "Tabs");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "BudgetGoals");
        }
    }
}
