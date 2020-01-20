using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Innovators.NotificationSender.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MailSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastModificationDate = table.Column<DateTime>(nullable: true),
                    CreatedByCustomerId = table.Column<int>(nullable: true),
                    LastModifiedByCustomerId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Host = table.Column<string>(nullable: false),
                    Port = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastModificationDate = table.Column<DateTime>(nullable: true),
                    CreatedByCustomerId = table.Column<int>(nullable: true),
                    LastModifiedByCustomerId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Template = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
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
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmsSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastModificationDate = table.Column<DateTime>(nullable: true),
                    CreatedByCustomerId = table.Column<int>(nullable: true),
                    LastModifiedByCustomerId = table.Column<int>(nullable: true),
                    ServiceRequestUrl = table.Column<string>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastModificationDate = table.Column<DateTime>(nullable: true),
                    CreatedByCustomerId = table.Column<int>(nullable: true),
                    LastModifiedByCustomerId = table.Column<int>(nullable: true),
                    Sender = table.Column<string>(nullable: false),
                    SenderDisplay = table.Column<string>(nullable: true),
                    Receiver = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    Body = table.Column<string>(nullable: false),
                    ProviderName = table.Column<string>(nullable: true),
                    ServiceId = table.Column<int>(nullable: false),
                    NotificationTypeId = table.Column<int>(nullable: false),
                    IsBodyHtml = table.Column<bool>(nullable: false),
                    MessageId = table.Column<string>(nullable: true),
                    TemplateId = table.Column<int>(nullable: true),
                    TemplateItems = table.Column<string>(nullable: true),
                    NotificationStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationType_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MailSettings_Id",
                table: "MailSettings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MailTemplates_Id",
                table: "MailTemplates",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Id",
                table: "Notifications",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTypes_Id",
                table: "NotificationTypes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SmsSettings_Id",
                table: "SmsSettings",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailSettings");

            migrationBuilder.DropTable(
                name: "MailTemplates");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "SmsSettings");

            migrationBuilder.DropTable(
                name: "NotificationTypes");
        }
    }
}
