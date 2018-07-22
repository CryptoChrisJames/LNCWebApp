using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LNCLibrary.Migrations
{
    public partial class Initialv11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Orders_OrderID",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Orders_CurrentOrderID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CurrentOrderID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_OrderID",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CurrentOrderID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "CartItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentOrderID",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "CartItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CurrentOrderID",
                table: "Orders",
                column: "CurrentOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_OrderID",
                table: "CartItems",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Orders_OrderID",
                table: "CartItems",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Orders_CurrentOrderID",
                table: "Orders",
                column: "CurrentOrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
