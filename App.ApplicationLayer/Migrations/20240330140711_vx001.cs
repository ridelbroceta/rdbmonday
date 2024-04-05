using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.ApplicationLayer.Migrations
{
    /// <inheritdoc />
    public partial class vx001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "sch1");

            migrationBuilder.CreateTable(
                name: "Table_1",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Table_2",
                schema: "sch1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_2", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Table_1",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Table_2",
                schema: "sch1");
        }
    }
}
