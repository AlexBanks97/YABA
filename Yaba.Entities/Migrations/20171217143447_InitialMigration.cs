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
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FacebookId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                name: "Tabs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    UserOneId = table.Column<Guid>(nullable: true),
                    UserTwoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tabs_Users_UserOneId",
                        column: x => x.UserOneId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tabs_Users_UserTwoId",
                        column: x => x.UserTwoId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "TabItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CreateBy = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true),
                    TabEntityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabItems_Tabs_TabEntityId",
                        column: x => x.TabEntityId,
                        principalTable: "Tabs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_TabItems_TabEntityId",
                table: "TabItems",
                column: "TabEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_UserOneId",
                table: "Tabs",
                column: "UserOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_UserTwoId",
                table: "Tabs",
                column: "UserTwoId");
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
                name: "Tabs");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "BudgetGoals");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
