using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalFileStorageSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SubdomainPrefix = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TenantStatus = table.Column<string>(type: "text", nullable: false),
                    BillingPlan = table.Column<string>(type: "text", nullable: false),
                    StorageQuota = table.Column<long>(type: "bigint", nullable: false),
                    BandwithQuota = table.Column<long>(type: "bigint", nullable: false),
                    APIRateLimit = table.Column<int>(type: "integer", nullable: false),
                    DataResidencyRegion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ComplianceRequirements = table.Column<int>(type: "integer", nullable: false),
                    EncryptionRequirements = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsageRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    StorageUsed = table.Column<long>(type: "bigint", nullable: false),
                    BandwidthUsed = table.Column<long>(type: "bigint", nullable: false),
                    APICallsCount = table.Column<int>(type: "integer", nullable: false),
                    FileOperationCount = table.Column<long>(type: "bigint", nullable: false),
                    ActiveUserCount = table.Column<int>(type: "integer", nullable: false),
                    StorageTransactions = table.Column<long>(type: "bigint", nullable: false),
                    SnapshotDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsageRecords_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Permissions = table.Column<int>(type: "integer", nullable: false),
                    LastLoginTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MFAEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    APIKeyHash = table.Column<string>(type: "text", nullable: true),
                    SessionTimeout = table.Column<long>(type: "bigint", nullable: false),
                    IPWhitelist = table.Column<List<string>>(type: "text[]", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    StoragePath = table.Column<string>(type: "text", nullable: false),
                    MD5Hash = table.Column<string>(type: "text", nullable: false),
                    SHA256Hash = table.Column<string>(type: "text", nullable: false),
                    EncryptionKeyId = table.Column<string>(type: "text", nullable: true),
                    UploadTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastAccessedTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    VersionNumber = table.Column<int>(type: "integer", nullable: false),
                    Metadata = table.Column<Dictionary<string, string>>(type: "jsonb", nullable: false),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: false),
                    AccessLevel = table.Column<string>(type: "text", nullable: false),
                    UploadedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Files_Users_UploadedBy",
                        column: x => x.UploadedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_TenantId_FileName",
                table: "Files",
                columns: new[] { "TenantId", "FileName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_UploadedBy",
                table: "Files",
                column: "UploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_SubdomainPrefix",
                table: "Tenants",
                column: "SubdomainPrefix",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsageRecords_TenantId_SnapshotDate",
                table: "UsageRecords",
                columns: new[] { "TenantId", "SnapshotDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                table: "Users",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "UsageRecords");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
