using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LNCLibrary.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ConfirmationNumber",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ConfirmationNumber",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
