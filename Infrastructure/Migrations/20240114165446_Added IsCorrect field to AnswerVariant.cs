using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsCorrectfieldtoAnswerVariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_AnswerVariant_CorrectVariantId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_CorrectVariantId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CorrectVariantId",
                table: "Question");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "AnswerVariant",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "AnswerVariant");

            migrationBuilder.AddColumn<Guid>(
                name: "CorrectVariantId",
                table: "Question",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Question_CorrectVariantId",
                table: "Question",
                column: "CorrectVariantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_AnswerVariant_CorrectVariantId",
                table: "Question",
                column: "CorrectVariantId",
                principalTable: "AnswerVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
