using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    /// <inheritdoc />
    public partial class RenameIngredientsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_ingredients_IngredientsId",
                table: "IngredientPizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ingredients",
                table: "ingredients");

            migrationBuilder.RenameTable(
                name: "ingredients",
                newName: "Ingredients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Ingredients_IngredientsId",
                table: "IngredientPizza",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Ingredients_IngredientsId",
                table: "IngredientPizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "ingredients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingredients",
                table: "ingredients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_ingredients_IngredientsId",
                table: "IngredientPizza",
                column: "IngredientsId",
                principalTable: "ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
