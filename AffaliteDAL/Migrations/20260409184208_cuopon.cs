using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AffaliteDAL.Migrations
{
    /// <inheritdoc />
    public partial class cuopon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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

            migrationBuilder.UpdateData(
                table: "Affiliates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 18, 42, 7, 201, DateTimeKind.Utc).AddTicks(893));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87093f4a-af0c-4dda-95ac-a42a26164916", "AQAAAAIAAYagAAAAELzjoAHjWcQF845EAumKRu3Q4kHlbKwOw+5JIth1Aok9lO/48EoNglfMvHuKVk5/CQ==", "7e7f0870-16f1-4646-ab66-1299b2349603" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "834cb421-d1f9-417b-9ba8-cf28bd305fa0", "AQAAAAIAAYagAAAAEBpsV3JSuAymK93EruivBt9qLLVQtpKRja4RtY1u/h9gmpZ5Krw7v1oUIJXAx12u/A==", "8f97c156-ad81-4e0c-9e8a-9a315ca151d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8d5481a-5261-4dba-b732-fa39fcb3bcad", "AQAAAAIAAYagAAAAELOS0hoIz/+Tvby9u4L/vi5/OC+BupHPWX20AlifpeNQ+OHt6dyEnfyiQC99MBf6BQ==", "3c6f64c7-dc04-4e7e-8bb9-fac97c6de76b" });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1265));

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1275));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1213));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 18, 42, 7, 201, DateTimeKind.Utc).AddTicks(756));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 18, 42, 7, 201, DateTimeKind.Utc).AddTicks(759));

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 18, 42, 7, 201, DateTimeKind.Utc).AddTicks(1674));

            migrationBuilder.UpdateData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 18, 42, 7, 201, DateTimeKind.Utc).AddTicks(827));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1463));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1468));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1346));

            migrationBuilder.UpdateData(
                table: "ProductReview",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1137));

            migrationBuilder.UpdateData(
                table: "ProductReview",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1143));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(964));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 20, 42, 7, 201, DateTimeKind.Local).AddTicks(1028));

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_ProductId",
                table: "Coupons",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");

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
        }
    }
}
