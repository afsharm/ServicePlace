using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicePlace.Data.Migrations
{
    /// <inheritdoc />
    public partial class ServiceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Providers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Providers_ServiceId",
                table: "Providers",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_Service_ServiceId",
                table: "Providers",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_Service_ServiceId",
                table: "Providers");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Providers_ServiceId",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Providers");
        }
    }
}
