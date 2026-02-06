using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NHManager.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class AddClientMeasurementModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ClientMeasurementResults_ClientMeasurementId",
                table: "ClientMeasurementResults",
                column: "ClientMeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientMeasurementResults_ClientMeasurements_ClientMeasurementId",
                table: "ClientMeasurementResults",
                column: "ClientMeasurementId",
                principalTable: "ClientMeasurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientMeasurementResults_ClientMeasurements_ClientMeasurementId",
                table: "ClientMeasurementResults");

            migrationBuilder.DropIndex(
                name: "IX_ClientMeasurementResults_ClientMeasurementId",
                table: "ClientMeasurementResults");
        }
    }
}
