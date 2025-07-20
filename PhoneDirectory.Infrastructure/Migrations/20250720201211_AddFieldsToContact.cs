using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneDirectory.Infrastructure.Migrations
{
    public partial class AddFieldsToContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Contacts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoPath",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoPath",
                table: "Contacts");
        }
    }
}
