using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yaba.Entities.Migrations
{
    public partial class CascadeMig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetEntries_BudgetCategories_CategoryEntityId",
                table: "BudgetEntries");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetEntries_BudgetCategories_CategoryEntityId",
                table: "BudgetEntries",
                column: "CategoryEntityId",
                principalTable: "BudgetCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetEntries_BudgetCategories_CategoryEntityId",
                table: "BudgetEntries");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetEntries_BudgetCategories_CategoryEntityId",
                table: "BudgetEntries",
                column: "CategoryEntityId",
                principalTable: "BudgetCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
