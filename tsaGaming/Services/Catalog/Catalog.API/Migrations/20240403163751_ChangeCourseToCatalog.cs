using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCourseToCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Course_CourseId",
                schema: "cat",
                table: "Lesson");

            migrationBuilder.DropTable(
                name: "Course",
                schema: "cat");

            migrationBuilder.DropSequence(
                name: "courseseq",
                schema: "cat");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                schema: "cat",
                table: "Lesson",
                newName: "CatalogId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_CourseId",
                schema: "cat",
                table: "Lesson",
                newName: "IX_Lesson_CatalogId");

            migrationBuilder.CreateSequence(
                name: "catalogseq",
                schema: "cat",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Catalog",
                schema: "cat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    IsTop = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "cat",
                table: "Catalog",
                columns: new[] { "Id", "CreatedAt", "Description", "DisplayName", "ImageUrl", "IsActive", "IsTop", "Name", "Price", "PriceVAT", "SortIndex", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Bé Học Chữ", "https://monkeymedia.vcdn.com.vn/upload/web/img/01-Game-hoc-chu-cai-tieng-Viet.jpg", true, true, "Easy Alphabet", 0m, 0m, 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Toán Thông Minh", "https://play-lh.googleusercontent.com/I1YRhi1oTYrFf1ZCbs3Dbx7J3Kj_h5SXICD8ObajQ5NOuYFJLNGCa1a774AD_z7D9w=w526-h296-rw", true, false, "Mathematical Thinking", 0m, 0m, 2, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Catalog_CatalogId",
                schema: "cat",
                table: "Lesson",
                column: "CatalogId",
                principalSchema: "cat",
                principalTable: "Catalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Catalog_CatalogId",
                schema: "cat",
                table: "Lesson");

            migrationBuilder.DropTable(
                name: "Catalog",
                schema: "cat");

            migrationBuilder.DropSequence(
                name: "catalogseq",
                schema: "cat");

            migrationBuilder.RenameColumn(
                name: "CatalogId",
                schema: "cat",
                table: "Lesson",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_CatalogId",
                schema: "cat",
                table: "Lesson",
                newName: "IX_Lesson_CourseId");

            migrationBuilder.CreateSequence(
                name: "courseseq",
                schema: "cat",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Course",
                schema: "cat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsTop = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "cat",
                table: "Course",
                columns: new[] { "Id", "CreatedAt", "Description", "DisplayName", "ImageUrl", "IsActive", "IsTop", "Name", "Price", "PriceVAT", "SortIndex", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Bé Học Chữ", "https://monkeymedia.vcdn.com.vn/upload/web/img/01-Game-hoc-chu-cai-tieng-Viet.jpg", true, true, "Easy Alphabet", 0m, 0m, 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Toán Thông Minh", "https://play-lh.googleusercontent.com/I1YRhi1oTYrFf1ZCbs3Dbx7J3Kj_h5SXICD8ObajQ5NOuYFJLNGCa1a774AD_z7D9w=w526-h296-rw", true, false, "Mathematical Thinking", 0m, 0m, 2, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Course_CourseId",
                schema: "cat",
                table: "Lesson",
                column: "CourseId",
                principalSchema: "cat",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
