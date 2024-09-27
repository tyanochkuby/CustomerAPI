using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomersRepo.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "CustomersRepo",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                schema: "CustomersRepo",
                table: "Customers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                schema: "CustomersRepo",
                table: "Customers",
                column: "UserId",
                principalSchema: "CustomersRepo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                schema: "CustomersRepo",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserId",
                schema: "CustomersRepo",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "CustomersRepo",
                table: "Customers");
        }
    }
}
