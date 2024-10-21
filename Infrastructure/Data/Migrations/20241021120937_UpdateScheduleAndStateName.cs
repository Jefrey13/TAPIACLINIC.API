using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScheduleAndStateName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Staffs_StaffId",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "StateName",
                table: "States",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Schedules",
                newName: "SpecialtyId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_StaffId",
                table: "Schedules",
                newName: "IX_Schedules_SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Specialties_SpecialtyId",
                table: "Schedules",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Specialties_SpecialtyId",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "States",
                newName: "StateName");

            migrationBuilder.RenameColumn(
                name: "SpecialtyId",
                table: "Schedules",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_SpecialtyId",
                table: "Schedules",
                newName: "IX_Schedules_StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Staffs_StaffId",
                table: "Schedules",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
