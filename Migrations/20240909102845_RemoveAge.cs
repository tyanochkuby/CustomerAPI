using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomersRepo.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Customers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
