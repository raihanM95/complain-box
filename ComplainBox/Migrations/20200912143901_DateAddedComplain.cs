using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComplainBox.Migrations
{
    public partial class DateAddedComplain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ComplainDate",
                table: "complains",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "complains",
                type: "date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComplainDate",
                table: "complains");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "complains");
        }
    }
}
