using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Listing.Repository.Migrations
{
    public partial class ten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                table: "Images",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<string>(
                name: "ImageDataBase64",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSrc",
                table: "Images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageDataBase64",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageSrc",
                table: "Images");

            migrationBuilder.AlterColumn<byte>(
                name: "ImageData",
                table: "Images",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }
    }
}
