using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TiendaServicios.Api.Libro.Migrations
{
    public partial class UpdateClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.DropPrimaryKey(
                name: "PK_LibreriaMaterial",
                table: "LibreriaMaterial");
            

            migrationBuilder.DropColumn(
                name: "LibreriaMaterialId",
                table: "LibreriaMaterial");
         
            migrationBuilder.AddColumn<Guid>(
                name: "LibroMaterialId",
                table: "LibreriaMaterial",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));         
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_LibroMaterial",
                table: "LibroMaterial",
                column: "LibroMaterialId");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LibreriaMaterial",
                table: "LibreriaMaterial");

            migrationBuilder.DropColumn(
                name: "LibroMaterialId",
                table: "LibreriaMaterial");

            migrationBuilder.AddColumn<Guid>(
                name: "LibreriaMaterialId",
                table: "LibreriaMaterial",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibreriaMaterial",
                table: "LibreriaMaterial",
                column: "LibreriaMaterialId");
        }
    }
}
