using Microsoft.EntityFrameworkCore.Migrations;

namespace CS06_EF.Migrations
{
    public partial class keyfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorPublisher_Publishers_PublishersPublisherKey",
                table: "AuthorPublisher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorPublisher",
                table: "AuthorPublisher");

            migrationBuilder.DropIndex(
                name: "IX_AuthorPublisher_PublishersPublisherKey",
                table: "AuthorPublisher");

            migrationBuilder.DropColumn(
                name: "PublishersPublisherKey",
                table: "AuthorPublisher");

            migrationBuilder.RenameColumn(
                name: "PublisherKey",
                table: "AuthorPublisher",
                newName: "PublisherPublisherKey");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorPublisher",
                table: "AuthorPublisher",
                columns: new[] { "AuthorId", "PublisherPublisherKey" });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorPublisher_PublisherPublisherKey",
                table: "AuthorPublisher",
                column: "PublisherPublisherKey");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorPublisher_Publishers_PublisherPublisherKey",
                table: "AuthorPublisher",
                column: "PublisherPublisherKey",
                principalTable: "Publishers",
                principalColumn: "PublisherKey",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorPublisher_Publishers_PublisherPublisherKey",
                table: "AuthorPublisher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorPublisher",
                table: "AuthorPublisher");

            migrationBuilder.DropIndex(
                name: "IX_AuthorPublisher_PublisherPublisherKey",
                table: "AuthorPublisher");

            migrationBuilder.RenameColumn(
                name: "PublisherPublisherKey",
                table: "AuthorPublisher",
                newName: "PublisherKey");

            migrationBuilder.AddColumn<int>(
                name: "PublishersPublisherKey",
                table: "AuthorPublisher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorPublisher",
                table: "AuthorPublisher",
                columns: new[] { "AuthorId", "PublishersPublisherKey" });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorPublisher_PublishersPublisherKey",
                table: "AuthorPublisher",
                column: "PublishersPublisherKey");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorPublisher_Publishers_PublishersPublisherKey",
                table: "AuthorPublisher",
                column: "PublishersPublisherKey",
                principalTable: "Publishers",
                principalColumn: "PublisherKey",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
