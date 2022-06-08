using Microsoft.EntityFrameworkCore.Migrations;

namespace Shmelev_Backend_Task3.Migrations
{
    public partial class Next : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "229c7b7c-d68e-4f2a-bb73-1bb28c5cb36c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2977ed39-183e-40cb-8bb6-c193427dbc80");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6ada523d-0a36-4002-bc56-3296fe6a6f52", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ada523d-0a36-4002-bc56-3296fe6a6f52");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "dcd6603d-1e41-4c6b-87aa-541ccf5eb895", "d63177a6-c6e6-463b-84c2-41749cd8fdca", "Admin", "ADMIN" },
                    { "bd7acb6c-c29a-464e-984c-b1c369f13729", "d1f15ea9-d20e-41cd-9fc0-dd7f2c898570", "User", "USER" },
                    { "2dbe9c16-990d-4117-8162-8de2bc7bcfcf", "0339278f-7176-46be-b9a7-d65a5577bb39", "Moderator", "MODERATOR" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64b6755d-ee10-4abf-99f6-7eb549e18b6f", "AQAAAAEAACcQAAAAELXaW9zbh3Q4GyRXoJs/3aQ1a11RILnF82PjzJEc6DWqys3Q6JtUQLWAwRD7LkQ8Bg==", "945461d7-9f5e-4be1-872b-888cc63a75eb" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2", 0, "ceb5881d-6990-4010-9036-b1d1c49e85f7", "mod@mod.ru", true, false, null, "MOD@MOD.RU", "MOD@MOD.RU", "AQAAAAEAACcQAAAAED76ySbetz1ccHfWKZh9XcnXTZAaph1zrPKFsSQZS5oUCqMDmDb2jDr9AePFchdhSw==", null, false, "97fbf36f-6fbc-4c20-a342-73b38b2cf0ab", false, "mod@mod.ru" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dcd6603d-1e41-4c6b-87aa-541ccf5eb895", "1" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2dbe9c16-990d-4117-8162-8de2bc7bcfcf", "2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd7acb6c-c29a-464e-984c-b1c369f13729");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dcd6603d-1e41-4c6b-87aa-541ccf5eb895", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2dbe9c16-990d-4117-8162-8de2bc7bcfcf", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2dbe9c16-990d-4117-8162-8de2bc7bcfcf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dcd6603d-1e41-4c6b-87aa-541ccf5eb895");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6ada523d-0a36-4002-bc56-3296fe6a6f52", "bbdc13ac-4cda-43f9-9f79-05d639237e81", "Admin", "ADMIN" },
                    { "2977ed39-183e-40cb-8bb6-c193427dbc80", "d0f389bb-adbd-4f35-8f32-7ea2a1d15b4e", "User", "USER" },
                    { "229c7b7c-d68e-4f2a-bb73-1bb28c5cb36c", "42782db2-4b2d-406a-a9ac-022fbac39d83", "Moderator", "MODERATOR" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "797f1fcd-ed62-4cb9-af59-63647a745d15", "AQAAAAEAACcQAAAAEOTapph9zvCyo9V9HmFLO6PYzSGOINh9Yo2f37IOATFqKWUZvGCDHQ/eTNkWt1At5A==", "9e386ee1-c069-4db4-8efa-1f227cb18469" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6ada523d-0a36-4002-bc56-3296fe6a6f52", "1" });
        }
    }
}
