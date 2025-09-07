using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TODO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalProgress = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoModSubTaskModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ProjectTask_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoModSubTaskModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoModSubTaskModels_ProjectTasks_ProjectTask_Id",
                        column: x => x.ProjectTask_Id,
                        principalTable: "ProjectTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_ExpiryDate",
                table: "ProjectTasks",
                column: "ExpiryDate");

            migrationBuilder.CreateIndex(
                name: "IX_TodoModSubTaskModels_ProjectTask_Id",
                table: "TodoModSubTaskModels",
                column: "ProjectTask_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoModSubTaskModels");

            migrationBuilder.DropTable(
                name: "ProjectTasks");
        }
    }
}
