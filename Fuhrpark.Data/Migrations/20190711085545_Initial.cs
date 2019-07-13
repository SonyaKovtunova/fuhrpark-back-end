using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fuhrpark.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 200, nullable: false),
                    LastName = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 100, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    RefreshTokenExpires = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbEngineOil",
                columns: table => new
                {
                    id_engine_oil_car = table.Column<int>(maxLength: 8, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    engine_oil_name = table.Column<string>(maxLength: 255, nullable: false),
                    engine_oil_date_create = table.Column<DateTime>(nullable: false),
                    engine_oil_date_update = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbEngineOil", x => x.id_engine_oil_car);
                });

            migrationBuilder.CreateTable(
                name: "tbFuel",
                columns: table => new
                {
                    id_fuel_car = table.Column<int>(maxLength: 8, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    fuel_name = table.Column<string>(maxLength: 255, nullable: false),
                    fuel_date_create = table.Column<DateTime>(nullable: false),
                    fuel_date_update = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbFuel", x => x.id_fuel_car);
                });

            migrationBuilder.CreateTable(
                name: "tbGearOil",
                columns: table => new
                {
                    id_gear_oil_car = table.Column<int>(maxLength: 8, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gear_oil_name = table.Column<string>(maxLength: 255, nullable: false),
                    gear_oil_date_create = table.Column<DateTime>(nullable: false),
                    gear_oil_date_update = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbGearOil", x => x.id_gear_oil_car);
                });

            migrationBuilder.CreateTable(
                name: "tbManufacturer",
                columns: table => new
                {
                    id_manufacturer_car = table.Column<int>(maxLength: 8, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    manufacturer_name = table.Column<string>(maxLength: 255, nullable: false),
                    manufacturer_date_create = table.Column<DateTime>(nullable: false),
                    manufacturer_date_update = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbManufacturer", x => x.id_manufacturer_car);
                });

            migrationBuilder.CreateTable(
                name: "tbTyp",
                columns: table => new
                {
                    id_typ_car = table.Column<int>(maxLength: 8, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    typ_name = table.Column<string>(maxLength: 255, nullable: false),
                    typ_date_create = table.Column<DateTime>(nullable: false),
                    typ_date_update = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbTyp", x => x.id_typ_car);
                });

            migrationBuilder.CreateTable(
                name: "tbUser",
                columns: table => new
                {
                    id_user_car = table.Column<int>(maxLength: 8, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    user_name = table.Column<string>(maxLength: 255, nullable: false),
                    user_date_create = table.Column<DateTime>(nullable: false),
                    user_date_update = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbUser", x => x.id_user_car);
                });

            migrationBuilder.CreateTable(
                name: "tbCarCore",
                columns: table => new
                {
                    id_car_core = table.Column<int>(maxLength: 8, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    car_reg_number = table.Column<string>(maxLength: 255, nullable: false),
                    id_typ_car = table.Column<int>(nullable: false),
                    id_manufacturer_car = table.Column<int>(nullable: false),
                    car_model = table.Column<string>(maxLength: 255, nullable: true),
                    car_color = table.Column<string>(maxLength: 255, nullable: true),
                    car_chassis_number = table.Column<string>(maxLength: 255, nullable: true),
                    car_decommissioned = table.Column<bool>(nullable: false),
                    id_fuel_car = table.Column<int>(nullable: false),
                    car_performace = table.Column<int>(maxLength: 8, nullable: true),
                    car_displacement = table.Column<int>(maxLength: 8, nullable: true),
                    car_max_speed = table.Column<int>(maxLength: 8, nullable: true),
                    car_weight = table.Column<int>(maxLength: 8, nullable: true),
                    car_engine_code = table.Column<string>(maxLength: 255, nullable: true),
                    id_engine_oil_car = table.Column<int>(nullable: false),
                    id_gear_oil_car = table.Column<int>(nullable: false),
                    car_production_date = table.Column<DateTime>(nullable: true),
                    car_registration_date = table.Column<DateTime>(nullable: true),
                    car_catalyst = table.Column<bool>(nullable: false),
                    car_hybrid_drive = table.Column<bool>(nullable: false),
                    car_location = table.Column<string>(maxLength: 255, nullable: true),
                    id_user_car = table.Column<int>(nullable: true),
                    car_date_create = table.Column<DateTime>(nullable: false),
                    car_date_update = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCarCore", x => x.id_car_core);
                    table.ForeignKey(
                        name: "FK_tbCarCore_tbManufacturer_id_manufacturer_car",
                        column: x => x.id_manufacturer_car,
                        principalTable: "tbManufacturer",
                        principalColumn: "id_manufacturer_car",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbCarCore_tbTyp_id_typ_car",
                        column: x => x.id_typ_car,
                        principalTable: "tbTyp",
                        principalColumn: "id_typ_car",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbCarCore_tbUser_id_user_car",
                        column: x => x.id_user_car,
                        principalTable: "tbUser",
                        principalColumn: "id_user_car",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbCarCore_tbEngineOil_id_engine_oil_car",
                        column: x => x.id_engine_oil_car,
                        principalTable: "tbEngineOil",
                        principalColumn: "id_engine_oil_car",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbCarCore_tbFuel_id_fuel_car",
                        column: x => x.id_fuel_car,
                        principalTable: "tbFuel",
                        principalColumn: "id_fuel_car",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbCarCore_tbGearOil_id_gear_oil_car",
                        column: x => x.id_gear_oil_car,
                        principalTable: "tbGearOil",
                        principalColumn: "id_gear_oil_car",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbCarCore_id_manufacturer_car",
                table: "tbCarCore",
                column: "id_manufacturer_car");

            migrationBuilder.CreateIndex(
                name: "IX_tbCarCore_id_typ_car",
                table: "tbCarCore",
                column: "id_typ_car");

            migrationBuilder.CreateIndex(
                name: "IX_tbCarCore_id_user_car",
                table: "tbCarCore",
                column: "id_user_car");

            migrationBuilder.CreateIndex(
                name: "IX_tbCarCore_id_engine_oil_car",
                table: "tbCarCore",
                column: "id_engine_oil_car");

            migrationBuilder.CreateIndex(
                name: "IX_tbCarCore_id_fuel_car",
                table: "tbCarCore",
                column: "id_fuel_car");

            migrationBuilder.CreateIndex(
                name: "IX_tbCarCore_id_gear_oil_car",
                table: "tbCarCore",
                column: "id_gear_oil_car");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "tbCarCore");

            migrationBuilder.DropTable(
                name: "tbManufacturer");

            migrationBuilder.DropTable(
                name: "tbTyp");

            migrationBuilder.DropTable(
                name: "tbUser");

            migrationBuilder.DropTable(
                name: "tbEngineOil");

            migrationBuilder.DropTable(
                name: "tbFuel");

            migrationBuilder.DropTable(
                name: "tbGearOil");
        }
    }
}
