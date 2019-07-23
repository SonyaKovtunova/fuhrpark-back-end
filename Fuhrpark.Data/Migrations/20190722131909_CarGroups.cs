using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fuhrpark.Data.Migrations
{
    public partial class CarGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarSubgroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarSubgroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarInCarSubgroup",
                columns: table => new
                {
                    CarId = table.Column<int>(nullable: false),
                    CarSubgroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarInCarSubgroup", x => new { x.CarId, x.CarSubgroupId });
                    table.ForeignKey(
                        name: "FK_CarInCarSubgroup_tbCarCore_CarId",
                        column: x => x.CarId,
                        principalTable: "tbCarCore",
                        principalColumn: "id_car_core",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarInCarSubgroup_CarSubgroup_CarSubgroupId",
                        column: x => x.CarSubgroupId,
                        principalTable: "CarSubgroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarSubgroupInCarGroup",
                columns: table => new
                {
                    CarSubgroupId = table.Column<int>(nullable: false),
                    CarGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarSubgroupInCarGroup", x => new { x.CarSubgroupId, x.CarGroupId });
                    table.ForeignKey(
                        name: "FK_CarSubgroupInCarGroup_CarGroup_CarGroupId",
                        column: x => x.CarGroupId,
                        principalTable: "CarGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarSubgroupInCarGroup_CarSubgroup_CarSubgroupId",
                        column: x => x.CarSubgroupId,
                        principalTable: "CarSubgroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarInCarSubgroup_CarSubgroupId",
                table: "CarInCarSubgroup",
                column: "CarSubgroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CarSubgroupInCarGroup_CarGroupId",
                table: "CarSubgroupInCarGroup",
                column: "CarGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarInCarSubgroup");

            migrationBuilder.DropTable(
                name: "CarSubgroupInCarGroup");

            migrationBuilder.DropTable(
                name: "CarGroup");

            migrationBuilder.DropTable(
                name: "CarSubgroup");
        }
    }
}
