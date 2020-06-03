using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blogs.Persistency.Migrations
{
    public partial class Comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    PostID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ApiKey", "ApiSecret", "Name" },
                values: new object[] { "189c2743-f7d7-4c26-b975-3d8a6add943a", "d5943b9b-4e04-42e9-a3e3-b5555ef91ee4", "Bruno" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "ApiKey", "ApiSecret", "Name" },
                values: new object[] { 2, "b29798c0-9311-4ac9-84e9-35151ee6c0f1", "8be748c3-c64c-41e3-b998-dfc05f9e8025", "Gabi" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "ApiKey", "ApiSecret", "Name" },
                values: new object[] { 3, "4524c37a-7122-4e6e-b5f5-dfe04214e72a", "c9c3d85e-aacf-4d53-aa03-c0efba81d8fc", "Nohan" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "ApiKey", "ApiSecret", "Name" },
                values: new object[] { 4, "d685f9f3-9626-47b9-b12d-192255b82900", "9cf43693-7cb7-478e-b168-044b93da28a0", "Ricardo" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostID",
                table: "Comments",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ApiKey", "ApiSecret" },
                values: new object[] { "1fc6529e-ecac-40b6-b7c8-ad815303a57c", "52c7ecfd-dea1-4865-a43f-16f70dfa26e6" });
        }
    }
}
