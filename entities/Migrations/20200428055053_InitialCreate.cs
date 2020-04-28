using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace entities.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogBrandSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrandSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogTypeSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogTypeSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItemSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Price = table.Column<decimal>(maxLength: 10, nullable: false),
                    PictureFileName = table.Column<string>(nullable: true),
                    PictureUri = table.Column<string>(nullable: true),
                    CatalogTypeId = table.Column<Guid>(nullable: false),
                    CatalogBrandId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItemSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItemSet_CatalogBrandSet_CatalogBrandId",
                        column: x => x.CatalogBrandId,
                        principalTable: "CatalogBrandSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogItemSet_CatalogTypeSet_CatalogTypeId",
                        column: x => x.CatalogTypeId,
                        principalTable: "CatalogTypeSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItemSet_CatalogBrandId",
                table: "CatalogItemSet",
                column: "CatalogBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItemSet_CatalogTypeId",
                table: "CatalogItemSet",
                column: "CatalogTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogItemSet");

            migrationBuilder.DropTable(
                name: "CatalogBrandSet");

            migrationBuilder.DropTable(
                name: "CatalogTypeSet");
        }
    }
}
