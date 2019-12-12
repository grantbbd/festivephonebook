using Microsoft.EntityFrameworkCore.Migrations;

namespace Festive_Phonebook.Migrations
{
    public partial class Entry2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "PhonebookEntries",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "PhonebookEntries",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PhonebookEntries");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "PhonebookEntries");
        }
    }
}
