﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Listing.Repository.Migrations
{
    public partial class six : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ListingId",
                table: "Images",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserImageId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ListingId",
                table: "Images",
                column: "ListingId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserImageId",
                table: "Images",
                column: "UserImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Listings_ListingId",
                table: "Images",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_UserImageId",
                table: "Images",
                column: "UserImageId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Listings_ListingId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_UserImageId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ListingId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_UserImageId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UserImageId",
                table: "Images");
        }
    }
}
