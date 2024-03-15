using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMesProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessParameters_Parameters_ParametersId",
                table: "ProcessParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessParameters_Processes_ProcessesId",
                table: "ProcessParameters");

            migrationBuilder.DropIndex(
                name: "IX_ProcessParameters_ParametersId",
                table: "ProcessParameters");

            migrationBuilder.DropIndex(
                name: "IX_ProcessParameters_ProcessesId",
                table: "ProcessParameters");

            migrationBuilder.DropColumn(
                name: "ParametersId",
                table: "ProcessParameters");

            migrationBuilder.DropColumn(
                name: "ProcessesId",
                table: "ProcessParameters");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessParameters_ParameterId",
                table: "ProcessParameters",
                column: "ParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessParameters_ProcessId",
                table: "ProcessParameters",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessParameters_Parameters_ParameterId",
                table: "ProcessParameters",
                column: "ParameterId",
                principalTable: "Parameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessParameters_Processes_ProcessId",
                table: "ProcessParameters",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessParameters_Parameters_ParameterId",
                table: "ProcessParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessParameters_Processes_ProcessId",
                table: "ProcessParameters");

            migrationBuilder.DropIndex(
                name: "IX_ProcessParameters_ParameterId",
                table: "ProcessParameters");

            migrationBuilder.DropIndex(
                name: "IX_ProcessParameters_ProcessId",
                table: "ProcessParameters");

            migrationBuilder.AddColumn<int>(
                name: "ParametersId",
                table: "ProcessParameters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcessesId",
                table: "ProcessParameters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessParameters_ParametersId",
                table: "ProcessParameters",
                column: "ParametersId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessParameters_ProcessesId",
                table: "ProcessParameters",
                column: "ProcessesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessParameters_Parameters_ParametersId",
                table: "ProcessParameters",
                column: "ParametersId",
                principalTable: "Parameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessParameters_Processes_ProcessesId",
                table: "ProcessParameters",
                column: "ProcessesId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
