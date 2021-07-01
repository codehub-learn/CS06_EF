using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CS06_EF.Migrations
{
    public partial class payload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorPublisher",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    PublishersPublisherKey = table.Column<int>(type: "int", nullable: false),
                    PublisherKey = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorPublisher", x => new { x.AuthorId, x.PublishersPublisherKey });
                    table.ForeignKey(
                        name: "FK_AuthorPublisher_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorPublisher_Publishers_PublishersPublisherKey",
                        column: x => x.PublishersPublisherKey,
                        principalTable: "Publishers",
                        principalColumn: "PublisherKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorPublisher_PublishersPublisherKey",
                table: "AuthorPublisher",
                column: "PublishersPublisherKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorPublisher");
        }
    }
}
