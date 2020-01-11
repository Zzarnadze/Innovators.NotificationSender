using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Innovators.NotificationSender.Persistence.Migrations
{
    public partial class ada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Name = table.Column<string>(maxLength: 300, nullable: false),
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
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
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
                    Name = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
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
                    Sender = table.Column<string>(maxLength: 254, nullable: false),
                    SenderDisplay = table.Column<string>(maxLength: 50, nullable: true),
                    Receiver = table.Column<string>(maxLength: 254, nullable: false),
                    Subject = table.Column<string>(maxLength: 300, nullable: true),
                    Body = table.Column<string>(maxLength: 1000, nullable: true),
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
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailTemplates");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
