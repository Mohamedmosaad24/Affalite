using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AffaliteDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddWishlistAndCUpon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MaxUsageCount = table.Column<int>(type: "int", nullable: true),
                    UsedCount = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coupons_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

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

            migrationBuilder.UpdateData(
                table: "Affiliates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 28, 26, 816, DateTimeKind.Utc).AddTicks(5198));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37def511-4a06-4e31-bedb-20195e896fa6", "AQAAAAIAAYagAAAAENO4PTD6mqNFxnYbeMHfgS+Pc7Ot4poZTL0Mz1Jz5LK8koKpuVgxP8bzza7dYDfH9g==", "71fcc375-e893-41d5-acd0-9b856af3d604" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d2ba00c-8838-42e2-9888-1b2b33ecb085", "AQAAAAIAAYagAAAAECqEcPL4DXuIPiG1P9qGXNI690u+xkaKCoUmbubt8nShpqXBV9SoJaAH9Qdc8r64hA==", "cb003b91-726b-4b8e-9812-48017684744c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5aafab0d-0426-4b06-8bdc-f90163ee1291", "AQAAAAIAAYagAAAAELFbM+DQBhy+D/5boAdYht7I3GkxojdZYMHY4yrAZfFTsenNUuaeGXkUQaLxwxTwXw==", "8effee1f-cf47-4856-bbc5-e0e67f0cecf5" });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 22, 28, 26, 816, DateTimeKind.Local).AddTicks(5672));

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 22, 28, 26, 816, DateTimeKind.Local).AddTicks(5681));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 22, 28, 26, 816, DateTimeKind.Local).AddTicks(5589));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 28, 26, 816, DateTimeKind.Utc).AddTicks(5040));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 28, 26, 816, DateTimeKind.Utc).AddTicks(5042));

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 20, 28, 26, 816, DateTimeKind.Utc).AddTicks(6647));

            migrationBuilder.UpdateData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 28, 26, 816, DateTimeKind.Utc).AddTicks(5119));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 22, 28, 26, 816, DateTimeKind.Local).AddTicks(6432));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 22, 28, 26, 816, DateTimeKind.Local).AddTicks(6472));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 22, 28, 26, 816, DateTimeKind.Local).AddTicks(5952));

            migrationBuilder.UpdateData(
                table: "ProductReview",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 22, 28, 26, 816, DateTimeKind.Local).AddTicks(5496));

            migrationBuilder.UpdateData(
                table: "ProductReview",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 22, 28, 26, 816, DateTimeKind.Local).AddTicks(5502));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 22, 28, 26, 816, DateTimeKind.Local).AddTicks(5319));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 22, 28, 26, 816, DateTimeKind.Local).AddTicks(5377));

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_ProductId",
                table: "Coupons",
                column: "ProductId");

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
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.UpdateData(
                table: "Affiliates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 5, 45, 21, 544, DateTimeKind.Utc).AddTicks(1696));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0430c25f-bc2c-4739-a327-03a9b9ee2754", "AQAAAAIAAYagAAAAEPPDMA7sBsVIbG+yimcCCs8MAWnBoCbEQA7cfJy3Bf6a3ViKKJM6DDZSgWn4X4KmfQ==", "c4e5dc9f-399f-4cfa-a573-a6545afbb9f0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de5c8a26-b37e-4ce0-a597-f89ef104ee6e", "AQAAAAIAAYagAAAAEIxgSbNB7oHr17LVvb0UWKPhgB0qYUCHvQ8JHyRvkCrqMH60bPP1JDPAAUmycP7Fvw==", "964975d4-d7cd-4627-bbff-d26984c7e4e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e344c7fd-97e9-46b9-85fd-621baf5c30de", "AQAAAAIAAYagAAAAEIzaSLKx5VgiYzdFdWD0ki16N/k/WmDvmnRbPy+Jg2C/VfY/f3hHQLQwhMIXBsHVXA==", "5cb3470c-cc81-489c-b667-1306e6b42c82" });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 7, 45, 21, 544, DateTimeKind.Local).AddTicks(2236));

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 7, 45, 21, 544, DateTimeKind.Local).AddTicks(2253));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 7, 45, 21, 544, DateTimeKind.Local).AddTicks(2170));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 5, 45, 21, 544, DateTimeKind.Utc).AddTicks(1498));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 5, 45, 21, 544, DateTimeKind.Utc).AddTicks(1501));

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 5, 45, 21, 544, DateTimeKind.Utc).AddTicks(2670));

            migrationBuilder.UpdateData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 5, 45, 21, 544, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 7, 45, 21, 544, DateTimeKind.Local).AddTicks(2581));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 7, 45, 21, 544, DateTimeKind.Local).AddTicks(2586));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 7, 45, 21, 544, DateTimeKind.Local).AddTicks(2397));

            migrationBuilder.UpdateData(
                table: "ProductReview",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 7, 45, 21, 544, DateTimeKind.Local).AddTicks(2063));

            migrationBuilder.UpdateData(
                table: "ProductReview",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 7, 45, 21, 544, DateTimeKind.Local).AddTicks(2068));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 7, 45, 21, 544, DateTimeKind.Local).AddTicks(1865));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 7, 45, 21, 544, DateTimeKind.Local).AddTicks(1926));

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
