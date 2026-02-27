using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "NVARCHAR(60)",
                nullable: true);

            migrationBuilder.Sql("UPDATE Orders SET Status = 'Completed'");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "NVARCHAR(60)",
                nullable: false,
                defaultValue: "Draft");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");
        }
    }
}
