using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Listing.Repository.Migrations
{
    public partial class fourteen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "AspNetUsers");
        }
    }
}
