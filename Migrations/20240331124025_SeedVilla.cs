using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedAt", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "SQft", "UpdatedAt" },
                values: new object[] { 1, "Villa 1 Amenity", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Villa 1 Description", "https://via.placeholder.com/150", "Villa 1", 20, 1000.0, 5000, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
