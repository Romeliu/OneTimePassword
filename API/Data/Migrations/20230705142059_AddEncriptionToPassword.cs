using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEncriptionToPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EncodedPassword",
                table: "TemporaryPasswords",
                newName: "EncodedPasswordSalt");

            migrationBuilder.AddColumn<string>(
                name: "EncodedPasswordHash",
                table: "TemporaryPasswords",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncodedPasswordHash",
                table: "TemporaryPasswords");

            migrationBuilder.RenameColumn(
                name: "EncodedPasswordSalt",
                table: "TemporaryPasswords",
                newName: "EncodedPassword");
        }
    }
}
