using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMesProject.Migrations
{
    /// <inheritdoc />
    public partial class relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParametersId",
                table: "ProcessParameters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcessesId",
                table: "ProcessParameters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "Processes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MachineId1",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessParameters_ParametersId",
                table: "ProcessParameters",
                column: "ParametersId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessParameters_ProcessesId",
                table: "ProcessParameters",
                column: "ProcessesId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_OrderId1",
                table: "Processes",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MachineId1",
                table: "Orders",
                column: "MachineId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId1",
                table: "Orders",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Machines_MachineId1",
                table: "Orders",
                column: "MachineId1",
                principalTable: "Machines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId1",
                table: "Orders",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Orders_OrderId1",
                table: "Processes",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessParameters_Parameters_ParametersId",
                table: "ProcessParameters",
                column: "ParametersId",
                principalTable: "Parameters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessParameters_Processes_ProcessesId",
                table: "ProcessParameters",
                column: "ProcessesId",
                principalTable: "Processes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Machines_MachineId1",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId1",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Orders_OrderId1",
                table: "Processes");

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

            migrationBuilder.DropIndex(
                name: "IX_Processes_OrderId1",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MachineId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ParametersId",
                table: "ProcessParameters");

            migrationBuilder.DropColumn(
                name: "ProcessesId",
                table: "ProcessParameters");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "MachineId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Orders");
        }
    }
}
