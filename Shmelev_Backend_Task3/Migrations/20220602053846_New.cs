using Microsoft.EntityFrameworkCore.Migrations;

namespace Shmelev_Backend_Task3.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4f7533a-4ddb-49e0-ad2d-436a881f35bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fda64a77-c2ea-4da1-a9c1-508bcd5c412d");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "157f3d1f-9ac3-4e72-bddc-7d801950c5f4", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "157f3d1f-9ac3-4e72-bddc-7d801950c5f4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "157f3d1f-9ac3-4e72-bddc-7d801950c5f4", "057df98a-02ea-47fc-b85d-6b5ab25725b8", "Admin", "ADMIN" },
                    { "f4f7533a-4ddb-49e0-ad2d-436a881f35bc", "900d3b82-52bb-42e1-a211-4ddda15ccf56", "User", "USER" },
                    { "fda64a77-c2ea-4da1-a9c1-508bcd5c412d", "92b80f7b-1c67-4ec6-bd5b-b8311f36d220", "Moderator", "MODERATOR" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a025420-fc25-46a8-80f0-d1275ad47171", "AQAAAAEAACcQAAAAEJdBuOPDD1x+guNdqnNMKMIHfHzAmSDIM5ds0jKqVqEeHonw0ZVwlrg0Ti+yX1UWrQ==", "bf7d5765-f11d-43b9-af18-f995f6dee12b" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "157f3d1f-9ac3-4e72-bddc-7d801950c5f4", "1" });
        }
    }
}
