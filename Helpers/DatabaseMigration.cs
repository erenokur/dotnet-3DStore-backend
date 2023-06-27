using Microsoft.EntityFrameworkCore.Migrations;

public class InitialMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("MySQL:AutoIncrement", true),
                UserName = table.Column<string>(nullable: false),
                Email = table.Column<string>(nullable: false),
                Password = table.Column<string>(nullable: false),
                Created = table.Column<DateTime>(nullable: false),
                Role = table.Column<string>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("MySQL:AutoIncrement", true),
                Name = table.Column<string>(maxLength: 255, nullable: false),
                Description = table.Column<string>(maxLength: 255, nullable: false),
                Image = table.Column<string>(maxLength: 255, nullable: false),
                Price = table.Column<int>(nullable: false),
                Quantity = table.Column<int>(nullable: false),
                Created = table.Column<DateTime>(nullable: false),
                SellerUserId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Orders",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("MySQL:AutoIncrement", true),
                CustomerUserId = table.Column<int>(nullable: false),
                SellerUserId = table.Column<int>(nullable: false),
                Created = table.Column<DateTime>(nullable: false),
                Status = table.Column<string>(nullable: false),
                DeliveryCompany = table.Column<string>(nullable: false),
                DeliveryCompanyCode = table.Column<string>(nullable: false),
                DeliveryAddress = table.Column<string>(nullable: false),
                EstimatedDeliveryDate = table.Column<DateTime>(nullable: false),
                OrderNote = table.Column<string>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "OrderItems",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("MySQL:AutoIncrement", true),
                OrderId = table.Column<int>(nullable: false),
                ProductId = table.Column<int>(nullable: false),
                Quantity = table.Column<int>(nullable: false),
                Price = table.Column<int>(nullable: false),
                Created = table.Column<DateTime>(nullable: false),
                Status = table.Column<string>(nullable: false),
                OrderNote = table.Column<string>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderItems", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "OrderItems");

        migrationBuilder.DropTable(
            name: "Orders");

        migrationBuilder.DropTable(
            name: "Products");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
