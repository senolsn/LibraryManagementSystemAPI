using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interpreter",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.CreateTable(
                name: "Interpreters",
                columns: table => new
                {
                    InterpreterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InterpreterFirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InterpreterLastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interpreters", x => x.InterpreterId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BookInterpreter",
                columns: table => new
                {
                    BookInterpretersInterpreterId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InterpreterBooksBookId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInterpreter", x => new { x.BookInterpretersInterpreterId, x.InterpreterBooksBookId });
                    table.ForeignKey(
                        name: "FK_BookInterpreter_Books_InterpreterBooksBookId",
                        column: x => x.InterpreterBooksBookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookInterpreter_Interpreters_BookInterpretersInterpreterId",
                        column: x => x.BookInterpretersInterpreterId,
                        principalTable: "Interpreters",
                        principalColumn: "InterpreterId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BookInterpreter_InterpreterBooksBookId",
                table: "BookInterpreter",
                column: "InterpreterBooksBookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookInterpreter");

            migrationBuilder.DropTable(
                name: "Interpreters");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Books",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Interpreter",
                table: "Books",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
