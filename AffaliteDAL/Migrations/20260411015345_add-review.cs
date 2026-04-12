using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AffaliteDAL.Migrations
{
    /// <inheritdoc />
    public partial class addreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductReview");

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

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AffiliateId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.CreateTable(
                name: "ProductReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AffiliateId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReview_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    { "user1", 0, "87093f4a-af0c-4dda-95ac-a42a26164916", "merchant1@affalite.com", true, "Ahmed Hassan", false, null, "MERCHANT1@AFFALITE.COM", "MERCHANT1@AFFALITE.COM", "AQAAAAIAAYagAAAAELzjoAHjWcQF845EAumKRu3Q4kHlbKwOw+5JIth1Aok9lO/48EoNglfMvHuKVk5/CQ==", "01001234567", false, "7e7f0870-16f1-4646-ab66-1299b2349603", false, "merchant1" },
                    { "user2", 0, "834cb421-d1f9-417b-9ba8-cf28bd305fa0", "affiliate1@affalite.com", true, "Youssef Ali", false, null, "AFFILIATE1@AFFALITE.COM", "AFFILIATE1@AFFALITE.COM", "AQAAAAIAAYagAAAAEBpsV3JSuAymK93EruivBt9qLLVQtpKRja4RtY1u/h9gmpZ5Krw7v1oUIJXAx12u/A==", "01001112233", false, "8f97c156-ad81-4e0c-9e8a-9a315ca151d8", false, "affiliate1" },
                    { "user3", 0, "f8d5481a-5261-4dba-b732-fa39fcb3bcad", "customer1@affalite.com", true, "Hana Adel", false, null, "CUSTOMER1@AFFALITE.COM", "CUSTOMER1@AFFALITE.COM", "AQAAAAIAAYagAAAAELOS0hoIz/+Tvby9u4L/vi5/OC+BupHPWX20AlifpeNQ+OHt6dyEnfyiQC99MBf6BQ==", "01002223344", false, "3c6f64c7-dc04-4e7e-8bb9-fac97c6de76b", false, "customer1" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 9, 18, 42, 7, 201, DateTimeKind.Utc).AddTicks(756), "Electronics", "electronics" },
                    { 2, new DateTime(2026, 4, 9, 18, 42, 7, 201, DateTimeKind.Utc).AddTicks(759), "Fashion", "fashion" }
                });

            migrationBuilder.InsertData(
                table: "Affiliates",
                columns: new[] { "Id", "AppUserId", "Balance", "CreatedAt" },
                values: new object[] { 1, "user2", 1500m, new DateTime(2026, 4, 9, 18, 42, 7, 201, DateTimeKind.Utc).AddTicks(893) });

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
                values: new object[] { 1, "user1", 5000m, new DateTime(2026, 4, 9, 18, 42, 7, 201, DateTimeKind.Utc).AddTicks(827) });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "AffiliateId", "CreatedAt" },
                values: new object[] { 1, 1, new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1213) });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AffiliateCommissionPct", "AffiliateId", "CreatedAt", "CustomerAddress", "CustomerName", "CustomerPhone", "Status", "TotalPrice" },
                values: new object[] { 1, 5m, 1, new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1346), "123 Street", "David", "01000000004", 1, 2018m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Details", "MerchantId", "Name", "PlatformCommissionPct", "Price", "SaleCount", "Status", "Stock" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(964), "Latest Apple iPhone", "Details here", 1, "iPhone 14", 5m, 999m, 10, 2, 50 },
                    { 2, 2, new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1028), "Fantasy novel", "Details here", 1, "Harry Potter Book", 2m, 20m, 50, 2, 100 }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CartId", "CreatedAt", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1265), 1, 2 },
                    { 2, 1, new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1275), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Commissions",
                columns: new[] { "Id", "AffiliateAmount", "CreatedAt", "MerchantAmount", "OrderId", "PlatformAmount", "Status" },
                values: new object[] { 1, 578.99m, new DateTime(2026, 3, 11, 18, 42, 7, 201, DateTimeKind.Utc).AddTicks(1674), 17756.00m, 1, 964.99m, 1 });

            migrationBuilder.InsertData(
                table: "MerchantOrder",
                columns: new[] { "MerchantId", "OrderId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CreatedAt", "OrderId", "Price", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1463), 1, 999m, 1, 2 },
                    { 2, new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1468), 1, 20m, 2, 1 }
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
                table: "ProductReview",
                columns: new[] { "Id", "AffiliateId", "Comment", "CreatedAt", "ProductId", "Rating" },
                values: new object[,]
                {
                    { 1, 1, "Great phone!", new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1137), 1, 5 },
                    { 2, 1, "Loved the book", new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1143), 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "MerchantCommissions",
                columns: new[] { "Id", "CommissionId", "MerchantId", "value" },
                values: new object[] { 1, 1, 1, 20m });

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ProductId",
                table: "ProductReview",
                column: "ProductId");
        }
    }
}
