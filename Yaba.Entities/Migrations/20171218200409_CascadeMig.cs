using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yaba.Entities.Migrations
{
    public partial class CascadeMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetCategories_Budgets_BudgetEntityId",
                table: "BudgetCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetCategories_Budgets_BudgetEntityId",
                table: "BudgetCategories",
                column: "BudgetEntityId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetCategories_Budgets_BudgetEntityId",
                table: "BudgetCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetCategories_Budgets_BudgetEntityId",
                table: "BudgetCategories",
                column: "BudgetEntityId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
