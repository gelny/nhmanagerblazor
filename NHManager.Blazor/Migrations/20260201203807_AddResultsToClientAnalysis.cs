using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NHManager.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class AddResultsToClientAnalysis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ClientAnalysisResults_ClientAnalysisId",
                table: "ClientAnalysisResults",
                column: "ClientAnalysisId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAnalysisResults_ClientAnalysis_ClientAnalysisId",
                table: "ClientAnalysisResults",
                column: "ClientAnalysisId",
                principalTable: "ClientAnalysis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientAnalysisResults_ClientAnalysis_ClientAnalysisId",
                table: "ClientAnalysisResults");

            migrationBuilder.DropIndex(
                name: "IX_ClientAnalysisResults_ClientAnalysisId",
                table: "ClientAnalysisResults");
        }
    }
}
