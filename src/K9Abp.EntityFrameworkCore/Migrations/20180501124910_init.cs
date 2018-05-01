using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace K9Abp.EntityFrameworkCore.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "abp_audit_log",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BrowserInfo = table.Column<string>(maxLength: 256, nullable: true),
                    ClientIpAddress = table.Column<string>(maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(maxLength: 128, nullable: true),
                    CustomData = table.Column<string>(maxLength: 2000, nullable: true),
                    Exception = table.Column<string>(maxLength: 2000, nullable: true),
                    ExecutionDuration = table.Column<int>(nullable: false),
                    ExecutionTime = table.Column<DateTime>(nullable: false),
                    ImpersonatorTenantId = table.Column<int>(nullable: true),
                    ImpersonatorUserId = table.Column<long>(nullable: true),
                    MethodName = table.Column<string>(maxLength: 256, nullable: true),
                    Parameters = table.Column<string>(maxLength: 1024, nullable: true),
                    ServiceName = table.Column<string>(maxLength: 256, nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_audit_log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_background_job",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsAbandoned = table.Column<bool>(nullable: false),
                    JobArgs = table.Column<string>(maxLength: 1048576, nullable: false),
                    JobType = table.Column<string>(maxLength: 512, nullable: false),
                    LastTryTime = table.Column<DateTime>(nullable: true),
                    NextTryTime = table.Column<DateTime>(nullable: false),
                    Priority = table.Column<byte>(nullable: false),
                    TryCount = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_background_job", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_edition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(maxLength: 64, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    AnnualPrice = table.Column<decimal>(nullable: true),
                    ExpiringEditionId = table.Column<int>(nullable: true),
                    MonthlyPrice = table.Column<decimal>(nullable: true),
                    TrialDayCount = table.Column<int>(nullable: true),
                    WaitingDayAfterExpire = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_edition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_entity_change_set",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BrowserInfo = table.Column<string>(maxLength: 256, nullable: true),
                    ClientIpAddress = table.Column<string>(maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(maxLength: 128, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ExtensionData = table.Column<string>(nullable: true),
                    ImpersonatorTenantId = table.Column<int>(nullable: true),
                    ImpersonatorUserId = table.Column<long>(nullable: true),
                    Reason = table.Column<string>(maxLength: 256, nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_entity_change_set", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 64, nullable: false),
                    Icon = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 10, nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_language_text",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Key = table.Column<string>(maxLength: 256, nullable: false),
                    LanguageName = table.Column<string>(maxLength: 10, nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Source = table.Column<string>(maxLength: 128, nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(maxLength: 67108864, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_language_text", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Data = table.Column<string>(maxLength: 1048576, nullable: true),
                    DataTypeName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityId = table.Column<string>(maxLength: 96, nullable: true),
                    EntityTypeAssemblyQualifiedName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    ExcludedUserIds = table.Column<string>(maxLength: 131072, nullable: true),
                    NotificationName = table.Column<string>(maxLength: 96, nullable: false),
                    Severity = table.Column<byte>(nullable: false),
                    TenantIds = table.Column<string>(maxLength: 131072, nullable: true),
                    UserIds = table.Column<string>(maxLength: 131072, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_notification_subscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    EntityId = table.Column<string>(maxLength: 96, nullable: true),
                    EntityTypeAssemblyQualifiedName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    NotificationName = table.Column<string>(maxLength: 96, nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_notification_subscription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_organization_unit",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 95, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 128, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_organization_unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_organization_unit_abp_organization_unit_ParentId",
                        column: x => x.ParentId,
                        principalTable: "abp_organization_unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "abp_persisted_grant",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 200, nullable: false),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 50000, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_persisted_grant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_tenant_notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Data = table.Column<string>(maxLength: 1048576, nullable: true),
                    DataTypeName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityId = table.Column<string>(maxLength: 96, nullable: true),
                    EntityTypeAssemblyQualifiedName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    NotificationName = table.Column<string>(maxLength: 96, nullable: false),
                    Severity = table.Column<byte>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_tenant_notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_user",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AuthenticationSource = table.Column<string>(maxLength: 64, nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 128, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 256, nullable: false),
                    EmailConfirmationCode = table.Column<string>(maxLength: 328, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEmailConfirmed = table.Column<bool>(nullable: false),
                    IsLockoutEnabled = table.Column<bool>(nullable: false),
                    IsPhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    IsTwoFactorEnabled = table.Column<bool>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LockoutEndDateUtc = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    NormalizedEmailAddress = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 32, nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    Password = table.Column<string>(maxLength: 128, nullable: false),
                    PasswordResetCode = table.Column<string>(maxLength: 328, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 32, nullable: true),
                    ProfilePictureId = table.Column<Guid>(nullable: true),
                    SecurityStamp = table.Column<string>(maxLength: 128, nullable: true),
                    ShouldChangePasswordOnNextLogin = table.Column<bool>(nullable: false),
                    SignInToken = table.Column<string>(nullable: true),
                    SignInTokenExpireTimeUtc = table.Column<DateTime>(nullable: true),
                    Surname = table.Column<string>(maxLength: 32, nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    UserName = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_user_abp_user_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_user_abp_user_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_user_abp_user_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_account",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 256, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    UserLinkId = table.Column<long>(nullable: true),
                    UserName = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_login_attempt",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BrowserInfo = table.Column<string>(maxLength: 256, nullable: true),
                    ClientIpAddress = table.Column<string>(maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(maxLength: 128, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Result = table.Column<byte>(nullable: false),
                    TenancyName = table.Column<string>(maxLength: 64, nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    UserNameOrEmailAddress = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_login_attempt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    TenantNotificationId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_organization_unit",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_organization_unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "app_binary_object",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Bytes = table.Column<byte[]>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_binary_object", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "app_chat_message",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(maxLength: 4096, nullable: false),
                    ReadState = table.Column<int>(nullable: false),
                    ReceiverReadState = table.Column<int>(nullable: false),
                    SharedMessageId = table.Column<Guid>(nullable: true),
                    Side = table.Column<int>(nullable: false),
                    TargetTenantId = table.Column<int>(nullable: true),
                    TargetUserId = table.Column<long>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_chat_message", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "app_friendship",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    FriendProfilePictureId = table.Column<Guid>(nullable: true),
                    FriendTenancyName = table.Column<string>(nullable: true),
                    FriendTenantId = table.Column<int>(nullable: true),
                    FriendUserId = table.Column<long>(nullable: false),
                    FriendUserName = table.Column<string>(maxLength: 32, nullable: false),
                    State = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_friendship", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "app_invoice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    InvoiceNo = table.Column<string>(nullable: true),
                    TenantAddress = table.Column<string>(nullable: true),
                    TenantLegalName = table.Column<string>(nullable: true),
                    TenantTaxNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "deskwork_customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "char(11)", maxLength: 11, nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deskwork_customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "abp_feature",
                columns: table => new
                {
                    EditionId = table.Column<int>(nullable: true),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_feature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_feature_abp_edition_EditionId",
                        column: x => x.EditionId,
                        principalTable: "abp_edition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_subscription_payment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DayCount = table.Column<int>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    EditionId = table.Column<int>(nullable: false),
                    Gateway = table.Column<int>(nullable: false),
                    InvoiceNo = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PaymentId = table.Column<string>(nullable: true),
                    PaymentPeriodType = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_subscription_payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_app_subscription_payment_abp_edition_EditionId",
                        column: x => x.EditionId,
                        principalTable: "abp_edition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_entity_change",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ChangeTime = table.Column<DateTime>(nullable: false),
                    ChangeType = table.Column<byte>(nullable: false),
                    EntityChangeSetId = table.Column<long>(nullable: false),
                    EntityId = table.Column<string>(maxLength: 48, nullable: true),
                    EntityTypeFullName = table.Column<string>(maxLength: 192, nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_entity_change", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_entity_change_abp_entity_change_set_EntityChangeSetId",
                        column: x => x.EntityChangeSetId,
                        principalTable: "abp_entity_change_set",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConcurrencyStamp = table.Column<string>(maxLength: 128, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 64, nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsStatic = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    NormalizedName = table.Column<string>(maxLength: 32, nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_role_abp_user_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_role_abp_user_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_role_abp_user_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "abp_setting",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    Value = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_setting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_setting_abp_user_UserId",
                        column: x => x.UserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "abp_tenant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConnectionString = table.Column<string>(maxLength: 1024, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CustomCssId = table.Column<Guid>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    EditionId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsInTrialPeriod = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LogoFileType = table.Column<string>(maxLength: 64, nullable: true),
                    LogoId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    SubscriptionEndDateUtc = table.Column<DateTime>(nullable: true),
                    TenancyName = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_tenant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_tenant_abp_user_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_tenant_abp_user_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_tenant_abp_edition_EditionId",
                        column: x => x.EditionId,
                        principalTable: "abp_edition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_abp_tenant_abp_user_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_claim",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(maxLength: 256, nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_claim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_user_claim_abp_user_UserId",
                        column: x => x.UserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_login",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 256, nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_login", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_user_login_abp_user_UserId",
                        column: x => x.UserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_role",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_user_role_abp_user_UserId",
                        column: x => x.UserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_user_token",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LoginProvider = table.Column<string>(maxLength: 64, nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    Value = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_user_token", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_user_token_abp_user_UserId",
                        column: x => x.UserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_entity_property_change",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EntityChangeId = table.Column<long>(nullable: false),
                    NewValue = table.Column<string>(maxLength: 512, nullable: true),
                    OriginalValue = table.Column<string>(maxLength: 512, nullable: true),
                    PropertyName = table.Column<string>(maxLength: 96, nullable: true),
                    PropertyTypeFullName = table.Column<string>(maxLength: 192, nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_entity_property_change", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_entity_property_change_abp_entity_change_EntityChangeId",
                        column: x => x.EntityChangeId,
                        principalTable: "abp_entity_change",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_permission",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    IsGranted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_permission_abp_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "abp_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_abp_permission_abp_user_UserId",
                        column: x => x.UserId,
                        principalTable: "abp_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "abp_role_claim",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(maxLength: 256, nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_role_claim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_abp_role_claim_abp_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "abp_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_abp_audit_log_TenantId_ExecutionDuration",
                table: "abp_audit_log",
                columns: new[] { "TenantId", "ExecutionDuration" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_audit_log_TenantId_ExecutionTime",
                table: "abp_audit_log",
                columns: new[] { "TenantId", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_audit_log_TenantId_UserId",
                table: "abp_audit_log",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_background_job_IsAbandoned_NextTryTime",
                table: "abp_background_job",
                columns: new[] { "IsAbandoned", "NextTryTime" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_entity_change_EntityChangeSetId",
                table: "abp_entity_change",
                column: "EntityChangeSetId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_entity_change_EntityTypeFullName_EntityId",
                table: "abp_entity_change",
                columns: new[] { "EntityTypeFullName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_entity_change_set_TenantId_CreationTime",
                table: "abp_entity_change_set",
                columns: new[] { "TenantId", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_entity_change_set_TenantId_Reason",
                table: "abp_entity_change_set",
                columns: new[] { "TenantId", "Reason" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_entity_change_set_TenantId_UserId",
                table: "abp_entity_change_set",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_entity_property_change_EntityChangeId",
                table: "abp_entity_property_change",
                column: "EntityChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_feature_EditionId_Name",
                table: "abp_feature",
                columns: new[] { "EditionId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_feature_TenantId_Name",
                table: "abp_feature",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_language_TenantId_Name",
                table: "abp_language",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_language_text_TenantId_Source_LanguageName_Key",
                table: "abp_language_text",
                columns: new[] { "TenantId", "Source", "LanguageName", "Key" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_notification_subscription_NotificationName_EntityTypeName_EntityId_UserId",
                table: "abp_notification_subscription",
                columns: new[] { "NotificationName", "EntityTypeName", "EntityId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_notification_subscription_TenantId_NotificationName_EntityTypeName_EntityId_UserId",
                table: "abp_notification_subscription",
                columns: new[] { "TenantId", "NotificationName", "EntityTypeName", "EntityId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_organization_unit_ParentId",
                table: "abp_organization_unit",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_organization_unit_TenantId_Code",
                table: "abp_organization_unit",
                columns: new[] { "TenantId", "Code" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_permission_TenantId_Name",
                table: "abp_permission",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_permission_RoleId",
                table: "abp_permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_permission_UserId",
                table: "abp_permission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_persisted_grant_SubjectId_ClientId_Type",
                table: "abp_persisted_grant",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_role_CreatorUserId",
                table: "abp_role",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_role_DeleterUserId",
                table: "abp_role",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_role_LastModifierUserId",
                table: "abp_role",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_role_TenantId_NormalizedName",
                table: "abp_role",
                columns: new[] { "TenantId", "NormalizedName" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_role_claim_RoleId",
                table: "abp_role_claim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_role_claim_TenantId_ClaimType",
                table: "abp_role_claim",
                columns: new[] { "TenantId", "ClaimType" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_setting_UserId",
                table: "abp_setting",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_setting_TenantId_Name",
                table: "abp_setting",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenant_CreationTime",
                table: "abp_tenant",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenant_CreatorUserId",
                table: "abp_tenant",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenant_DeleterUserId",
                table: "abp_tenant",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenant_EditionId",
                table: "abp_tenant",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenant_LastModifierUserId",
                table: "abp_tenant",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenant_SubscriptionEndDateUtc",
                table: "abp_tenant",
                column: "SubscriptionEndDateUtc");

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenant_TenancyName",
                table: "abp_tenant",
                column: "TenancyName");

            migrationBuilder.CreateIndex(
                name: "IX_abp_tenant_notification_TenantId",
                table: "abp_tenant_notification",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_CreatorUserId",
                table: "abp_user",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_DeleterUserId",
                table: "abp_user",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_LastModifierUserId",
                table: "abp_user",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_TenantId_NormalizedEmailAddress",
                table: "abp_user",
                columns: new[] { "TenantId", "NormalizedEmailAddress" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_TenantId_NormalizedUserName",
                table: "abp_user",
                columns: new[] { "TenantId", "NormalizedUserName" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_account_EmailAddress",
                table: "abp_user_account",
                column: "EmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_account_UserName",
                table: "abp_user_account",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_account_TenantId_EmailAddress",
                table: "abp_user_account",
                columns: new[] { "TenantId", "EmailAddress" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_account_TenantId_UserId",
                table: "abp_user_account",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_account_TenantId_UserName",
                table: "abp_user_account",
                columns: new[] { "TenantId", "UserName" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_claim_UserId",
                table: "abp_user_claim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_claim_TenantId_ClaimType",
                table: "abp_user_claim",
                columns: new[] { "TenantId", "ClaimType" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_login_UserId",
                table: "abp_user_login",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_login_TenantId_UserId",
                table: "abp_user_login",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_login_TenantId_LoginProvider_ProviderKey",
                table: "abp_user_login",
                columns: new[] { "TenantId", "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_login_attempt_UserId_TenantId",
                table: "abp_user_login_attempt",
                columns: new[] { "UserId", "TenantId" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_login_attempt_TenancyName_UserNameOrEmailAddress_Result",
                table: "abp_user_login_attempt",
                columns: new[] { "TenancyName", "UserNameOrEmailAddress", "Result" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_notification_UserId_State_CreationTime",
                table: "abp_user_notification",
                columns: new[] { "UserId", "State", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_organization_unit_TenantId_OrganizationUnitId",
                table: "abp_user_organization_unit",
                columns: new[] { "TenantId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_organization_unit_TenantId_UserId",
                table: "abp_user_organization_unit",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_role_UserId",
                table: "abp_user_role",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_role_TenantId_RoleId",
                table: "abp_user_role",
                columns: new[] { "TenantId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_role_TenantId_UserId",
                table: "abp_user_role",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_token_UserId",
                table: "abp_user_token",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_abp_user_token_TenantId_UserId",
                table: "abp_user_token",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_app_binary_object_TenantId",
                table: "app_binary_object",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_app_chat_message_TargetTenantId_TargetUserId_ReadState",
                table: "app_chat_message",
                columns: new[] { "TargetTenantId", "TargetUserId", "ReadState" });

            migrationBuilder.CreateIndex(
                name: "IX_app_chat_message_TargetTenantId_UserId_ReadState",
                table: "app_chat_message",
                columns: new[] { "TargetTenantId", "UserId", "ReadState" });

            migrationBuilder.CreateIndex(
                name: "IX_app_chat_message_TenantId_TargetUserId_ReadState",
                table: "app_chat_message",
                columns: new[] { "TenantId", "TargetUserId", "ReadState" });

            migrationBuilder.CreateIndex(
                name: "IX_app_chat_message_TenantId_UserId_ReadState",
                table: "app_chat_message",
                columns: new[] { "TenantId", "UserId", "ReadState" });

            migrationBuilder.CreateIndex(
                name: "IX_app_friendship_FriendTenantId_FriendUserId",
                table: "app_friendship",
                columns: new[] { "FriendTenantId", "FriendUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_app_friendship_FriendTenantId_UserId",
                table: "app_friendship",
                columns: new[] { "FriendTenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_app_friendship_TenantId_FriendUserId",
                table: "app_friendship",
                columns: new[] { "TenantId", "FriendUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_app_friendship_TenantId_UserId",
                table: "app_friendship",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_app_subscription_payment_EditionId",
                table: "app_subscription_payment",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_app_subscription_payment_PaymentId_Gateway",
                table: "app_subscription_payment",
                columns: new[] { "PaymentId", "Gateway" });

            migrationBuilder.CreateIndex(
                name: "IX_app_subscription_payment_Status_CreationTime",
                table: "app_subscription_payment",
                columns: new[] { "Status", "CreationTime" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "abp_audit_log");

            migrationBuilder.DropTable(
                name: "abp_background_job");

            migrationBuilder.DropTable(
                name: "abp_entity_property_change");

            migrationBuilder.DropTable(
                name: "abp_feature");

            migrationBuilder.DropTable(
                name: "abp_language");

            migrationBuilder.DropTable(
                name: "abp_language_text");

            migrationBuilder.DropTable(
                name: "abp_notification");

            migrationBuilder.DropTable(
                name: "abp_notification_subscription");

            migrationBuilder.DropTable(
                name: "abp_organization_unit");

            migrationBuilder.DropTable(
                name: "abp_permission");

            migrationBuilder.DropTable(
                name: "abp_persisted_grant");

            migrationBuilder.DropTable(
                name: "abp_role_claim");

            migrationBuilder.DropTable(
                name: "abp_setting");

            migrationBuilder.DropTable(
                name: "abp_tenant");

            migrationBuilder.DropTable(
                name: "abp_tenant_notification");

            migrationBuilder.DropTable(
                name: "abp_user_account");

            migrationBuilder.DropTable(
                name: "abp_user_claim");

            migrationBuilder.DropTable(
                name: "abp_user_login");

            migrationBuilder.DropTable(
                name: "abp_user_login_attempt");

            migrationBuilder.DropTable(
                name: "abp_user_notification");

            migrationBuilder.DropTable(
                name: "abp_user_organization_unit");

            migrationBuilder.DropTable(
                name: "abp_user_role");

            migrationBuilder.DropTable(
                name: "abp_user_token");

            migrationBuilder.DropTable(
                name: "app_binary_object");

            migrationBuilder.DropTable(
                name: "app_chat_message");

            migrationBuilder.DropTable(
                name: "app_friendship");

            migrationBuilder.DropTable(
                name: "app_invoice");

            migrationBuilder.DropTable(
                name: "app_subscription_payment");

            migrationBuilder.DropTable(
                name: "deskwork_customer");

            migrationBuilder.DropTable(
                name: "abp_entity_change");

            migrationBuilder.DropTable(
                name: "abp_role");

            migrationBuilder.DropTable(
                name: "abp_edition");

            migrationBuilder.DropTable(
                name: "abp_entity_change_set");

            migrationBuilder.DropTable(
                name: "abp_user");
        }
    }
}
