using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPP.API.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class AddIdentityRoleAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4e3c5cff-4462-460e-824b-86e40cb1a1fa", "4e3c5cff-4462-460e-824b-86e40cb1a1fa", "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4e3c5cff-4462-460e-824b-86e40cb1a1fa",
                columns: new[] { "LockoutEnd", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 7, 20, 23, 32, 53, 932, DateTimeKind.Unspecified).AddTicks(2186), new TimeSpan(0, 0, 0, 0, 0)), "AQAAAAIAAYagAAAAENR7z7Xq++eZppr1C9rEQI0Kghqsl5/hG6BlloP1jjfzVDXFLqnWuxIaOJp1KNGs7Q==", "be3cc782-faa6-4ba0-9541-369547bbe176" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4e3c5cff-4462-460e-824b-86e40cb1a1fa", "4e3c5cff-4462-460e-824b-86e40cb1a1fa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4e3c5cff-4462-460e-824b-86e40cb1a1fa", "4e3c5cff-4462-460e-824b-86e40cb1a1fa" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e3c5cff-4462-460e-824b-86e40cb1a1fa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4e3c5cff-4462-460e-824b-86e40cb1a1fa",
                columns: new[] { "LockoutEnd", "PasswordHash", "SecurityStamp" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 11, 14, 7, 20, 35, 224, DateTimeKind.Unspecified).AddTicks(6105), new TimeSpan(0, 0, 0, 0, 0)), "AQAAAAIAAYagAAAAEPY6z8NPHiooka76ZVqPaiVHtgiRsKkJ2AVjMGZg+gtK82p92jhmoM1QWwy5REMo5g==", "b0479587-46ae-4ade-ac4b-51ba27b0888a" });
        }
    }
}
