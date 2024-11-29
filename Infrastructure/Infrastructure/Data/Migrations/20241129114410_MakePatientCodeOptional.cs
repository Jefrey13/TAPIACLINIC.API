using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakePatientCodeOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_PatientCode",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PatientCode",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_User_PatientCode",
                table: "Users",
                column: "PatientCode",
                unique: true,
                filter: "[PatientCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_PatientCode",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PatientCode",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PatientCode",
                table: "Users",
                column: "PatientCode",
                unique: true);
        }
    }
}
