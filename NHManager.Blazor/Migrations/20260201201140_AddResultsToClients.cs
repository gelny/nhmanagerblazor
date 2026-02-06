using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NHManager.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class AddResultsToClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ClientQuestionnaireResults_ClientQuestionnaireId",
                table: "ClientQuestionnaireResults",
                column: "ClientQuestionnaireId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientQuestionnaireResults_ClientQuestionnaires_ClientQuestionnaireId",
                table: "ClientQuestionnaireResults",
                column: "ClientQuestionnaireId",
                principalTable: "ClientQuestionnaires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientQuestionnaireResults_ClientQuestionnaires_ClientQuestionnaireId",
                table: "ClientQuestionnaireResults");

            migrationBuilder.DropIndex(
                name: "IX_ClientQuestionnaireResults_ClientQuestionnaireId",
                table: "ClientQuestionnaireResults");
        }
    }
}
