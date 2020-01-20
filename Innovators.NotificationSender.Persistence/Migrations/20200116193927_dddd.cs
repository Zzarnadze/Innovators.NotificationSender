using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Innovators.NotificationSender.Persistence.Migrations
{
    public partial class dddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmsConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastModificationDate = table.Column<DateTime>(nullable: true),
                    CreatedByCustomerId = table.Column<int>(nullable: true),
                    LastModifiedByCustomerId = table.Column<int>(nullable: true),
                    NotificationTypeId = table.Column<int>(nullable: true),
                    ServiceRequestUrl = table.Column<string>(maxLength: 500, nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmsConfigurations_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmsConfigurations_NotificationTypeId",
                table: "SmsConfigurations",
                column: "NotificationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmsConfigurations");
        }
    }
}
