using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Edutasksmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "ApplicationUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AcademicHoursDuration = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.UniqueConstraint("AK_Course_Title", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserCourse",
                columns: table => new
                {
                    CoursesAsMentorId = table.Column<Guid>(type: "uuid", nullable: false),
                    MentorsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserCourse", x => new { x.CoursesAsMentorId, x.MentorsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserCourse_ApplicationUser_MentorsId",
                        column: x => x.MentorsId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserCourse_Course_CoursesAsMentorId",
                        column: x => x.CoursesAsMentorId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserCourse1",
                columns: table => new
                {
                    CoursesAsStudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserCourse1", x => new { x.CoursesAsStudentId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserCourse1_ApplicationUser_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserCourse1_Course_CoursesAsStudentId",
                        column: x => x.CoursesAsStudentId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Section_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTask_ApplicationUser_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestTask_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lecture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    SectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EditAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lecture_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestTaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResultInPercents = table.Column<double>(type: "double precision", nullable: false),
                    TotalQuestionsCount = table.Column<int>(type: "integer", nullable: false),
                    CorrectAnswersCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Result_ApplicationUser_StudentId",
                        column: x => x.StudentId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Result_TestTask_TestTaskId",
                        column: x => x.TestTaskId,
                        principalTable: "TestTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerVariant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerVariant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    TestTaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    CorrectVariantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_AnswerVariant_CorrectVariantId",
                        column: x => x.CorrectVariantId,
                        principalTable: "AnswerVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Question_TestTask_TestTaskId",
                        column: x => x.TestTaskId,
                        principalTable: "TestTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_Username",
                table: "ApplicationUser",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerVariant_QuestionId",
                table: "AnswerVariant",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserCourse_MentorsId",
                table: "ApplicationUserCourse",
                column: "MentorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserCourse1_StudentsId",
                table: "ApplicationUserCourse1",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecture_SectionId",
                table: "Lecture",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_CorrectVariantId",
                table: "Question",
                column: "CorrectVariantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_TestTaskId",
                table: "Question",
                column: "TestTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_StudentId",
                table: "Result",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_TestTaskId",
                table: "Result",
                column: "TestTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_CourseId",
                table: "Section",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_AuthorId",
                table: "TestTask",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTask_CourseId",
                table: "TestTask",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerVariant_Question_QuestionId",
                table: "AnswerVariant",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerVariant_Question_QuestionId",
                table: "AnswerVariant");

            migrationBuilder.DropTable(
                name: "ApplicationUserCourse");

            migrationBuilder.DropTable(
                name: "ApplicationUserCourse1");

            migrationBuilder.DropTable(
                name: "Lecture");

            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "AnswerVariant");

            migrationBuilder.DropTable(
                name: "TestTask");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_Username",
                table: "ApplicationUser");

            migrationBuilder.RenameTable(
                name: "ApplicationUser",
                newName: "Profiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");
        }
    }
}
