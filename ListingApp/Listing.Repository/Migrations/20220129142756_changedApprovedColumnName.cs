using Microsoft.EntityFrameworkCore.Migrations;

namespace Listing.Repository.Migrations
{
    public partial class changedApprovedColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Listings");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Listings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Listings");

            migrationBuilder.AddColumn<string>(
                name: "Approved",
                table: "Listings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
