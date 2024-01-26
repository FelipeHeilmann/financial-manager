using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialManager.Infra.Migrations
{
    /// <inheritdoc />
    public partial class MapTransactionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "transactions",
                newName: "type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "transactions",
                newName: "Type");
        }
    }
}
