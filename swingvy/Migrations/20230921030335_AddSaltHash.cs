using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace swingvy.Migrations
{
    /// <inheritdoc />
    public partial class AddSaltHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "member",
                columns: table => new
                {
                    member_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__member__B29B8534A1B0EBBA", x => x.member_id);
                });

            migrationBuilder.CreateTable(
                name: "calendar",
                columns: table => new
                {
                    calendar_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    member_id = table.Column<int>(type: "int", nullable: false),
                    startTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    endTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__calendar__584C1344A5072E3C", x => x.calendar_id);
                    table.ForeignKey(
                        name: "FK__calendar__member__3F466844",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "member_id");
                });

            migrationBuilder.CreateTable(
                name: "leaveOrder",
                columns: table => new
                {
                    leaveOrder_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    member_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    startTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    endTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    applyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    reason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    state = table.Column<int>(type: "int", nullable: false),
                    head = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__leaveOrd__18AB28A687EB1153", x => x.leaveOrder_id);
                    table.ForeignKey(
                        name: "FK__leaveOrde__membe__3C69FB99",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "member_id");
                });

            migrationBuilder.CreateTable(
                name: "memberData",
                columns: table => new
                {
                    memberData_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    member_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    type = table.Column<int>(type: "int", nullable: false),
                    position = table.Column<int>(type: "int", nullable: false),
                    head = table.Column<int>(type: "int", nullable: false),
                    img_url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__memberDa__5811618C385105A0", x => x.memberData_id);
                    table.ForeignKey(
                        name: "FK__memberDat__membe__398D8EEE",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "member_id");
                });

            migrationBuilder.CreateTable(
                name: "worktime",
                columns: table => new
                {
                    worktime_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    member_id = table.Column<int>(type: "int", nullable: false),
                    startTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    endTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    state = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__worktime__3593708F23BC1E5F", x => x.worktime_id);
                    table.ForeignKey(
                        name: "FK__worktime__member__4222D4EF",
                        column: x => x.member_id,
                        principalTable: "member",
                        principalColumn: "member_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_calendar_member_id",
                table: "calendar",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_leaveOrder_member_id",
                table: "leaveOrder",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_memberData_member_id",
                table: "memberData",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_worktime_member_id",
                table: "worktime",
                column: "member_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calendar");

            migrationBuilder.DropTable(
                name: "leaveOrder");

            migrationBuilder.DropTable(
                name: "memberData");

            migrationBuilder.DropTable(
                name: "worktime");

            migrationBuilder.DropTable(
                name: "member");
        }
    }
}
