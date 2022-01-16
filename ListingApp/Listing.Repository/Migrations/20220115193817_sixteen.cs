using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Listing.Repository.Migrations
{
    public partial class sixteen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_UserImageId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_UserImageId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UserImageId",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Images",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ListingId",
                table: "Images",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Images",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserId",
                table: "Images",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_UserId",
                table: "Images",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_UserId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_UserId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ListingId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserImageId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserImageId",
                table: "Images",
                column: "UserImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_UserImageId",
                table: "Images",
                column: "UserImageId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
