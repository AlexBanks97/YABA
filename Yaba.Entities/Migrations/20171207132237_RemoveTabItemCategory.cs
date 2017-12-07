using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Yaba.Entities.Migrations
{
    public partial class RemoveTabItemCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabItems_TabItemCategories_CategoryEntityId",
                table: "TabItems");

            migrationBuilder.DropTable(
                name: "TabItemCategories");

            migrationBuilder.DropIndex(
                name: "IX_TabItems_CategoryEntityId",
                table: "TabItems");

            migrationBuilder.DropColumn(
                name: "CategoryEntityId",
                table: "TabItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryEntityId",
                table: "TabItems",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_TabItems_CategoryEntityId",
                table: "TabItems",
                column: "CategoryEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TabItems_TabItemCategories_CategoryEntityId",
                table: "TabItems",
                column: "CategoryEntityId",
                principalTable: "TabItemCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
