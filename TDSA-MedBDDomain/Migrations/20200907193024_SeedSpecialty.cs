using Microsoft.EntityFrameworkCore.Migrations;

namespace TDSA_MedBDDomain.Migrations
{
    public partial class SeedSpecialty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "deleted_at", "Name" },
                values: new object[] { 1, null, "Clínico Geral" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
