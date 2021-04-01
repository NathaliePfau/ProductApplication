using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductApplication.Infra.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SUPPLIERS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(18)", unicode: false, maxLength: 18, nullable: false),
                    Trade = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    AddressZipCode = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    AddressNeighborhood = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    AddressStreet = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    AddressCity = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    AddressState = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    AddressNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    AddressComplement = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    ContactEmail = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Telephone = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUPPLIERS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    IdSupplier = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CATEGORIES_SUPPLIERS_IdSupplier",
                        column: x => x.IdSupplier,
                        principalTable: "SUPPLIERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIES_IdSupplier",
                table: "CATEGORIES",
                column: "IdSupplier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CATEGORIES");

            migrationBuilder.DropTable(
                name: "SUPPLIERS");
        }
    }
}
