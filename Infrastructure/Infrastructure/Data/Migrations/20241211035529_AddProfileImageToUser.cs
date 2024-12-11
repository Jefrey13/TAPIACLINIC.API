using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileImageToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Menus_MenuId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_MenuId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Roles");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImage",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_MenuId",
                table: "Roles",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Menus_MenuId",
                table: "Roles",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id");
        }
    }
}
