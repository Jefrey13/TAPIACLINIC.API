using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexesToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PatientCode",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdCard",
                table: "Users",
                column: "IdCard",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PatientCode",
                table: "Users",
                column: "PatientCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_IdCard",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_User_PatientCode",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_User_UserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PatientCode",
                table: "Users");
        }
    }
}
