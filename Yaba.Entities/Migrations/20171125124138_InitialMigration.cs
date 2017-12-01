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
				name: "Goal",
				columns: table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Amount = table.Column<decimal>(nullable: false),
					Recurrence = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Goal", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Expense",
				columns: table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Amount = table.Column<decimal>(nullable: false),
					BudgetId = table.Column<Guid>(nullable: true),
					Name = table.Column<string>(maxLength: 50, nullable: false),
					Recurrence = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Expense", x => x.Id);
					table.ForeignKey(
						name: "FK_Expense_Budgets_BudgetId",
						column: x => x.BudgetId,
						principalTable: "Budgets",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Income",
				columns: table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Amount = table.Column<decimal>(nullable: false),
					BudgetId = table.Column<Guid>(nullable: true),
					Name = table.Column<string>(maxLength: 50, nullable: false),
					Recurrence = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Income", x => x.Id);
					table.ForeignKey(
						name: "FK_Income_Budgets_BudgetId",
						column: x => x.BudgetId,
						principalTable: "Budgets",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Category",
				columns: table => new
				{
					Id = table.Column<Guid>(nullable: false),
					BudgetId = table.Column<Guid>(nullable: true),
					GoalId = table.Column<Guid>(nullable: true),
					Name = table.Column<string>(maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Category", x => x.Id);
					table.ForeignKey(
						name: "FK_Category_Budgets_BudgetId",
						column: x => x.BudgetId,
						principalTable: "Budgets",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Category_Goal_GoalId",
						column: x => x.GoalId,
						principalTable: "Goal",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Entry",
				columns: table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Amount = table.Column<decimal>(nullable: false),
					CategoryId = table.Column<Guid>(nullable: true),
					Date = table.Column<DateTime>(nullable: false),
					Description = table.Column<string>(maxLength: 150, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Entry", x => x.Id);
					table.ForeignKey(
						name: "FK_Entry_Category_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Category",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Category_BudgetId",
				table: "Category",
				column: "BudgetId");

			migrationBuilder.CreateIndex(
				name: "IX_Category_GoalId",
				table: "Category",
				column: "GoalId",
				unique: true,
				filter: "[GoalId] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_Entry_CategoryId",
				table: "Entry",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_Expense_BudgetId",
				table: "Expense",
				column: "BudgetId");

			migrationBuilder.CreateIndex(
				name: "IX_Income_BudgetId",
				table: "Income",
				column: "BudgetId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Entry");

			migrationBuilder.DropTable(
				name: "Expense");

			migrationBuilder.DropTable(
				name: "Income");

			migrationBuilder.DropTable(
				name: "Category");

			migrationBuilder.DropTable(
				name: "Budgets");

			migrationBuilder.DropTable(
				name: "Goal");
		}
	}
}
