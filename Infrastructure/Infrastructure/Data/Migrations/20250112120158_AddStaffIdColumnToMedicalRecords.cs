using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStaffIdColumnToMedicalRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar la columna StaffId a la tabla MedicalRecords
            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "MedicalRecords",
                nullable: false,  // Esto puede ser true si la columna StaffId puede ser nula
                defaultValue: 0); // Este valor predeterminado puede ser ajustado según sea necesario

            // Agregar la clave foránea para la relación entre MedicalRecords y Staffs
            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Staffs_StaffId",
                table: "MedicalRecords",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar la clave foránea si se revierte la migración
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Staffs_StaffId",
                table: "MedicalRecords");

            // Eliminar la columna StaffId si se revierte la migración
            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "MedicalRecords");
        }
    }
}
