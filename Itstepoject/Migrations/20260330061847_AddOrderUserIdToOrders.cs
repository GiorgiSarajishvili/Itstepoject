using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itstepoject.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderUserIdToOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");
        }
    }
}
