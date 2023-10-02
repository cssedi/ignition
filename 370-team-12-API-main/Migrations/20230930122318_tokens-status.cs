using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMWIgnition_API.Migrations
{
    public partial class tokensstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaximumTokens",
                columns: table => new
                {
                    MaximumTokensId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tokens = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaximumTokens", x => x.MaximumTokensId);
                });

            migrationBuilder.InsertData(
                table: "ChallengeInstanceStatus",
                columns: new[] { "ChallengeInstanceStatusId", "Name" },
                values: new object[,]
                {
                    { 4, "Submited" },
                    { 5, "Approved" },
                    { 6, "Declined" }
                });

            migrationBuilder.InsertData(
                table: "MaximumTokens",
                columns: new[] { "MaximumTokensId", "Tokens" },
                values: new object[] { 1, 100 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaximumTokens");

            migrationBuilder.DeleteData(
                table: "ChallengeInstanceStatus",
                keyColumn: "ChallengeInstanceStatusId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ChallengeInstanceStatus",
                keyColumn: "ChallengeInstanceStatusId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ChallengeInstanceStatus",
                keyColumn: "ChallengeInstanceStatusId",
                keyValue: 6);
        }
    }
}
