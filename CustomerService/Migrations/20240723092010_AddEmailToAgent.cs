using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerService.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToAgent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Agents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 1,
                columns: new[] { "Email", "Password" },
                values: new object[] { "agent1@example.com", "AQAAAAIAAYagAAAAEJvatBHPIzmvf7ZVxlXbHvHG3pPyXL642+ezFnpiMsMToVUokDXfhOku1zwLkPZaWA==" });

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 2,
                columns: new[] { "Email", "Password" },
                values: new object[] { "agent2@example.com", "AQAAAAIAAYagAAAAEBx+XW0Q2DtWNUPQ6SwOLysLuWzF5pzcUzZbu8Qk4C4zdDeGjiKsZzHCC/x2dB29kw==" });

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 3,
                columns: new[] { "Email", "Password" },
                values: new object[] { "agent3@example.com", "AQAAAAIAAYagAAAAEBRvjTT9AFEtKtDikePxppt88YP7QvvqX5MEqj5erNc29Ae8GC+XfvOnYWidr0Yatw==" });

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 4,
                columns: new[] { "Email", "Password" },
                values: new object[] { "agent4@example.com", "AQAAAAIAAYagAAAAEMgmECIOwAPlD7NUN2h7JyBl418GxEtutDR1EKfoFx6inf34FZqSUxsGuWlsXhG/oQ==" });

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 5,
                columns: new[] { "Email", "Password" },
                values: new object[] { "agent5@example.com", "AQAAAAIAAYagAAAAEIEJMME7miVTJxY8MHHZIyKD5/nZS9cSIh1vG6Yl3rX3g5Lm2ia95i1XohXXhhDZww==" });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_Email",
                table: "Agents",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agents_Email",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEKjt9ShHhDiqGEJWk2zhqgXCn84gEeYsjElwkgs2G7uQCJ9OBhKajKd3DfKcCcSuQA==");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEHTUAruJASBw/I8Pzrf/Oeh5DPVhMz2CtN+VGT8TfVOCDMgUUCKEIlwMvIqRaMYRQQ==");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 3,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEO2ensguN3s1qYZv8O3rxJlq1rzug/OBD3OTBGnsOZtMNf2LGhxQ+5Ra/URLDthnLw==");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 4,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEPag72ZcnJmBl6cZzv3q4dmgXX1TtiFDAm64m0EAiQXNd+6hLMUueuru8f/MOOkZjQ==");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 5,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEBZdxYRKYLZejbKD2IJMAMVU7aGmqZf6Jj4NaTo1YzKNz3nk9l1noDnL/4ddrcbGaQ==");
        }
    }
}
