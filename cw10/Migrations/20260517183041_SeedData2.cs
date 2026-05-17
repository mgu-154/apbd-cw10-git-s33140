using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cw10.Migrations
{
    /// <inheritdoc />
    public partial class SeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Components");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Code",
                keyValue: "A",
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Code",
                keyValue: "B",
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Components",
                keyColumn: "Code",
                keyValue: "C",
                column: "Id",
                value: 0);
        }
    }
}
