using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS06_EF.Migrations
{
    public partial class synopsischanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PubDate",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AuthorPublisher",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 2, 12, 42, 29, 964, DateTimeKind.Utc).AddTicks(3413),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 1, 13, 30, 46, 524, DateTimeKind.Utc).AddTicks(8433));

            migrationBuilder.CreateTable(
                name: "Synopses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WriterFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WriterLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Synopses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Synopses_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Synopses_BookId",
                table: "Synopses",
                column: "BookId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Synopses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PubDate",
                table: "Books",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AuthorPublisher",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 1, 13, 30, 46, 524, DateTimeKind.Utc).AddTicks(8433),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 2, 12, 42, 29, 964, DateTimeKind.Utc).AddTicks(3413));
        }
    }
}
