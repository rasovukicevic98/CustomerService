using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerService.Migrations
{
    /// <inheritdoc />
    public partial class AddUsernameToDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Discounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEDd+HE+JFrQT8KjI5fzbwlqhe3t9sPnXCM4CSXI981R/Ttr4ZCwLOE2eK0XoipP6kA==");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEITDww9U3iWycyWoeRyHMLlo9Ey0m92gM/2P0wYiGWobnGDJlCF/NGgxMKYHXneKyw==");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 3,
                column: "Password",
                value: "AQAAAAIAAYagAAAAECZti7b9lI2EV0OrKbqDeSv6Bd43AI04XaB2SUch5RtWZQLNiwlP3pI2/hdLIqmicw==");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 4,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEAb8/CeIi5vnt10JZGUQKedYbNw6pxJ97EbQmpyj1LchCcSDyTKVVwaWtTqXT+Drdg==");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 5,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEBZ6S2xW2nUHG3DBUs1is7lH+qfHmMg5pMLA38Bp2tH0o0ktwd6I85seei6apctNsg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Discounts");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEJvatBHPIzmvf7ZVxlXbHvHG3pPyXL642+ezFnpiMsMToVUokDXfhOku1zwLkPZaWA==");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEBx+XW0Q2DtWNUPQ6SwOLysLuWzF5pzcUzZbu8Qk4C4zdDeGjiKsZzHCC/x2dB29kw==");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 3,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEBRvjTT9AFEtKtDikePxppt88YP7QvvqX5MEqj5erNc29Ae8GC+XfvOnYWidr0Yatw==");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 4,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEMgmECIOwAPlD7NUN2h7JyBl418GxEtutDR1EKfoFx6inf34FZqSUxsGuWlsXhG/oQ==");

            migrationBuilder.UpdateData(
                table: "Agents",
                keyColumn: "AgentId",
                keyValue: 5,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEIEJMME7miVTJxY8MHHZIyKD5/nZS9cSIh1vG6Yl3rX3g5Lm2ia95i1XohXXhhDZww==");
        }
    }
}
