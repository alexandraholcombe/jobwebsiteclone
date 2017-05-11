using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JobWebsiteClone.Migrations
{
    public partial class addEmployer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });

            migrationBuilder.AddColumn<int>(
                name: "EmployerId",
                table: "Listings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_EmployerId",
                table: "Listings",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Employers_EmployerId",
                table: "Listings",
                column: "EmployerId",
                principalTable: "Employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Employers_EmployerId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_EmployerId",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "Listings");

            migrationBuilder.DropTable(
                name: "Employers");
        }
    }
}
