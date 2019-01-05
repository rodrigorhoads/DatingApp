using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class ExtendUserClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConhecidoComo",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Criado",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interesses",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introducao",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Nascimento",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcurandoPor",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaActividade",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateAdd = table.Column<DateTime>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConhecidoComo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Criado",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Interesses",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Introducao",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Nascimento",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProcurandoPor",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UltimaActividade",
                table: "Users");
        }
    }
}
