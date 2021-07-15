using Microsoft.EntityFrameworkCore.Migrations;

namespace VendingMachine.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemEntities",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    itemName = table.Column<string>(type: "TEXT", nullable: true),
                    price = table.Column<float>(type: "REAL", nullable: false),
                    unit = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemEntities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "VendingMachineEntities",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    machineValidity = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendingMachineEntities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WorkerEntities",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    workerIdOriginal = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerEntities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "VendingMachineItemEntities",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    vendingMachineId = table.Column<int>(type: "INTEGER", nullable: false),
                    itemId = table.Column<int>(type: "INTEGER", nullable: false),
                    amountOfItem = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendingMachineItemEntities", x => x.id);
                    table.ForeignKey(
                        name: "FK_VendingMachineItemEntities_ItemEntities_itemId",
                        column: x => x.itemId,
                        principalTable: "ItemEntities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendingMachineItemEntities_VendingMachineEntities_vendingMachineId",
                        column: x => x.vendingMachineId,
                        principalTable: "VendingMachineEntities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobMachineWorkerEntities",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    vendingMachineId = table.Column<int>(type: "INTEGER", nullable: false),
                    workerId = table.Column<int>(type: "INTEGER", nullable: false),
                    intworkerIdOriginal = table.Column<int>(type: "INTEGER", nullable: false),
                    finished = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobMachineWorkerEntities", x => x.id);
                    table.ForeignKey(
                        name: "FK_JobMachineWorkerEntities_VendingMachineEntities_vendingMachineId",
                        column: x => x.vendingMachineId,
                        principalTable: "VendingMachineEntities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobMachineWorkerEntities_WorkerEntities_workerId",
                        column: x => x.workerId,
                        principalTable: "WorkerEntities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobMachineWorkerEntities_vendingMachineId",
                table: "JobMachineWorkerEntities",
                column: "vendingMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_JobMachineWorkerEntities_workerId",
                table: "JobMachineWorkerEntities",
                column: "workerId");

            migrationBuilder.CreateIndex(
                name: "IX_VendingMachineItemEntities_itemId",
                table: "VendingMachineItemEntities",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_VendingMachineItemEntities_vendingMachineId",
                table: "VendingMachineItemEntities",
                column: "vendingMachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobMachineWorkerEntities");

            migrationBuilder.DropTable(
                name: "VendingMachineItemEntities");

            migrationBuilder.DropTable(
                name: "WorkerEntities");

            migrationBuilder.DropTable(
                name: "ItemEntities");

            migrationBuilder.DropTable(
                name: "VendingMachineEntities");
        }
    }
}
