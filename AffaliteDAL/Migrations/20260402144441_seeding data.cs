using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AffaliteDAL.Migrations
{
    /// <inheritdoc />
    public partial class seedingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "user-affiliate-1", 0, "dff4d241-bed7-4e70-ac27-39ae6eaa52a1", "affiliate@test.com", true, "Affiliate One", false, null, "AFFILIATE@TEST.COM", "AFFILIATE1", "AQAAAAIAAYagAAAAECb3IIYjWRZxQbn6KlvqIsT0oUZ9UAkTP0iP0rI2B7IEo7pUXC0x6zaQY1cdSkbWGA==", null, false, "38ae546d-1aaf-4682-abfb-d88690ff4bb5", false, "affiliate1" },
                    { "user-merchant-1", 0, "04d4585f-707d-45e9-a229-e358897e695b", "merchant@test.com", true, "Merchant One", false, null, "MERCHANT@TEST.COM", "MERCHANT1", "AQAAAAIAAYagAAAAEEfC8UwDo7PyngUXKR86lWrxKYNBWbdXgWsYVHDR8IxB079ezlyeYYf2P0ecObydaA==", null, false, "d4f5a8cb-3d84-460d-af49-9bb7e3a9dfa0", false, "merchant1" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mobiles", "mobiles" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laptops", "laptops" }
                });

            migrationBuilder.InsertData(
                table: "Affiliates",
                columns: new[] { "Id", "AppUserId", "Balance", "CreatedAt" },
                values: new object[] { 1, "user-affiliate-1", 0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "AppUserId", "Balance", "CreatedAt" },
                values: new object[] { 1, "user-merchant-1", 0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AffiliateCommissionPct", "AffiliateId", "CreatedAt", "CustomerAddress", "CustomerName", "CustomerPhone", "MerchantId", "Status", "TotalPrice" },
                values: new object[] { 1, 5m, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cairo", "Ahmed", "01000000000", 1, 3, 30000m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "MerchantId", "Name", "PlatformCommissionPct", "Price", "Status", "Stock" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "518f36c2-0b27-46bf-a813-ff46a4d2168a.jpg", 1, "iPhone 14", 10m, 30000m, 2, 10 },
                    { 2, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "518f36c2-0b27-46bf-a813-ff46a4d2168a.jpg", 1, "Samsung S23", 10m, 25000m, 2, 15 },
                    { 3, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "518f36c2-0b27-46bf-a813-ff46a4d2168a.jpg", 1, "Dell Laptop", 12m, 40000m, 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "Id", "AffiliateAmount", "CreatedAt", "MerchantAmount", "OrderId", "PlatformAmount", "Status" },
                values: new object[] { 1, 1500m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 25500m, 1, 3000m, 2 });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CreatedAt", "OrderId", "Price", "ProductId", "Quantity" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 30000m, 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Affiliates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-affiliate-1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user-merchant-1");
        }
    }
}
