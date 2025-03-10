using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WMAPI.Migrations
{
    public partial class UpdateOutAndInCodeLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse_In",
                columns: table => new
                {
                    in_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    in_code = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    supplier_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    total_price_in = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    in_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Warehous__1CD08BE9EAD10864", x => x.in_id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse_Out",
                columns: table => new
                {
                    out_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    out_code = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    recipient_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    total_price_out = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    out_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Warehous__D7CC77D8C0C004E5", x => x.out_id);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    inventory_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity_in_stock = table.Column<int>(type: "int", nullable: false),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.inventory_id);
                    table.ForeignKey(
                        name: "FK__Inventory__produ__46E78A0C",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse_In_Details",
                columns: table => new
                {
                    in_detail_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    in_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity_in = table.Column<int>(type: "int", nullable: false),
                    price_in = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Warehous__8F4C7547D8B15D35", x => x.in_detail_id);
                    table.ForeignKey(
                        name: "FK__Warehouse__in_id__3B75D760",
                        column: x => x.in_id,
                        principalTable: "Warehouse_In",
                        principalColumn: "in_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Warehouse__produ__3C69FB99",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse_Out_Details",
                columns: table => new
                {
                    out_detail_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    out_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity_out = table.Column<int>(type: "int", nullable: false),
                    price_out = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Warehous__0DC07A7FCD119C79", x => x.out_detail_id);
                    table.ForeignKey(
                        name: "FK__Warehouse__out_i__4222D4EF",
                        column: x => x.out_id,
                        principalTable: "Warehouse_Out",
                        principalColumn: "out_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Warehouse__produ__4316F928",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Inventor__47027DF4096D455F",
                table: "Inventory",
                column: "product_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Warehous__CE5B7C52C81F4C70",
                table: "Warehouse_In",
                column: "in_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_In_Details_in_id",
                table: "Warehouse_In_Details",
                column: "in_id");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_In_Details_product_id",
                table: "Warehouse_In_Details",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Warehous__BD9478991FD91B95",
                table: "Warehouse_Out",
                column: "out_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_Out_Details_out_id",
                table: "Warehouse_Out_Details",
                column: "out_id");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_Out_Details_product_id",
                table: "Warehouse_Out_Details",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Warehouse_In_Details");

            migrationBuilder.DropTable(
                name: "Warehouse_Out_Details");

            migrationBuilder.DropTable(
                name: "Warehouse_In");

            migrationBuilder.DropTable(
                name: "Warehouse_Out");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
