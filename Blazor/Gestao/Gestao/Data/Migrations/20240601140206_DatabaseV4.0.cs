using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestao.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseV40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceDate",
                table: "Accounts"
            );
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "BalanceDate",
                table: "Accounts",
                type: "datetimeoffset",
                nullable: false
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BalanceDate",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: false
            );
        }
    }
}
