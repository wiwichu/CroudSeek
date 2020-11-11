using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftDrive.IDP.Migrations.IdentityDb
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Subject = table.Column<string>(maxLength: 200, nullable: false),
                    Username = table.Column<string>(maxLength: 200, nullable: true),
                    Password = table.Column<string>(maxLength: 200, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 250, nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Password", "Subject", "Username" },
                values: new object[] { new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), true, "ef6e4c07-abe4-43c2-8765-f2598569ddea", "password", "d860efca-22d9-47fd-8249-791ba61b07c7", "Frank" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Password", "Subject", "Username" },
                values: new object[] { new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), true, "c71576bc-edaa-4573-a8fb-85755aa08a01", "password", "b7539694-97e7-4dfe-84da-b4256e1ff5c7", "Claire" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("6fefddcd-eef3-4874-bcc2-cf4e7cc575b0"), "96f0579c-9335-4b8c-8759-1549ff0174b9", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Frank" },
                    { new Guid("1e20b4ee-5e01-4890-97ef-eb39310b2255"), "1009119a-24b7-482e-9de9-2fbbf54b929f", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Underwood" },
                    { new Guid("a00f8a8f-2b91-4ff8-af64-95dd2d60ef90"), "81b925c9-fd81-4724-a9d1-fecfb9a9440e", "address", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Main Road 1" },
                    { new Guid("11a1475e-b55c-4b76-b9df-53b5eb3f872a"), "ca45af6c-8434-45b3-a4d9-2d930e1278d3", "subscriptionlevel", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" },
                    { new Guid("cefdeff2-cfc8-428f-88ac-31473f135718"), "5ad1f681-ed15-42e2-b4c3-d8b64ddcb8c3", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" },
                    { new Guid("73d863fc-161c-4684-9171-35c66ae1c571"), "2027749c-e562-4e58-a3ac-1aee8da3a1e0", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Claire" },
                    { new Guid("8aa188d2-0ac9-46e4-827c-27319a0ef3eb"), "23801e93-93a7-41cf-b84b-54c223fe789d", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Underwood" },
                    { new Guid("3b5296e3-06dc-438a-9ab7-9cbd5ce20937"), "d54c2ae4-9bab-4e6f-a53c-e1109be76ae9", "address", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Big Street 2" },
                    { new Guid("5b8c1a65-b9d5-4f33-90de-fd8ceefb6b28"), "d061a135-ecae-4e08-be73-7f09e6a14ab0", "subscriptionlevel", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" },
                    { new Guid("e3815fec-379e-4199-9114-645a3f9b414d"), "4b2139ab-5445-4284-8480-8567b1bbda37", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Subject",
                table: "Users",
                column: "Subject",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
