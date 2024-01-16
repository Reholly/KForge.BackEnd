using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addedauditsmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "TestTask",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "TestTask",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastlyEditedAtUtc",
                table: "TestTask",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Section",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "Section",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastlyEditedAtUtc",
                table: "Section",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Result",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "Result",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastlyEditedAtUtc",
                table: "Result",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Question",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "Question",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastlyEditedAtUtc",
                table: "Question",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Lecture",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "Lecture",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastlyEditedAtUtc",
                table: "Lecture",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Course",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "Course",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastlyEditedAtUtc",
                table: "Course",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "ApplicationUser",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "ApplicationUser",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastlyEditedAtUtc",
                table: "ApplicationUser",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "AnswerVariant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "AnswerVariant",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastlyEditedAtUtc",
                table: "AnswerVariant",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "LastlyEditedAtUtc",
                table: "TestTask");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "LastlyEditedAtUtc",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "LastlyEditedAtUtc",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "LastlyEditedAtUtc",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "Lecture");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "Lecture");

            migrationBuilder.DropColumn(
                name: "LastlyEditedAtUtc",
                table: "Lecture");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "LastlyEditedAtUtc",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "LastlyEditedAtUtc",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "AnswerVariant");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "AnswerVariant");

            migrationBuilder.DropColumn(
                name: "LastlyEditedAtUtc",
                table: "AnswerVariant");
        }
    }
}
