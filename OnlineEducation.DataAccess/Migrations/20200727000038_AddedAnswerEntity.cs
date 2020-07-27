using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineEducation.DataAccess.Migrations
{
    public partial class AddedAnswerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PublicId = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    AnswerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerImages_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerVideos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PublicId = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    AnswerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerVideos_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerImages_AnswerId",
                table: "AnswerImages",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerVideos_AnswerId",
                table: "AnswerVideos",
                column: "AnswerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerImages");

            migrationBuilder.DropTable(
                name: "AnswerVideos");

            migrationBuilder.DropTable(
                name: "Answers");
        }
    }
}
