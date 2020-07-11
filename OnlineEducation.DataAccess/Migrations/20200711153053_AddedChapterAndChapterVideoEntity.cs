using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineEducation.DataAccess.Migrations
{
    public partial class AddedChapterAndChapterVideoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LessonId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapters_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChapterVideos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PublicId = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ChapterId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterVideos_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_LessonId",
                table: "Chapters",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterVideos_ChapterId",
                table: "ChapterVideos",
                column: "ChapterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChapterVideos");

            migrationBuilder.DropTable(
                name: "Chapters");
        }
    }
}
