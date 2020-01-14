using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Innovators.NotificationSender.Persistence.Migrations
{
    public partial class FirstMigratin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    NotificationTypeId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTypes_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MailConfigurations",
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
                    NotificationTypeId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(maxLength: 254, nullable: false),
                    UserName = table.Column<string>(maxLength: 300, nullable: false),
                    Password = table.Column<string>(maxLength: 300, nullable: false),
                    Host = table.Column<string>(maxLength: 300, nullable: false),
                    Port = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailConfigurations_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MailTemplates",
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
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    zzz = table.Column<string>(nullable: true),
                    Template = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailTemplates_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
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
                    Name = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Sender = table.Column<string>(maxLength: 254, nullable: false),
                    SenderDisplay = table.Column<string>(maxLength: 50, nullable: true),
                    Receiver = table.Column<string>(maxLength: 254, nullable: false),
                    Subject = table.Column<string>(maxLength: 300, nullable: false),
                    Body = table.Column<string>(maxLength: 1000, nullable: false),
                    ProviderName = table.Column<string>(maxLength: 300, nullable: true),
                    ServiceId = table.Column<int>(nullable: false),
                    NotificationTypeId = table.Column<int>(nullable: false),
                    IsBodyHtml = table.Column<bool>(nullable: false),
                    MessageId = table.Column<string>(maxLength: 10, nullable: true),
                    TemplateId = table.Column<int>(nullable: true),
                    TemplateItems = table.Column<string>(nullable: true),
                    SubTemplateId = table.Column<int>(nullable: true),
                    SubTemplateItems = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    NotificationStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_NotificationType_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MailConfigurations_Id",
                table: "MailConfigurations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MailConfigurations_NotificationTypeId",
                table: "MailConfigurations",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MailTemplates_NotificationTypeId",
                table: "MailTemplates",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Id",
                table: "Notifications",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTypes_Id",
                table: "NotificationTypes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTypes_NotificationTypeId",
                table: "NotificationTypes",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NotificationTypeId",
                table: "Users",
                column: "NotificationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailConfigurations");

            migrationBuilder.DropTable(
                name: "MailTemplates");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "NotificationTypes");
        }
    }
}
