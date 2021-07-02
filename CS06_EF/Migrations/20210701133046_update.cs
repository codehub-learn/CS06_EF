using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS06_EF.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AuthorPublisher",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 7, 1, 13, 30, 46, 524, DateTimeKind.Utc).AddTicks(8433),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AuthorPublisher",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 7, 1, 13, 30, 46, 524, DateTimeKind.Utc).AddTicks(8433));
        }
    }
}
