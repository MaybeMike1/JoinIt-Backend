using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoinIt_Backend.Shared.Migrations
{
    /// <inheritdoc />
    public partial class addexternalidentityid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalIdentityId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalIdentityId",
                table: "Users");
        }
    }
}
