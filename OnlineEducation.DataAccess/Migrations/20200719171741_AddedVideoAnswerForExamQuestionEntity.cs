using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineEducation.DataAccess.Migrations
{
    public partial class AddedVideoAnswerForExamQuestionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoAnswerForExamQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PublicId = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ExamQuestionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoAnswerForExamQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoAnswerForExamQuestions_ExamQuestions_ExamQuestionId",
                        column: x => x.ExamQuestionId,
                        principalTable: "ExamQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoAnswerForExamQuestions_ExamQuestionId",
                table: "VideoAnswerForExamQuestions",
                column: "ExamQuestionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoAnswerForExamQuestions");
        }
    }
}
