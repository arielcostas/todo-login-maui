using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class Watafac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tareas",
                table: "Tareas");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tareas",
                newName: "TareaId");

            migrationBuilder.AlterColumn<string>(
                name: "CreadorId",
                table: "Tareas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tareas",
                table: "Tareas",
                column: "TareaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_CreadorId",
                table: "Tareas",
                column: "CreadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tareas",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_CreadorId",
                table: "Tareas");

            migrationBuilder.RenameColumn(
                name: "TareaId",
                table: "Tareas",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "CreadorId",
                table: "Tareas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tareas",
                table: "Tareas",
                columns: new[] { "CreadorId", "Id" });
        }
    }
}
