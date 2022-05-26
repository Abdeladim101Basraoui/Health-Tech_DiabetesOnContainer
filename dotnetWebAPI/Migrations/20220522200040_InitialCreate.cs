using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetWebAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    C_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    C_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    C_lastName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.C_ID);
                });

            migrationBuilder.CreateTable(
                name: "ficheMedicals",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    analyses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    patientC_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ficheMedicals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ficheMedicals_Patients_patientC_ID",
                        column: x => x.patientC_ID,
                        principalTable: "Patients",
                        principalColumn: "C_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ficheMedicals_patientC_ID",
                table: "ficheMedicals",
                column: "patientC_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ficheMedicals");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
