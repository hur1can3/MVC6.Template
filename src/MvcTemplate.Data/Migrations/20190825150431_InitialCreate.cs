using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcTemplate.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    CreatedByAccountId = table.Column<int>(nullable: true),
                    ModifiedByAccountId = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<int>(nullable: true),
                    Action = table.Column<string>(maxLength: 16, nullable: false),
                    EntityName = table.Column<string>(maxLength: 64, nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    Changes = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    CreatedByAccountId = table.Column<int>(nullable: true),
                    ModifiedByAccountId = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Area = table.Column<string>(maxLength: 64, nullable: true),
                    Controller = table.Column<string>(maxLength: 64, nullable: false),
                    Action = table.Column<string>(maxLength: 64, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    CreatedByAccountId = table.Column<int>(nullable: true),
                    ModifiedByAccountId = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    CreatedByAccountId = table.Column<int>(nullable: true),
                    ModifiedByAccountId = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(maxLength: 32, nullable: false),
                    Passhash = table.Column<string>(maxLength: 64, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    RecoveryToken = table.Column<string>(maxLength: 36, nullable: true),
                    RecoveryTokenExpirationDate = table.Column<DateTime>(nullable: true),
                    RoleId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Account_CreatedByAccountId",
                        column: x => x.CreatedByAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Account_Account_ModifiedByAccountId",
                        column: x => x.ModifiedByAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Account_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    CreatedByAccountId = table.Column<int>(nullable: true),
                    ModifiedByAccountId = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermission_Account_CreatedByAccountId",
                        column: x => x.CreatedByAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermission_Account_ModifiedByAccountId",
                        column: x => x.ModifiedByAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_CreatedByAccountId",
                table: "Account",
                column: "CreatedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Email",
                table: "Account",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_ModifiedByAccountId",
                table: "Account",
                column: "ModifiedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_RoleId",
                table: "Account",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Username",
                table: "Account",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_CreatedByAccountId",
                table: "AuditLog",
                column: "CreatedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_ModifiedByAccountId",
                table: "AuditLog",
                column: "ModifiedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_CreatedByAccountId",
                table: "Permission",
                column: "CreatedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_ModifiedByAccountId",
                table: "Permission",
                column: "ModifiedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_CreatedByAccountId",
                table: "Role",
                column: "CreatedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_ModifiedByAccountId",
                table: "Role",
                column: "ModifiedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Title",
                table: "Role",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_CreatedByAccountId",
                table: "RolePermission",
                column: "CreatedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_ModifiedByAccountId",
                table: "RolePermission",
                column: "ModifiedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLog_Account_CreatedByAccountId",
                table: "AuditLog",
                column: "CreatedByAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLog_Account_ModifiedByAccountId",
                table: "AuditLog",
                column: "ModifiedByAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_Account_CreatedByAccountId",
                table: "Permission",
                column: "CreatedByAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_Account_ModifiedByAccountId",
                table: "Permission",
                column: "ModifiedByAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Account_CreatedByAccountId",
                table: "Role",
                column: "CreatedByAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Account_ModifiedByAccountId",
                table: "Role",
                column: "ModifiedByAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Role_RoleId",
                table: "Account");

            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
