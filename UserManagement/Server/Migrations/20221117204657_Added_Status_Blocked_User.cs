using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatusBlockedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserStatus",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserStatus",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
