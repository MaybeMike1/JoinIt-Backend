using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoinIt_Backend.Migrations
{
    /// <inheritdoc />
    public partial class addressinactivitiyandvenuename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VenueName",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AddressId",
                table: "Activities",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Addresses_AddressId",
                table: "Activities",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Addresses_AddressId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_AddressId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "VenueName",
                table: "Activities");
        }
    }
}
