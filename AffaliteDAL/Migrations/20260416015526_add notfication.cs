using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AffaliteDAL.Migrations
{
    /// <inheritdoc />
    public partial class addnotfication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Affiliates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 1, 55, 24, 944, DateTimeKind.Utc).AddTicks(811));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d274f1d-c805-4a48-ab58-94d7cc63e51f", "AQAAAAIAAYagAAAAEH96PteYRI8fFolKTWg/oY3F23J34ZH7EXeitI4K6PdrSibm3GVtGbV9GnjY4BvLDQ==", "c99e69cd-4e45-4b9a-b5fe-6904d0af20cb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f193082-be8d-40c1-8530-0a65ac5a9ba8", "AQAAAAIAAYagAAAAEKV9ypS1P1UQJ16FB/X3UfwvEYtHN15tgkwyU7y3C5N0J310FD0OqIIQlK86rNQiXw==", "66c08a0a-47cc-4530-95a0-6e037e28c940" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93bb4e7e-18e9-4a0c-b0ed-bf718fa2c568", "AQAAAAIAAYagAAAAEB6Pka8QTtJ/r41yilUSPzWMxrde08kj1Fep0dgWdJPaq+8DSSA0Appq5vHh6PsSNA==", "49d5559b-fce8-40a6-b13b-527c5b5b5276" });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 3, 55, 24, 944, DateTimeKind.Local).AddTicks(1625));

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 3, 55, 24, 944, DateTimeKind.Local).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 3, 55, 24, 944, DateTimeKind.Local).AddTicks(1526));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 1, 55, 24, 944, DateTimeKind.Utc).AddTicks(585));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 1, 55, 24, 944, DateTimeKind.Utc).AddTicks(592));

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 1, 55, 24, 944, DateTimeKind.Utc).AddTicks(2070));

            migrationBuilder.UpdateData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 1, 55, 24, 944, DateTimeKind.Utc).AddTicks(701));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 3, 55, 24, 944, DateTimeKind.Local).AddTicks(1953));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 3, 55, 24, 944, DateTimeKind.Local).AddTicks(1964));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 3, 55, 24, 944, DateTimeKind.Local).AddTicks(1778));

            migrationBuilder.UpdateData(
                table: "ProductImage",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "p5.png");

            migrationBuilder.UpdateData(
                table: "ProductReviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 3, 55, 24, 944, DateTimeKind.Local).AddTicks(1405));

            migrationBuilder.UpdateData(
                table: "ProductReviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 3, 55, 24, 944, DateTimeKind.Local).AddTicks(1419));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 3, 55, 24, 944, DateTimeKind.Local).AddTicks(999));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 3, 55, 24, 944, DateTimeKind.Local).AddTicks(1155));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Affiliates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 5, 19, 23, 602, DateTimeKind.Utc).AddTicks(3104));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5225354-41b5-42d5-86ca-0bc8a3e7e7d3", "AQAAAAIAAYagAAAAEBbBSfBGA5jM89nEYiCvg8+9/V+RIYQ8y3NYog6AOnIsHPmhqlYwF0W+uH7uLfmYew==", "b18f7a67-6283-4248-a53d-3ad677d6d9b0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67215f38-a34e-451c-8789-5b8be665061c", "AQAAAAIAAYagAAAAEA9/QAFMX0LECw2HNq5bjEelW+HW9BW3htFPCc/5Gp3pjQIqnAYAYlykq/c1VkndIw==", "99125844-003e-454e-94ea-cdf0a15f4774" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a49762b8-b848-4667-b397-7461b61585d2", "AQAAAAIAAYagAAAAEFqDCtUpGBVLS6ArB879mFRA8qj2zqn55BGgEyIn32HIjiChJc/Mg3tRT7vKcZg6AQ==", "40478c9c-ca84-4ff9-966b-2e1a735202c5" });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(4341));

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(4381));

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(4132));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 5, 19, 23, 602, DateTimeKind.Utc).AddTicks(2668));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 5, 19, 23, 602, DateTimeKind.Utc).AddTicks(2673));

            migrationBuilder.UpdateData(
                table: "Commissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 5, 19, 23, 602, DateTimeKind.Utc).AddTicks(5421));

            migrationBuilder.UpdateData(
                table: "Merchants",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 5, 19, 23, 602, DateTimeKind.Utc).AddTicks(2893));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(5205));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(5226));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(4760));

            migrationBuilder.UpdateData(
                table: "ProductImage",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "p1.jpg");

            migrationBuilder.UpdateData(
                table: "ProductReviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(3853));

            migrationBuilder.UpdateData(
                table: "ProductReviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(3882));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(3296));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 7, 19, 23, 602, DateTimeKind.Local).AddTicks(3453));
        }
    }
}
