using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SequencesWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SequenceInts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Integers",
                table: "Sequences");

            migrationBuilder.CreateTable(
                name: "SequenceInt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    SequenceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SequenceInt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SequenceInt_Sequences_SequenceId",
                        column: x => x.SequenceId,
                        principalTable: "Sequences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SequenceInt_SequenceId",
                table: "SequenceInt",
                column: "SequenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SequenceInt");

            migrationBuilder.AddColumn<string>(
                name: "Integers",
                table: "Sequences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
