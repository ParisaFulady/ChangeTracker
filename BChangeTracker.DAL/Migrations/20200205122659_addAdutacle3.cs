using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BChangeTracker.DAL.Migrations
{
    public partial class addAdutacle3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Updateby",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "insertDate",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "insertby",
                table: "Students",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Updateby",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "insertDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "insertby",
                table: "Students");
        }
    }
}
