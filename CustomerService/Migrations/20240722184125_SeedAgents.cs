using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerService.Migrations
{
    /// <inheritdoc />
    public partial class SeedAgents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "AgentId", "Name", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "Agent One", "AQAAAAIAAYagAAAAEKjt9ShHhDiqGEJWk2zhqgXCn84gEeYsjElwkgs2G7uQCJ9OBhKajKd3DfKcCcSuQA==", "agent1" },
                    { 2, "Agent Two", "AQAAAAIAAYagAAAAEHTUAruJASBw/I8Pzrf/Oeh5DPVhMz2CtN+VGT8TfVOCDMgUUCKEIlwMvIqRaMYRQQ==", "agent2" },
                    { 3, "Agent Three", "AQAAAAIAAYagAAAAEO2ensguN3s1qYZv8O3rxJlq1rzug/OBD3OTBGnsOZtMNf2LGhxQ+5Ra/URLDthnLw==", "agent3" },
                    { 4, "Agent Four", "AQAAAAIAAYagAAAAEPag72ZcnJmBl6cZzv3q4dmgXX1TtiFDAm64m0EAiQXNd+6hLMUueuru8f/MOOkZjQ==", "agent4" },
                    { 5, "Agent Five", "AQAAAAIAAYagAAAAEBZdxYRKYLZejbKD2IJMAMVU7aGmqZf6Jj4NaTo1YzKNz3nk9l1noDnL/4ddrcbGaQ==", "agent5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 5);
        }
    }
}
