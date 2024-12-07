using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Comparer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRentalTransactionImageAndDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RentalTransactions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "RentalTransactions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RentalTransactions");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "RentalTransactions");
        }
    }
}
