using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Provider.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedGeneratedByFieldToOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GeneratedBy",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneratedBy",
                table: "Offers");
        }
    }
}
