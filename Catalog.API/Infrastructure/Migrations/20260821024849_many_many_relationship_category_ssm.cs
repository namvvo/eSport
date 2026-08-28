using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace eSport.Catalog.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class many_many_relationship_category_ssm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Season_Stage_Mapping",
                table: "Season_Stage_Mapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category_SSM_Mapping",
                table: "Category_SSM_Mapping");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Category_SSM_Mapping");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Season_Stage_Mapping",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Season_Stage_Mapping",
                table: "Season_Stage_Mapping",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category_SSM_Mapping",
                table: "Category_SSM_Mapping",
                columns: new[] { "CategoryId", "SeasonStageId" });

            migrationBuilder.CreateIndex(
                name: "IX_Season_Stage_Mapping_SeasonId_StageId",
                table: "Season_Stage_Mapping",
                columns: new[] { "SeasonId", "StageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_SSM_Mapping_SeasonStageId",
                table: "Category_SSM_Mapping",
                column: "SeasonStageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_SSM_Mapping_Category_CategoryId",
                table: "Category_SSM_Mapping",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_SSM_Mapping_Season_Stage_Mapping_SeasonStageId",
                table: "Category_SSM_Mapping",
                column: "SeasonStageId",
                principalTable: "Season_Stage_Mapping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_SSM_Mapping_Category_CategoryId",
                table: "Category_SSM_Mapping");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_SSM_Mapping_Season_Stage_Mapping_SeasonStageId",
                table: "Category_SSM_Mapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Season_Stage_Mapping",
                table: "Season_Stage_Mapping");

            migrationBuilder.DropIndex(
                name: "IX_Season_Stage_Mapping_SeasonId_StageId",
                table: "Season_Stage_Mapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category_SSM_Mapping",
                table: "Category_SSM_Mapping");

            migrationBuilder.DropIndex(
                name: "IX_Category_SSM_Mapping_SeasonStageId",
                table: "Category_SSM_Mapping");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Season_Stage_Mapping",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Category_SSM_Mapping",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Season_Stage_Mapping",
                table: "Season_Stage_Mapping",
                columns: new[] { "SeasonId", "StageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category_SSM_Mapping",
                table: "Category_SSM_Mapping",
                columns: new[] { "Id", "CategoryId", "SeasonStageId" });
        }
    }
}
