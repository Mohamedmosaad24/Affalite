using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AffaliteDAL.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-admin");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-merchant", "user1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-affiliate", "user2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "role-customer", "user3" });

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MerchantCommissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MerchantOrder",
                keyColumns: new[] { "MerchantId", "OrderId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImage",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductReviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductReviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-affiliate");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-customer");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "role-merchant");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user3");

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Commissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Affiliates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user2");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercentage",
                table: "Coupons",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AffiliateId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ProductId",
                table: "Wishlists",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercentage",
                table: "Coupons",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "role-admin", null, "Admin", "ADMIN" },
                    { "role-affiliate", null, "Affiliate", "AFFILIATE" },
                    { "role-customer", null, "Customer", "CUSTOMER" },
                    { "role-merchant", null, "Merchant", "MERCHANT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "user1", 0, "913acdbb-b509-47e1-87b7-17248105825b", "merchant1@affalite.com", true, "Ahmed Hassan", false, null, "MERCHANT1@AFFALITE.COM", "MERCHANT1@AFFALITE.COM", "AQAAAAIAAYagAAAAEG1EqOF9+zjivoS3AiebUoT1CCdF2Odc2kkHzuzCcAOkRcwVWeQ7yMoyxX8UTGO2pw==", "01001234567", false, "62fbe75a-437e-4fb0-8ad2-b76d15515e5d", false, "merchant1" },
                    { "user2", 0, "4e3d6d9f-35a1-4d57-af3b-849dc4b42c05", "affiliate1@affalite.com", true, "Youssef Ali", false, null, "AFFILIATE1@AFFALITE.COM", "AFFILIATE1@AFFALITE.COM", "AQAAAAIAAYagAAAAEImCU2KjZAXmXa7LE5jzBRLzhrECEBKnlXiafdiQci06rZEcqEMaq99ekvPamOgG1w==", "01001112233", false, "f9c3c6d1-b65c-4d94-886a-d05b98ed29b0", false, "affiliate1" },
                    { "user3", 0, "9b4c966a-1fa2-44fe-a1b8-2abbeff3ee8f", "customer1@affalite.com", true, "Hana Adel", false, null, "CUSTOMER1@AFFALITE.COM", "CUSTOMER1@AFFALITE.COM", "AQAAAAIAAYagAAAAEKOP1rO39EN1hSePVu1n5c8DXNi6JkPwc2bJb1nVT6fXGv5bJhoCNNvMLm7GgGl5AQ==", "01002223344", false, "1b90b7e7-3d6e-4208-8b6f-010980e1c96b", false, "customer1" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 11, 2, 50, 18, 162, DateTimeKind.Utc).AddTicks(3628), "Electronics", "electronics" },
                    { 2, new DateTime(2026, 4, 11, 2, 50, 18, 162, DateTimeKind.Utc).AddTicks(3630), "Fashion", "fashion" }
                });

            migrationBuilder.InsertData(
                table: "Affiliates",
                columns: new[] { "Id", "AppUserId", "Balance", "CreatedAt" },
                values: new object[] { 1, "user2", 1500m, new DateTime(2026, 4, 11, 2, 50, 18, 162, DateTimeKind.Utc).AddTicks(3777) });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "role-merchant", "user1" },
                    { "role-affiliate", "user2" },
                    { "role-customer", "user3" }
                });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "AppUserId", "Balance", "CreatedAt" },
                values: new object[] { 1, "user1", 5000m, new DateTime(2026, 4, 11, 2, 50, 18, 162, DateTimeKind.Utc).AddTicks(3708) });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "AffiliateId", "CreatedAt" },
                values: new object[] { 1, 1, new DateTime(2026, 4, 11, 4, 50, 18, 162, DateTimeKind.Local).AddTicks(4077) });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AffiliateCommissionPct", "AffiliateId", "CreatedAt", "CustomerAddress", "CustomerName", "CustomerPhone", "Status", "TotalPrice" },
                values: new object[] { 1, 5m, 1, new DateTime(2026, 4, 11, 4, 50, 18, 162, DateTimeKind.Local).AddTicks(4260), "123 Street", "David", "01000000004", 1, 2018m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Details", "MerchantId", "Name", "PlatformCommissionPct", "Price", "SaleCount", "Status", "Stock" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 4, 11, 4, 50, 18, 162, DateTimeKind.Local).AddTicks(3844), "Latest Apple iPhone", "Details here", 1, "iPhone 14", 5m, 999m, 10, 2, 50 },
                    { 2, 2, new DateTime(2026, 4, 11, 4, 50, 18, 162, DateTimeKind.Local).AddTicks(3916), "Fantasy novel", "Details here", 1, "Harry Potter Book", 2m, 20m, 50, 2, 100 }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CartId", "CreatedAt", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 4, 11, 4, 50, 18, 162, DateTimeKind.Local).AddTicks(4116), 1, 2 },
                    { 2, 1, new DateTime(2026, 4, 11, 4, 50, 18, 162, DateTimeKind.Local).AddTicks(4133), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "Id", "AffiliateAmount", "CreatedAt", "MerchantAmount", "OrderId", "PlatformAmount", "Status" },
                values: new object[] { 1, 578.99m, new DateTime(2026, 3, 13, 2, 50, 18, 162, DateTimeKind.Utc).AddTicks(4396), 17756.00m, 1, 964.99m, 1 });

            migrationBuilder.InsertData(
                table: "MerchantOrder",
                columns: new[] { "MerchantId", "OrderId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CreatedAt", "OrderId", "Price", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 11, 4, 50, 18, 162, DateTimeKind.Local).AddTicks(4344), 1, 999m, 1, 2 },
                    { 2, new DateTime(2026, 4, 11, 4, 50, 18, 162, DateTimeKind.Local).AddTicks(4348), 1, 20m, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "Id", "FileName", "ImageUrl", "ProductId" },
                values: new object[,]
                {
                    { 1, "iphone14.jpg", "p1.jpg", 1 },
                    { 2, "iphone14.jpg", "p4.jpg", 1 },
                    { 3, "harrypotter.jpg", "p2.png", 2 },
                    { 4, "harrypotter.jpg", "p3.jpg", 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductReviews",
                columns: new[] { "Id", "AffiliateId", "Comment", "CreatedAt", "ProductId", "Rating" },
                values: new object[,]
                {
                    { 1, 1, "Great phone!", new DateTime(2026, 4, 11, 4, 50, 18, 162, DateTimeKind.Local).AddTicks(4007), 1, 5 },
                    { 2, 1, "Loved the book", new DateTime(2026, 4, 11, 4, 50, 18, 162, DateTimeKind.Local).AddTicks(4010), 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "MerchantCommissions",
                columns: new[] { "Id", "CommissionId", "MerchantId", "value" },
                values: new object[] { 1, 1, 1, 20m });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
