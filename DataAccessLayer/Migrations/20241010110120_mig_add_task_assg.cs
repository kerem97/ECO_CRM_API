using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_add_task_assg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AbasId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity1 = table.Column<int>(type: "int", nullable: false),
                    Quantity2 = table.Column<int>(type: "int", nullable: true),
                    Quantity3 = table.Column<int>(type: "int", nullable: true),
                    Quantity4 = table.Column<int>(type: "int", nullable: true),
                    Quantity5 = table.Column<int>(type: "int", nullable: true),
                    Quantity6 = table.Column<int>(type: "int", nullable: true),
                    Quantity7 = table.Column<int>(type: "int", nullable: true),
                    Quantity8 = table.Column<int>(type: "int", nullable: true),
                    Quantity9 = table.Column<int>(type: "int", nullable: true),
                    Quantity10 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAssignments_CustomerOperations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "CustomerOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_OperationId",
                table: "TaskAssignments",
                column: "OperationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_UserId",
                table: "TaskAssignments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskAssignments");
        }
    }
}
