using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yaba.Entities.Migrations
{
    public partial class BetterTabItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateBy",
                table: "TabItems",
                newName: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TabItems_CreatedById",
                table: "TabItems",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_TabItems_Users_CreatedById",
                table: "TabItems",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabItems_Users_CreatedById",
                table: "TabItems");

            migrationBuilder.DropIndex(
                name: "IX_TabItems_CreatedById",
                table: "TabItems");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "TabItems",
                newName: "CreateBy");
        }
    }
}
