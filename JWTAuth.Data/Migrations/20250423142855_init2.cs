using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAuth.Data.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$E0TBI7UCPH6keoSl3lD2Y.BUOO9NcSPf19APOE9TfVL3QDsBKnze6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$uNRN.R89zdeAALwLTwOW4eJgTlGhDjYfNfpbLxcuJwHF.EI9lbXrC");
        }
    }
}
