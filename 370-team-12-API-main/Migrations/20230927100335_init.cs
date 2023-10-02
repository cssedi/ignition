using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMWIgnition_API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeInstanceStatus",
                columns: table => new
                {
                    ChallengeInstanceStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeInstanceStatus", x => x.ChallengeInstanceStatusId);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeStatuses",
                columns: table => new
                {
                    ChallengeStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeStatuses", x => x.ChallengeStatusID);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeTypes",
                columns: table => new
                {
                    ChallengeTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeTypes", x => x.ChallengeTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Emojis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emojis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQs",
                columns: table => new
                {
                    FAQId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQs", x => x.FAQId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "PrizeCategories",
                columns: table => new
                {
                    PrizeCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrizeCategories", x => x.PrizeCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "PrizeOrderStatus",
                columns: table => new
                {
                    PrizeOrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrizeOrderStatus", x => x.PrizeOrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "QuestionCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "RewardRedemptionStatus",
                columns: table => new
                {
                    RewardRedemptionStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardRedemptionStatus", x => x.RewardRedemptionStatusId);
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrderStatuses",
                columns: table => new
                {
                    SupplierOrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOrderStatuses", x => x.SupplierOrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medals",
                columns: table => new
                {
                    MedalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChallengeTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medals", x => x.MedalId);
                    table.ForeignKey(
                        name: "FK_Medals_ChallengeTypes_ChallengeTypeId",
                        column: x => x.ChallengeTypeId,
                        principalTable: "ChallengeTypes",
                        principalColumn: "ChallengeTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Helps",
                columns: table => new
                {
                    HelpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Helps", x => x.HelpId);
                    table.ForeignKey(
                        name: "FK_Helps_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrizeTypes",
                columns: table => new
                {
                    PrizeTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrizeCategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrizeTypes", x => x.PrizeTypeID);
                    table.ForeignKey(
                        name: "FK_PrizeTypes_PrizeCategories_PrizeCategoryID",
                        column: x => x.PrizeCategoryID,
                        principalTable: "PrizeCategories",
                        principalColumn: "PrizeCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "QuestionCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.QuizId);
                    table.ForeignKey(
                        name: "FK_Quizzes_QuestionCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "QuestionCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RewardRedemptions",
                columns: table => new
                {
                    RewardRedemptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QRCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RewardRedemptionStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardRedemptions", x => x.RewardRedemptionId);
                    table.ForeignKey(
                        name: "FK_RewardRedemptions_RewardRedemptionStatus_RewardRedemptionStatusId",
                        column: x => x.RewardRedemptionStatusId,
                        principalTable: "RewardRedemptionStatus",
                        principalColumn: "RewardRedemptionStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrders",
                columns: table => new
                {
                    SupplierOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatePlaced = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDelivered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupplierOrderStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOrders", x => x.SupplierOrderId);
                    table.ForeignKey(
                        name: "FK_SupplierOrders_SupplierOrderStatuses_SupplierOrderStatusId",
                        column: x => x.SupplierOrderStatusId,
                        principalTable: "SupplierOrderStatuses",
                        principalColumn: "SupplierOrderStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prizes",
                columns: table => new
                {
                    PrizeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontImgURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackImgURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    PrizeTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prizes", x => x.PrizeID);
                    table.ForeignKey(
                        name: "FK_Prizes_PrizeTypes_PrizeTypeID",
                        column: x => x.PrizeTypeID,
                        principalTable: "PrizeTypes",
                        principalColumn: "PrizeTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuizQuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => new { x.QuizId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    Tokens = table.Column<int>(type: "int", nullable: false),
                    AwardsArchitectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_AwardsArchitectId",
                        column: x => x.AwardsArchitectId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChallengerMedals",
                columns: table => new
                {
                    ChallenegerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedalId = table.Column<int>(type: "int", nullable: false),
                    ChallengerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengerMedals", x => new { x.ChallenegerId, x.MedalId });
                    table.ForeignKey(
                        name: "FK_ChallengerMedals_AspNetUsers_ChallengerId",
                        column: x => x.ChallengerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChallengerMedals_Medals_MedalId",
                        column: x => x.MedalId,
                        principalTable: "Medals",
                        principalColumn: "MedalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    ChallengeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tokens = table.Column<int>(type: "int", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChallengeStatusID = table.Column<int>(type: "int", nullable: true),
                    ChallengeTypeID = table.Column<int>(type: "int", nullable: false),
                    MedalId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrizeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.ChallengeID);
                    table.ForeignKey(
                        name: "FK_Challenges_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Challenges_ChallengeStatuses_ChallengeStatusID",
                        column: x => x.ChallengeStatusID,
                        principalTable: "ChallengeStatuses",
                        principalColumn: "ChallengeStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Challenges_ChallengeTypes_ChallengeTypeID",
                        column: x => x.ChallengeTypeID,
                        principalTable: "ChallengeTypes",
                        principalColumn: "ChallengeTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Challenges_Medals_MedalId",
                        column: x => x.MedalId,
                        principalTable: "Medals",
                        principalColumn: "MedalId");
                    table.ForeignKey(
                        name: "FK_Challenges_Prizes_PrizeId",
                        column: x => x.PrizeId,
                        principalTable: "Prizes",
                        principalColumn: "PrizeID");
                });

            migrationBuilder.CreateTable(
                name: "Functions",
                columns: table => new
                {
                    FunctionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FunctionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SuperArchitectId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functions", x => x.FunctionId);
                    table.ForeignKey(
                        name: "FK_Functions_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Functions_AspNetUsers_SuperArchitectId1",
                        column: x => x.SuperArchitectId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChallengerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_ChallengerId",
                        column: x => x.ChallengerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrizeOrders",
                columns: table => new
                {
                    PrizeId = table.Column<int>(type: "int", nullable: false),
                    ChallengerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrizeOrderId = table.Column<int>(type: "int", nullable: false),
                    DatePlaced = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrizeOrderStatusId = table.Column<int>(type: "int", nullable: false),
                    SupplierOrderId = table.Column<int>(type: "int", nullable: true),
                    RewardRedemptionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrizeOrders", x => new { x.ChallengerId, x.PrizeId });
                    table.ForeignKey(
                        name: "FK_PrizeOrders_AspNetUsers_ChallengerId",
                        column: x => x.ChallengerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrizeOrders_PrizeOrderStatus_PrizeOrderStatusId",
                        column: x => x.PrizeOrderStatusId,
                        principalTable: "PrizeOrderStatus",
                        principalColumn: "PrizeOrderStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrizeOrders_Prizes_PrizeId",
                        column: x => x.PrizeId,
                        principalTable: "Prizes",
                        principalColumn: "PrizeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrizeOrders_RewardRedemptions_RewardRedemptionId",
                        column: x => x.RewardRedemptionId,
                        principalTable: "RewardRedemptions",
                        principalColumn: "RewardRedemptionId");
                    table.ForeignKey(
                        name: "FK_PrizeOrders_SupplierOrders_SupplierOrderId",
                        column: x => x.SupplierOrderId,
                        principalTable: "SupplierOrders",
                        principalColumn: "SupplierOrderId");
                });

            migrationBuilder.CreateTable(
                name: "ChallengeInstances",
                columns: table => new
                {
                    ChallengerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChallengeID = table.Column<int>(type: "int", nullable: false),
                    ChallengeInstanceId = table.Column<int>(type: "int", nullable: false),
                    Submition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChallengeInstanceStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeInstances", x => new { x.ChallengeID, x.ChallengerId });
                    table.ForeignKey(
                        name: "FK_ChallengeInstances_AspNetUsers_ChallengerId",
                        column: x => x.ChallengerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChallengeInstances_ChallengeInstanceStatus_ChallengeInstanceStatusId",
                        column: x => x.ChallengeInstanceStatusId,
                        principalTable: "ChallengeInstanceStatus",
                        principalColumn: "ChallengeInstanceStatusId");
                    table.ForeignKey(
                        name: "FK_ChallengeInstances_Challenges_ChallengeID",
                        column: x => x.ChallengeID,
                        principalTable: "Challenges",
                        principalColumn: "ChallengeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Departments_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Departments_Functions_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Functions",
                        principalColumn: "FunctionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostID = table.Column<int>(type: "int", nullable: false),
                    ChallengerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_ChallengerId",
                        column: x => x.ChallengerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID");
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    ChallengerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.ChallengerId, x.PostId });
                    table.ForeignKey(
                        name: "FK_Likes_AspNetUsers_ChallengerId",
                        column: x => x.ChallengerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostID");
                });

            migrationBuilder.CreateTable(
                name: "DepartmentChallenges",
                columns: table => new
                {
                    ChallengeID = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentChallenges", x => new { x.ChallengeID, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_DepartmentChallenges_Challenges_ChallengeID",
                        column: x => x.ChallengeID,
                        principalTable: "Challenges",
                        principalColumn: "ChallengeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentChallenges_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChallengeInstanceStatus",
                columns: new[] { "ChallengeInstanceStatusId", "Name" },
                values: new object[,]
                {
                    { 1, "Completed" },
                    { 2, "In Progress" },
                    { 3, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "ChallengeStatuses",
                columns: new[] { "ChallengeStatusID", "Name" },
                values: new object[,]
                {
                    { 1, "In Progress" },
                    { 2, "Completed" },
                    { 3, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "ChallengeTypes",
                columns: new[] { "ChallengeTypeID", "Name" },
                values: new object[,]
                {
                    { 1, "Fitness" },
                    { 2, "Education" },
                    { 3, "AWS" },
                    { 4, "Attendance" },
                    { 5, "Agile" },
                    { 6, "Quiz" }
                });

            migrationBuilder.InsertData(
                table: "FAQs",
                columns: new[] { "FAQId", "Answer", "Question" },
                values: new object[,]
                {
                    { 1, "You can nominate an employee by filling out the nomination form on the employee reward portal. Provide details of the employee's outstanding performance and the reason for the nomination.", "How can I nominate an employee for a reward?" },
                    { 2, "We offer various types of rewards, including gift cards, cash bonuses, extra paid time off, and personalized gifts. The specific reward options may vary depending on the employee reward program.", "What types of rewards are available?" },
                    { 3, "Reward recipients are selected based on the quality and impact of their contributions to the organization. Nominations are carefully reviewed by the reward committee, considering factors such as innovation, teamwork, and customer satisfaction.", "How are reward recipients selected?" },
                    { 4, "No, employees cannot nominate themselves for a reward. All nominations must come from colleagues, supervisors, or managers who have witnessed exceptional performance by the nominee.", "Can employees nominate themselves for a reward?" },
                    { 5, "There is no specific limit to the number of times an employee can receive a reward. Exceptional performance is recognized and rewarded regardless of the number of previous awards.", "Is there a limit to the number of times an employee can receive a reward?" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Name" },
                values: new object[,]
                {
                    { 1, "" },
                    { 2, "login" },
                    { 3, "home" },
                    { 4, "social-feed" },
                    { 5, "departments" },
                    { 6, "functions" },
                    { 7, "emojis" },
                    { 8, "inbox" },
                    { 9, "reward-architects" },
                    { 10, "users" },
                    { 11, "challenge-types" },
                    { 12, "reward-category" },
                    { 13, "medals" },
                    { 14, "reports" },
                    { 15, "challenges" },
                    { 16, "create-challenge" },
                    { 17, "forgot-password" },
                    { 18, "library" },
                    { 19, "register" },
                    { 20, "reset-password" },
                    { 21, "user-challenges" },
                    { 22, "admin" },
                    { 23, "complete-challenge/:challengeId" },
                    { 24, "reward-shop" },
                    { 25, "submitions" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Name" },
                values: new object[,]
                {
                    { 26, "create-prize" },
                    { 27, "orders" },
                    { 28, "view-rewards" },
                    { 29, "update-reward/:rewardId" },
                    { 30, "view-faqs" },
                    { 31, "profile" },
                    { 32, "other-profile/:challengerId" },
                    { 33, "reward-architect-challenges" },
                    { 34, "test" },
                    { 35, "my-orders" },
                    { 36, "challenge-reports" },
                    { 37, "leader-board" },
                    { 38, "supplier-orders" },
                    { 39, "help-dash" }
                });

            migrationBuilder.InsertData(
                table: "PrizeCategories",
                columns: new[] { "PrizeCategoryID", "Name" },
                values: new object[] { 1, "Clothing" });

            migrationBuilder.InsertData(
                table: "SupplierOrderStatuses",
                columns: new[] { "SupplierOrderStatusId", "Name" },
                values: new object[,]
                {
                    { 1, "Placed" },
                    { 2, "Delivered" }
                });

            migrationBuilder.InsertData(
                table: "Medals",
                columns: new[] { "MedalId", "ChallengeTypeId", "ImageString", "MedalName" },
                values: new object[,]
                {
                    { 1, 2, "https://img.freepik.com/free-vector/award-medal-realistic-composition-with-isolated-image-medal-with-laurel-wreath-blank-background-vector-illustration_1284-66109.jpg?w=740&t=st=1692300070~exp=1692300670~hmac=fe185d7ddc8dc9a0bb3d2951759e63b8fb2b5face2d37a9b701296cd703d139a", "Innovation Star" },
                    { 2, 3, "https://img.freepik.com/free-vector/award-medal-with-red-ribbon_1284-42828.jpg?w=740&t=st=1692300095~exp=1692300695~hmac=3a40b077a2820ef8abcbd501fb5f45c64ce6bd31e9b657d48ddff10b2f140afa", "Collaboration Champion" },
                    { 3, 1, "https://img.freepik.com/free-vector/award-medal-realistic-composition-with-isolated-image-circle-shaped-medal-blank-background-vector-illustration_1284-66121.jpg?w=740&t=st=1692300202~exp=1692300802~hmac=04344476d0c2b7e6f7130f0c1c0868fb5f1db4702b3b548c29050323bdc952eb", "Excellence Enthusiast" },
                    { 4, 2, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSzeDJYxYcziAGgaZnYBxA2oaiksvOYIJI_T6BTG-FG0fKMl90Bn8UxytwdgFCw68uD2uA&usqp=CAU", "Pinnacle Performer" },
                    { 5, 3, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcStTpBqhjev7hkPymkTluFEPWHyDOivItTZL_bCKvMeC7ugucN2zagnjXYBhejQp8Uz9Yc&usqp=CAU", "Trailblazing Titan" },
                    { 6, 5, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSCnb4woDLxQwexc2szYwDt1-byOJn9NJfWAsSIIitbbF_3FVlBe_OHPnnc8f9s4kkgdqg&usqp=CAU", "MVP (Most Valuable Professional)" },
                    { 7, 2, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ7xZ5O1YVRE9vK7wVJdB9NT01OE1REPCrmYw&usqp=CAU", "Creative Dynamo" }
                });

            migrationBuilder.InsertData(
                table: "PrizeTypes",
                columns: new[] { "PrizeTypeID", "Name", "PrizeCategoryID" },
                values: new object[,]
                {
                    { 1, "Shirt", 1 },
                    { 2, "Hoodie", 1 },
                    { 3, "Jacket", 1 },
                    { 4, "Cap", 1 }
                });

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "PrizeID", "BackImgURL", "Description", "FrontImgURL", "Name", "Price", "PrizeTypeID" },
                values: new object[,]
                {
                    { 1, "https://cdn.shopify.com/s/files/1/2462/7807/products/cepure-ar-nagu-puma-bmw-mms-bb-cap-023743-puma-white-01_ae170428-5ed9-4abe-9e99-2431d0080808_1800x1800.webp?v=1674718456", "Trucker cap with BMW logo", "data:image/webp;base64,UklGRj7kAABXRUJQVlA4WAoAAAAoAAAABwcABwcASUNDUKgBAAAAAAGobGNtcwIQAABtbnRyUkdCIFhZWiAH3AABABkAAwApADlhY3NwQVBQTAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA9tYAAQAAAADTLWxjbXMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAlkZXNjAAAA8AAAAF9jcHJ0AAABTAAAAAx3dHB0AAABWAAAABRyWFlaAAABbAAAABRnWFlaAAABgAAAABRiWFlaAAABlAAAABRyVFJDAAABDAAAAEBnVFJDAAABDAAAAEBiVFJDAAABDAAAAEBkZXNjAAAAAAAAAAVjMmNpAAAAAAAAAAAAAAAAY3VydgAAAAAAAAAaAAAAywHJA2MFkghrC/YQPxVRGzQh8SmQMhg7kkYFUXdd7WtwegWJsZp8rGm/fdPD6TD//3RleHQAAAAAQ0MwAFhZWiAAAAAAAAD21gABAAAAANMtWFlaIAAAAAAAAG+iAAA49QAAA5BYWVogAAAAAAAAYpkAALeFAAAY2lhZWiAAAAAAAAAkoAAAD4QAALbPVlA4IK7hAAAw5gWdASoIBwgHPlEokUajoiIkoJIogJAKCWlu3ITdr1r/2xTv+3QB5v9fzBf//Ao73mX5qX7jpw9BP/Lzr//7ypf2G7/9KXT+sL9TxQ1E76SaT3d+baG0VPG428ilr21eQs47/z/LeWsmP45e/xff7h98vyu92b3D3FepBy5/if6n+TfzA9Q/mBfwf+af5v+vdfD9yPUF+3XrOek3/M+oB/HP+71o3oAeXD7OH7cfst7Pv//1fz5j/6vQv86/yPCH9F/Of5H5l/IThn+Y++P0M+3/7L/IfuX/jvn12r/rniEe0/939w3yNRPeztBH6r/iPRq7uenH8v+7fuDfrr/sPzd/f/7q8Mb+B/5vYF/pv+C/8P+j/Ln6fP+L/5/eX77/2L/l/+3/Z/Al/Qv8X/0+zP6SYN+Zi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMXsg2eZi9kGzzMX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F7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zF7INnmYvZBs8zFuAA/v6BKAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAKAXgjTfXcrTWd7RZkPq1Q0jtGlUXbtgPSzhE4SkffagYnnIfEfRSbiEVAAAAAAAAL667+D6OPGqxONsEnIeHR0eEwJp/6y51GgQoecbcXOEjGL837iyKcDIfhx8mGPf4TC2o94iuBoh+3C24+4YEzyboC/OMtAdyvA0T8FkWIUrLsAT/y2WAAAAAAATg4rHRWofPwJrOWt2U4xM7nVFwA5cY9ke+/R8EQEW+E2GNIAgZTB8s6Pq714rRYyDqssZrjciq20T4nIQvWlWXKT08Wf5MdnnBZfhouvp53TjjfFyX8JKdv0BK5IGpN1kZxd+JeTv4sG0zOxPHErAnEe6YDXtZyAinGLp0zyzdELjKGb7qFMJpynB+Q0ujysVmdfnbcPJxTIe+3HFj3YaeSpFEoJ1tBIH8s4K24MDomWFdS4EyBr63F2gNo9PlrtZH1HM1Oe3IzuNh7uhXYeVTWsZn30MygAAAAAAAuJw7Vjs7OflXwUrvmHpMyITsOLwhdQ0VOBeAQDDWaxq/+dGPNkyJdEUeImgSqVnldCy79Ie2AIaKlO6WV5na1Xr3mY3+uQ+up1+HlYbz1wLWikxYl//kd++/pPFp3/t4ZQkw5HQZRHvw45pcAa6AQEViOTAU2KpDbUyMFBcrPr8pZtPayTdZZxQMQIsmOwA+hAs7AAAAAAAoli+XrlFyII1gdsSxwo5AT6hkqxdJbxSoHtRdxVWXbHzR+QDsMYBJAs3BmIw8DichqITIRl9YRaTzQWix4l9ShN8SRDs4ql8y8BJFaC/4AC/8c6OP5dIFt3EAtQ0g9NY9O9FC+q4/oAAAAABS5YtPOct5pA39DfQ9DZiUQgT2y9LwCHjIM9QK4A1QX3TcKmLAq30k4FjE6tBUVT97VP/B/ALfmogIImVuQOX6KX/iuVJcfjn+C33QDzvDCEl8BgAAAAAOHB6hXzXusolW/jdDrDaI8jmwQq4K9guC41OGQ31zcngF+DGarcSa7Hgr9hYtKT1GL26udOrFGkrzU1JzAQWbHvBQi+aQQuA8A7xJ4wuF9M04R67i54GvlpBpjnAAAAAJ3co9E/cXxI22c9W17gUghkQfIybfFcNFPao3ePeQsHO8ycs3HKHEhOU4pgDWqrFB2pJxgSQQJuDMyZ4tbmIoXg1ZgUaHcymg0kBNdwxnLBNIDc16IQn4Y8Zzr8QqpJjQ2hJsAAAABQWIlvPPp4ZWGios0Mt+tAtTGjZkYSc0odri+oLQhRhUN9gcEFtot6Ib4kcDu8lmPfJHZoBdp3zucxtDc/zxqo3sG6mF/6NUpFCFgRLMAemlYAVBC4kq5W9UDoJk6igd8MmQAAAABQeTnwGwkLT2L7m7bZQGbNrKzEW+hQQVL2/A2MLwFmINkdBPKFDo6UXIAsMILw2nJ0i5QAgLD1AJcGW3U10z+BCw7Q0mS2JBlLosT9IClMrCHIEn3kg4ZcdP+YYJ47l8+Fgj66xFnEqTKhz5RZGVh+1unec7Bi904AAAAMtv8eSP8ZDDuYkQzqzErbKQ/qKSPDsiUXoyhQNpp4AWEIff9MXaBSDNoVp0M+QC/j0QShnNVeSGeRJdY0oKApbaeXfNQQQfKD7sawiy4TySsFa2IG0B7ueQWjHp7fSFVY0FHxGu33pHwBxiBVNOSbc9NDQnPRWU7W918D1j94McSRQjcADi8flRAAAAABBvYUB0sL7LO5lt60khDtVClKN//NZBQ8quh0aCeFgPfHO+NKfkia4R5E0F4r6YzW2zfeVKJ9dq2Y5HiaGAHUhjm7ANFQ7XDpw9MF7LlDld+yULlzYa2wDmyN6xII8qAAysvogBT7mznk0PtzgfFxerXYgQYM8IzLowF/SSFJNJ0D2gk0LemvKUl3Jh3xGtfxwzPk/SH4wpTj+peiur6PlqUHsU6TWJMIOh0plHXi5rpvjCd8yYkBb6xNMja1nOOErEJ5n/Z0AAAABdMVgf/tdgpj/+D23Rs2GSIA0IWV0rOGfGXNIvu+CiaEWcRW5Nx/AtA7Glh0uqUFzU0KAbr3KUeshhx8G12QR4qKTKy7dvx8AiyVFbtMGElqzopyEmXS0PTCB46A9fnhSz1TwZitdojEfh5b2evBOE+0VAxvlLYce/JpmzeNMqw44291AkQGtUTNz+bw9p2ij+R4n+/r/kSsYrkbUNX0MmIV9PFKtLpBYtur/WYzs7w51L4qEgQjgQIvx77n/TkxtQ2aZ9IQMrRyisr/2VwjmKqN6C4Maj9fjEm83W5dnpqIgHLB2W/YVrmJCeFzUVyWeJnb4AAADz9rM8Z7IvvE4xsdtTTgZtqHaZGdMHMmU+OVCh9JArJSimAgAo0hDDIDV8shARx49NPLxFBR6WUHJHBd4ip0Z+ei7lWjTx2hrRpsb2XiVGikoQOsAB2Ypk/HaCzpHCWG0om/2a1M+t8uguMk6ZepL1jekSA9iPskUaB0K1iPYbXT9c77iXzi08ACUZXWfw5/d/ogLAEd2wEx21aJQ0Phr9nWsTq+knZnWMtqFEAF50i8jGKmck+ojOeJsmRHOcqBzu7ruD4P97GCGdanU7CRWDXoJlKGsHo7+ASzawmgtEcoGdg5kl+nlNA/t1ULqpv+5Ie58bC7UmtGoxieYzJoCy6E/Mkq1e51QUBBqaDi1nkzPiXPqVC9nnN7pNKSkkA2Vlf8JnxVS/iYY+Uy2Xd8P/RTMPbNArptY+zP9zBxTuwb6D6PZNEuH2ZQ4KuIKZyVeUIMY0ySJEBnP/pM5cRIhb9+jYPpF/DbIIF9oy/AzPeDBRMJLu/Tr8kAgVaXTQ0CURPeZ1VE8dFJL7ROQEbCkP+vIUbAg9gAAAPuO3uM5Bz5Hogf7/v/DQ1GSRdRGFqPuPUm2c+n/2A5BrhNlxcaAEhtrXoYTmdsgwkyItkydUO+bZuc629LdNCawbLmA5jv9OjaNmywTfKxtPFu8sxoFsJBQmdT/v8skx6YDwtjRY+OGzdxM0nAA8W8A0dMiTcZVyJskBwBpXXSn1LFSmjedt7rjbH1NJ3x/d/DpVas3TGQV0/lH+R1yKgocT3C8qEmL2Jklmj3W7giaRlSg1s+o/K1LlSqmQp5Op68LV/4OyxczcTzz3DzSQwibkDi5q1L7bpxjRS+8WOKmVLm6aUf85CwsvTEzfG8VGld0S33xpCYbcU8G+VjT+lN0p5W5X98AK0f5PQ6CBndMJ7Z2Ro8+VyEfy1gm9mwb6zpteYsMCnb9rqXoLC/4gtXjZwsAwQq2Zr9clmlv4DA6Eei9RH0RRNl4e403/qyFSLiMgBodkd5F1aSiYWSQ+0S+DPDIdNmOq92gmF42DZXW+f3pw69DWLVv0wJJn5BfdOr7vFAbxO9BspF2L80HLQnI7eKwSJydJRWiQMBtRKCQ5pDh+snEPrzsGXkVnI5ycVBwzgdd0S4mWuEbwf1PDdTI3byguUG4ys+rKRkYxa6ChYVo72wREMst2YAf3ztAaxqM/u8cSrJGh6qyb9yCMhrC1jsAiP0hoOpi+xaLpCgpaiS4NyqIypJqQohcgPmDtTjzIPBkZIPUW2pU6jqACVME2yuGw6cpjqvmcD5fCh7ncz1lyKyD4ba+AAACnqzUd7KOyuTIMH5Hpa/tC09qdfZNt1a8A58pvlX81i1GdJtrvMW0x1B16dQmQFZ4QL043gCS6F/3tTg9qyLhRAuvDGye06K7Sop3Joi3fDeBmeWNBquMUFiZ8TqvyzOEfdWocuebsHM0+3zGoPUBJTkijMcdx393HSC883YmjCmlZ4tKTpZ1C44S6+o9JVlE7ejXo+mvrZQ54kZvkC1Py0/97imfvra9aTWrPKgrB66hGlPEfsks/EihAPij0UxFKpWeNjYp3f5AiChaNv7awymBI1PH1MMf1C1I67awVJ/J+M1nXI1Li6a5YC1E4sfbAqngF30s1yHYeRhP97r8AVTbDFlUBWf68YTFDLB2IOC51hhq1x4I36f+fzGhw8Tcms0B+tjgRDtOj+mCyOoKNfBY8ezghAXAs5D9wzBNRwpgXBeIyogBxAQrl3NeMBs2tDlm5bRoE/7hx6GYHV9j702absCq+LIhBtqswysFJ8pgtbRFxjPMUaQXGdNRek5SIo0T+F7slgXJJLCXMta+K4QUiM5J2mlSj+WEA4dXI03uup58Oe35Mac9Il+Y7O0CxpKLQSzbp7X5Q24B5ZN3H1ZaZVTmYhYsze8hRg0JKJlyDhzHCifr9nq6Y/m+99SghvPDqZ5fL9qUKi7xCfensNV/YM6XHCmTcKAl0yia2Sxa2mheukXJBPuUgO8RcFAYZTDMtjzUVaAzNgeQXbiPGAnZbx4tA91CkFnjFfW5XvplpuGqNbBWoXqS5oM6rS2tX3TmJbbde2asAPnX9fImIEpQ+uWYEocE98VMdDItxbbOOATCE71vxTa4srBGuVkOo1MGBTl8GGuaCNNWaqYys8iqIbNHcloCUiwibWwNXcr9/0psoKS5GK+xH9cJcEur2lOViGMVn0CUaI3xjLyqmJZ7KFY5g1UGWZIG64f4N1/0XmbvDJ4TdTVTvSCq3r/karApdqumWfCAAACg8qFpjVN1yuRYr9KZAw43yxGCgjRQrXgsOMah5xriVul4kgPjncR+Pl8rKKTGtzI2mqDK4DHgNlpx6GoKKHVaWW0aELRgIXUI16zqsBMh1nqkbQiZJuYcYiUKe6UbeOMqOGNYEfVK9f3T4hV76WcujahIJn8EMl4YfRwYLeWWeGsoKsntCRlN05RirEGnqDn5d5Zo+zOfiuWO22l7g3KRJLF6x0bhKqPWstJKMmoRk9ibkzKX6r2ZLdtpCZ2cbqmzsVtNCjm/ritx+0o+rnnRrks53fBak4x/XWIgb9wfrOMqzfKVBXtVDnPtOG7o8+rm1f6BWwx3hLrmr9U156en+qlJUm+WOjaigT4dh1ZwD4+QGEue6m6FqzzK0cw3Kmv/X+a8wyVojzvVm1bi833DRXgOSQjuha5iv9YXZDTkczIv6HXLCr+uF0H9zd00CWamI+AT36jI/2PSlbv0EXLPf6yKmXPpDx7ZM17sM9q/+iED706PcIywA1DXanRdtXzvEuVN/Zm5O3LWGp+W11+314qPPvJjUyLRHte3Ec8VBmDW4lBd5hg4Ig6vSoRhhE1zadj4/y2DmXg2on8VgFvb3fnMqwca1y4i3VxQdbmEumZe8fRU4/kQA6zltEb9bKSzuv0yFeR22kMir8VYNG08Phkd8ZQb784sdVheYLYIUYstBDnA13KaNfbTVd+oEFrHKj7Oj9z6U/HWH4K2FVijs+6nfmuXlaJwaP8Zy/BA4Dd0suUX2sVaWTuqhO6hvyuBQzhXkdblR3xSC3XBk554dnaiEDMbOkjz01rl+ReQeKYjdXsU+CZmxJq6/71DVdnkYxd5aaAewJFyGeRJxrb53bU5268q/jgnIgKk9LOeb95gv5Ry//5tbxxYh4ppiNKdzkYNRmr6fy5yb6rD21Sys2ZACjB7vcS9Qla3TbeK2Rrsbuos7l1VTt02eDgF79+BC00GBAtdtvv26i7uIXYSIPRMax6+iZExWmZobGxpGGkIHEkkAo/qhCxZu/joAzc5//cNhmNh+faHyrZWgs167JetqJviOnMTEPGu/KYOWq+CtnRsYEIEKIW5G5TnRlUL480/MdE63hOekRenixOpWdGlHhW3tynFTu3HruRt/kxEtsBFJyP3/epJj09P+PBJ9+6iZn2r1vHvg8jAW5eVbj5xK87vE6iHvPBRk7/1rhoagmTTbAAAAOB/BJ8CNtgP/vJecH9n2+em9il3Hrt96L9F6MyNZWmvAD93V4Rb5VtzrhEjfNOu6r4fseZp0ERsHdJ8KN6DLu+J+sXn6mPXDwnywkmDtfYsjTp6wYP35hasXTPrPrs9gm44ce2H0Pu1E7aS5RI7nsPnBYoer041B9UOHpxpYC/5/liSuV6/cnmzu+23wgC0uXbtHe1Ac6/Lv2fpZq3YFydk1aAHAJcR8xDIQ6VNDhXuNdF5WYPnAkZA+dh/sSeNUwC+oMAKMTuGi1S0QD0+IKsyA2m2ZlF1GnzzDC5ZzUQSRs5/7zp/NSwhnFT4ozsLKWjqv9cQAOmrm3eQUU/+037OOchUi+LN76TuziTjpywnwTH+mSFpPZqH83A9gpts4ncFwP1KfYAKYM6HBKMNEQ4RV9f/ince5xiuIRWHOmIcE7M6pp6fMD/nSIVfIgmfChl63/5nrltqWJqchJFPi1GG2kw3C32NVCS/wvmR7riS+P+qfIqhjUH0TcyNFL02+q52+O1Uac7pIGmxXke/RpHYZMdO7eWE/D7z7B8DeBee47ZDjLptrr5mfKG/1PBZc5TFUb91Bs7Xus8bKrDcRdCSmt9JVAYLkdb9Yj4qQ5RE963G14UltuCgp2ExK1S25g3UfH73sTKt5hhwhrF81CXjN6eCkdD67q6K14mn6UqpaI8v02Pj4m+O9CHG5P+7wwNGaWY0ZuVC0O3FlPCnU8H9M3MghEUzEO762FYmh3Je2Rb4C6B0Xa1ZcKWla0mJU6f0XrKzjO3hziI9sP+bgtVy7zMvbpRMaOdxTT6umSB42q3XkWMoSEur4n9JDHIlYWfRL0Wl+3XnVNeSS4Vs80/lYQOc8Ut8YTl3n9Cs4WPWQFN8gV5j2Bd47MtI1mjykNrQn54jhitne72H4l4vn8sTjDCdt7IxE1R/JLuEMowBuHVxp5qzn0ydtIBkR1nDnWYww9cAboi9ZGKvn5olNkuyxV4MlD1fn/ZS92fecTznWS71Cs+Nr+MvFM9csZpLq0LFJfM7rsF+855ZwKtupKrpIOvvgwp2yRZTe3kyqfh2XwLDroUOGiYl1VqvcfZv70UHt/7PrStgeXf74++qcwlFgygBqJfzV0+c45QeP1GraKfOx863Jm4xcjOw8zt967O6wZEjHGCgg8IBBwd3f3XpOGTDUc0wzlftFilisBC0PwxcMYxX8zyzpOoNGxsaJ6jmwWgEbCWE1brID3w7yTdDRcTzt/eODqlMbT50u4BrmEBxg6u86MLR4GkZOXnVvMFWN0X/4dPt4B9MMINTbZf+726uvBxpehVinUS5GGxcpGUI25eeideLuGgmW877RtIsFxUq1LFXrgmw5rkt46GxscV6T2/qNCunfTyFe5cpIXYNnAJ4ZEvxDAOdkvGic0SDXIMeme0VKyPzOxwKjLNSyCOjQ7swq1ed0s851PslnXuPJDxojmLEOveHKl3kqbLN+ra6K7fPkSKTRAXJS27K561UVGxyy3xyWcTeO4FIrt379pd8E6N31kEQGyXYu82NLWpKbeKFtE4j23eV1tAAACVCXhgC343Km+g78neXEbF/e2YNgMnG4Rny2tJKC5zDFq5tVedFH8tP0sg2HVwin5gxnsXnXfUb0ZHE2bESegC5L+P+3YB5As9fUInETlfQ1QYYot5PVLRCiNIAZgtCWk+NfuWRNtjRE7RKfBrN65ISBKsIZ2xo5X/+oKXWa2X/fJmQuw6PAzALzusgCuGplTv7ZAB578m5gQeNJBx/CgN3pcZHDcSftQXuvCWdHWxDEbXQj7urzr/0LZK7LsFvmDuB+xGeXddFL5Y12bhPl+hA6o3X/WIby1eCPGo7gsmJJwflQvP+OZd3HJU1IkquhjUavtlnkWWY/Kj5j04AD4PHNVlQV0nnQLA7mQx0s3wCV8RBc1P/ND7bAriKXgk4fePqrCgYejJVVMEEpg1+6+4me38mWUES9jo5QVPIeIfVlcn7FoTy9cFmRw7PhXoSTxCCdPwA68BUSZVxABRKoGihFdG5MaeJf8iAlmwvcI2UjPjln3pFq0prKYqpY5zXP2xXLkhYJ/BFnSaXS3dHGU0SXm/+owYmT/kCeXBC4zhyLsULv/2iVV9703i6GLkNLwaKBNgbCSroGsyzQ2Bvd3sBKWsvJ6+fOMaeqzpPdI+GcEXUPoO3QEzldZyKf58/FTUqE5tTSLKlQfCuTp4K5DFPw9MyHNGii/nGxj43aApseRzl127vcIFzeBksWrOaaQ2wJ1btLzIHvZogw1oU6WuT9wNJT40RBSnBHB5Q2U+rVy8N6aUKkOgSg5bdNJQIHsVE+RY3vZGM9g0gxaZT5f6hGkICshwbfkIrQ0w9tuJ8twD9nME246zll7ohyELT4a+INkWvCIim14DDxd2XOhK+oX2SxROvW2D3ilFPxEssPRn9x9QjRvt7PChGdRuyFHhjzA3rPtTchq5naBZfTnlpTgQlqXijrPkhK3RcMQR/7ApvJjM+g1vQqE/FsWm8BUnpsIg4ZFoP+Ix5cgAC0prrSB1FTUbBBc8URk5QGCgvQ2u1nNQtM5GJPyHZ0eSBM5FdoDaVagpp+Vi8qlGCXH+Zjfi5WYn6MxO8+8NO4amAH9KuTvSmEsfuatekImHeZLLMFZv/7mNrJAf7XMCP0P7iSmlkkXfK7AugnPxf3X+KQdq3KZZN6imC+Oi/niKdG5bz3H9t8tzkn/ROVPOvtaaqCvQ/SBlMJIBkLL2wAzWIAgOyPxUH6K0sPNXi13wwJQr/ivw2dE0vAKqTaPYiX89JSDfns7rzQ+ObsfqmHbCw2FcPDLruBUt6qYgJu0tbN6Pw8sTg7g5pKo1m0paW8frprcVrb21/gmFwhinfjT2n+89wPXptILoLSmEhncmhtdeJMtxjM+bLhbG2sBHN/8sYlk5hu7jld6zdlbKPe0cVBXk5Z8VwlImUYS1I5e7D6Dhwh9vOd6aI0/K2ruYkBn7AirhiO51bdNVomfTpGmu8dpIZfb5EvOYGcOmsIxdB88fcy5Nq55m4rcUfHyRqaE7zEWTkF6GXEO15IXoHxJWkjeczxpiIpw5IYOK3gXr3kSpLEFXsKdVvnAqMjWtz04mm2LF/sGTtCkGfx+Trv9yYDRRqyxBCdVK6kAAASy5eFUYisu7i0a/b3dMyay4Sf989RKH+0k+yaOTWH3/PaP/y7Cps/gF3YahLZ8a390MtKNzeTd6GfzYq/KEhn5bSXhrTNF69Rtu4sFatbH5GcHQsfTgGVac7HxKD32JoDueMJsWR2Ay08Ps7QXp4zd9zqqRjxkkXdo0xolAFF7GXnyQzMq1nuXaIHdTuaXdclIsk6wfwgVecZHJGrkvCiM0X0JPVTcXkyR5MaCSL4vTefZFYO+azYQjNPt0je1VjMORQ9Y0CeyxUSBA09dVTd+L43JFxZSJE2hEiLumhBwPeQ7GmSMf3JfH/Y1RH2DJKx28yrggrap5/cWF0PDCZnIBOtbjFx3TH5hFY1u/qVWfFGaqS+WEHLi1bkQAIadp1zQwkwQnRpilZgmV9P1bcPlBZfC+8kIUB1i6SERXtu2PnalgTcMeOkLH+OK97WSkD/UxP7m7gZ+S6JjVMl8YWOj/64X3Ire/+Nt2kosZHJ8vU3/vkm4P49QLYrK0wfFVoQOs/YnJawMIXPD9QKQq5fW5Fq7jVQk5wi2iUuvDLKNGP9Ct8KSdWhAgd+rBKcGNgG9ZvpwINWW+qtsD90Ig2BD5fGeTSGvD0//x2pnRumg9+Gc6BYligSaz3ghf9ioJrHshw6fbLbMIbxxIOvsNryaFBvyC2NKGboYZ2TLRPCWXl14KtXTZSd9N2XfA99d/bQVWUj8EJHtATzkIzaTvSAaBUekcOCHLDMKhNUy/pPXBz25wK/RYHbNpAGCrVnz6cx7+zqvFzq2AVqcfcQxKfFOjBwvSxs9yPPA/nA3Z5TWTuYlCWiCRDgKAvoIj/E1uMOriCuJE7EioY6qG6l/TJMnO54shUZhydDBmEORpEpS5msM4u0RtNifpi8ub+81P/LDiJ6Z/wWjtUJdnMceWVAEonocY8yPommKQur1dT28uzGtLSXkYTP+10a7fMbCEKaydRSBgjTJhIyYUOqnoj2B/uZFSbQWrANeSQat5UQInArHltnTCPw55Nyyx0smwyv6HRsXHJwh2+4StttZW+3tUOH13cMuol1dS51mLLJvb+bAc2XwJU/6pdLu8FZX+DHMCQwPSOx930YzDdy8g+jaiSYod0jc9ygR+2uRVdOCSaITqxyFmaVENay98grycMo6jwZ1W8NegfvpmtIS53tJp/WMtfYmOxPEE5G7oskz07rbAC19zHLPkDsicUMzwZvG955y6bkMmdSU56nFXk7l2hwqkHfKP9l1mD7UGbwVq1lG4qRmQogyZGAIuDn30lJZQLir3xamz6c3etFuvREOFuVZ0m9GYVmja4dWJbJeWpiHvuXExYRfz/59v53HO/APVz3DNpLSRAurK+FXlDlWosbytQoqtONMtWqI94av3clrVIXSlSDE2RtuNumbo5HmmJwEUn+DUO9lWV2WqHnI+Xz6oukRGguNaGh3wxO/j+nCYtFB8HY2Qv87shdGrjOdHbjemGO/nYXCuneN8mbFrCIfm5PR+klLdda63Rwnr2uZlbjDGFq9XfxX/YQh2X86Fi+L//amv/h/vBxXdlozfBMZZkmis4dbaP+BqY58Ef9JlB+3v3DhmuoAOC6F2nieG92jgfd2+x4vpHhJEP3Yt1kTYTR8bhmHIZoEKl38uTWlE1EgWRJch58ZiB6QlXQ6utMAyMzchj/yXW/5gAAA/ePVyM4Gz1VyhedQOAyIiMYilMyYC1Mgs7B28KrOcggNBFJsqH5gwCjtoIF+YqvNZ55ruhZ+omVUR0erdB6H1vLOHeIIJx3UcMxnYXivOzNDzNcAVRFvhEBYC2YOOUff4W773oxmmnmGV4Go3K6Opd0Flwr2/2xK0EwXx2U8M1QSKefuuWniX3I0Z9voRKk7wYYMkPE0HUIGwZA7C02lvpN5W6msNaX9WkQuxE7UK8mx2n/IULw0vK/BN6nZsKvaPszOvrcRWYYfFm9rd17kTTXkg5dnAfqUmflWq5/XPVfU0/0DCHaf+v1Z8TsPsLjTdnp+F7ePqLsq6k6GozquoyB+S7pfGnXXh8mpGdTbDPunMByWz2y5LOaOpzWQ4z9Bf3J2O7nQZLOXSKPlDFtjkXSp30s2j8QZpIky3FvLrGHcsi6+WT8zF9HeycBQH+GXwurn131RdNttz5smnuNm4gWiS3jP/+aqa8IgZdpuB2MFgqayXpKbZHHsaAJ2YG37Fiz2ZceDZZ3XIFnk/2XeUysN++gFX1cM2N1HIJpIB2jCv9PbdCdnR+6ahFNkX6UZh1LYXmL/H3xfKo8yI07kK6gqDqlQ1VvaJ4TVi/0zuWPvVg3JkYa8K/+o248NggZi9VSdBuRO8guYXT+TZG4UzdbxQXuQzDawRY755AMyChidOpJf6ysFXpQDLshlTmgVHkfBZru3lWKIH13cpjacy/sgHVLE79ua5jXgcfBDhAk+K/GEN+0OQO5w+fsgO1+rq+FKmkzqwlqeHx2bPQeEQKQI1LtZSokDOiCem+9rX0dEVHS41mIaHiWAw1VZ01VV6XSygXkSIxrc6mZko0o0renxTdOmqz/I90V9mtsXlOmrDw0q8LMkRJN4E2te/oo6oEaKI2A2tAx2gF/0WsPW2UbQ2eOwAtjAsXCxIbzzM8P4PLekC5mWCVvjPwNXXH0IFpkxgJDZu6CaT54R00ZmcBodzkrtnS3DLX2tyHL5JJ9Vd0ab9+gz6jXBUo28jUDsNUKDGRh1jbyE3aeGzAAG0J+TyPznlUgU/Jh6shgdGfakud7FBzeZaOcS7oGKyrxD2qDzs5TicdKdn7Bqrg/kwnVO+Lwwb9hqedUQb1+m31A8K5NGpq0BoUdPx6LF/BbyHjyAwvDLd//jOcs+It/h1pjQQiw/zg3ao46/GHOzG+B5jQEg8BAKIq70RdIAw8X8H7LUiS0ZpyblwznxYxDO/vK3dczcb+X3yW0i4eHcilQ0F8INZjjsGd974ftcTsrSMmONHw3EvSS/kQY34rsQ1fvrpTd5o4FlK72enceVo0ql2OocXqWaNUjSU5PgmUNQHrr5iPIcEEVijoua9UCM1A2O1J3dh8GSZQd0yb1JSC+46C4MYs7DC/rvxcL7C+IjnW99jAS0sohsLzt1VNl8dp1AfAfoJORgBwvWuzcnBgkC8sqhHGXUkZW/BLEcYvx9Cgmg+yVYf9TpXG9fzdI8xJWizGk0FlcsnoHQUeNhIprDpU7KZF+/BalS/O9wWz8RFOGU074F90u5dPGpfk9Iliu5r+cMXsyo3DvyHL09/bpoCksdlJRZ0C+P91EnJUNiyzxrXE4m6r4YXeta65J/BWFqQd6K6a3WlazsEBJ6ypv4LSM2vOwN5QOX9yWdnXAABTO7Tgti6SeixdRqnz+8jeG9cVqIKBHmH4PygdB2v9O7dHmGolQIIac8CpvzZqHMn268C6P92E2KTfocUzxQ1b2PKWUGaElzykSpVbGirrZLoV1nCTpWTtNaxmw/8RR4pBOlqDXoI4kwIZZhAQA3nepPmP3JYleNpVhrKOyHnul1yts5qlKFGgD/NloNJ7trpdtbPWK7I4tVoXv8xnBPy8msSx6Rl+Ac2Fld4i2z/GruetR4cEKFdQzdi0XcyBVyIMOhJFUYBBXV7xbIs0dwtrW06c3HjNNytvTbFCoi4WRLmJePvoroEHRloMqwvsd5xEDNgNv5FXoPIUmGuzw5JGwi+w8oEarc9yar39WjzqRth0IN/fY5ooCA54BVxFsU9Mm+avuO0BBB/xrKE5wbmdu569SW1wi0wKZN2/1J/yd5jmS3KQfS4pVER8SeafrLn11F/7gUx4tRJibkWf/CBaqIOQH9gb5FSSyl6+h0gj2UgKgHPInlmgtNAvPFmtcOt959H0CUvu8sBxgdLu/D5rk6aAHqQTffdpWOgvX8xmt0TQJb5mtwVQC4byVmNpdwC+PlzCQKS2W9UO6JfAfX/ac2ElxQff4ys1WKNrijvr1EcJnLT/qMcyYCWNYcgjPSMscqe0xoDnohix7z+oaU24/aHChnovz3xgpmavSDbnafxcnzAKdKe9clXl5Z6oQtux2zxj+kX9R1YTNj49Bg6Ybrsbmrnwp6q8MBDE9QtpZNedKleue2Pymxlr4xrrttAFQajZTkQhd8AOMNLPbT5xArYf5oL80PAdxu++jKUTClx/p/HiPlXtszRzme9WxmmqDB6ILIxfG7v8c9nmv3h1iJMPRghnF0iueX+MCED2eRpYR0Kck8orTT3JJn29cg8aLAKPDHv9hnRCuIn7jI4J1T95sbIqPoSNA/mOHZKiABjmQgxJEp6pB1dru8vFFaWwkGsYTMNGCChjF3/riIkYs0v9+jdGFW2FD8BnGv37RwlFbILpRA5JXA4DQ5UMOw8xPidbFwklLYh3Pt5A1zS8kdWL2dGmyxdHzCZ62TYCPQmxJ0drrJ/WHsyYdoIHF63vk7TIFTcDz2rXw3/TWYjJuqTCuNvL0SZjku4ph0TRc70G9eY11YNUsI2Aidw8kCuOmLEuPBcV7uAt/APTmKS9QI6dR1DxyoVGMtMnkTqCGuaYL2GixU8hYE1KbSajrzUstiF9kYpwp6NIgiymEEZJDPZcBJKSTFlm9w7jEjig7njOfwQVj9HWfjLuD+WNTrh3UTzsGJGIkqNmgGDSOMh0MiFpt3E/kLzzUEkQQVcWuJlYdb+kqMV+TzHAPIbY+TzOFvOs9j/EaH6zYhidbqX33nwOKd1Mjh1uwXwuW7apX5Tz5r2Z/2wQkV3VYuf0H2yjptuU/jHNx5WOMjWyz2Bxg38rv1gkOwSmp2+CzzGPQE4DtOogoUMaHxMxZnJJ5yWqW0rV8lC3OmpvHYU8ZePr1Ua0Z3hzQplTkqMmtN5+14D54ENdP8xCw0RG9oRTXXzCUvPmob3X8pTz6Da9BFemZiqa/QUuhnuSaeiJONgi0sRA5R5/nnnao9kKJPU2DK+Lk2rBFA1AmS4CVlbb+MpaZEDGOz4l+gOAj05uK6ga6kr2Mn23OEY8Lw1VvxAKSMBkatT4/nx56whWRagt73D/OBEeVj1vzwyViBgxsTzhJZq39TQRL/q/tHeJDO+7mEdu7YW33Or0wxiCn0ROXhjMiUOVD1OiMUEBIDSzYaPTzp95oxQxeycTwai/I7brlPFFL7l2c2E6G/s9ueLtpKdUTzxO6bKnV08KMEuXm1TnbM+zKUch8G5QT+4AAASGFF/L6pH6WM95N76ZxHtOKfDvx2Ola58gCDUgkvFH+BKGRH7PCYQ+NiKs93anmoxDTMGWy8WZsSFEUO22vEX2D+TooCSRWYQMU6dImKGSCp4DHTgyJyFSfU8MMt+8/EnHr0p3O2+DGSuRm0p5UMWSsthNfxdSEUorVG0aTAnLsjH3KissqpV+eG39sbnJUKbjFhZ6n5I5Wd4UqeHbXHm04MCTgcjAq5gWheTAwA5dyMz9UNATgxhf0gOk+AuyNtORW6emBdw3TVWOOSoDxzcf+K0gW2o2XmMfrMtVKSNI8Oh5gDjRfOm+dDgRWmknp5E/fG6gDAx5UV3UTh2IOwVq0jiNymcwUSbBiLYdmAPYbzfDh8lKquA1k4g7VS4YsXEpJhgCx+vwFP1teqA3JLX1Z77XweQ377DJ6PiosmtJuxm17kuWjabv+qaFiJCY0xpvNL7r3aTbbGGQrPlWP+oHRQW8ie5wOje0Mhn2BncWvdYfXEPxI8qYKCSmjOfeRSs+CmbsTiIko8+Y/AfeC26SIA8awcS97yS5KJVZrpnnlS3fAeudkACyqb2Ex2TUfQ0L5MjsEiIuOBJ0I0wBrwCDM+QCYwDr3Wj+R66wHe4KJjqL3RxhNWgLWmCU1tXI8fAwD2QLahFyj/Tlig7j61M4jp5VARyfvz5/e6x85Oebwt27YBeIkzJHwW67Ft600RwZLTugrX/C8kAzm6uO4O2edF5WQJN6N64mZr8ci1N0MYg1lTgF2JWZ+VicBi4zYQAdaySwDm1tQzGO5JcgNVqfX+1EqwATNSZ58QPiOZ0rdfv8lbRoqBlWBZV/YUWnBuhsXtdSDn7ZP9lQGfcGXycQHNhX6Bs2fI2tzlPhJBmk30DjiwqvMAvcxRog+aP9hsrhyCkFpuNssNYY0CpDERbrmtNHgfePtHNl70Skr2Q5auEMuQBVefQDQtdpf9fJS3M8dXTCZgr267hvPqVii48g7F3jQ6t3j9T5YGWcvsTooIBvOIui2kAMNObhQJ9cFsEQwWCqPpLg2ce2JKxC9zj4XrotRkKosSxN2wi/WR36QhgDccVLnoTic9Pe5zq4x83NftkjmW++d91guFRlmkl6L69vMNz0NCRzWsrEkE9DTEP3nVOTNI3GhQ8qSmCptwKORE9GsuqPxxVaBHfRJKxThYIcOc4u1lUTQAeLi+fNWBgyxwCPmRIR0RQnB8sAlCY33QHiK4Hyjzmvi/+QzgpVrCigEb7tFuGFikrD/b6gdPv5D3RFzBfNcygsgfVKUXSq68bW6VQfUPRrHmpzfFwRg6itAkNIeOhf3dnp5nl6u0QGt5YTTTnrYl0AcqQZ0xGl8BBDXwlIxoMnQoXDY82ue6td9q5Wdnz4zJwfIu9oGXVbCgSC9eX7VnY8rwVU650+T3n7h/8aLlufi5tySgqnsfgCblGa4sS4qj3UJJ6PtOLabr32LC46ev3rCR8Y7wRfvTFpCoLyY3pZv5xwzUY0Zdw2GUnin6WEMFXQ+dwbQnMiGJaYsY0PYB04mM9lPr3SAcT3zfVL/w/n+ks9PH1ldHMKpDz8Vvxo7q6zDqR1rcwccOWNnqTGOWxiSSL0KlacTcIOWe5TH91j0zMOPTWtF2x9FAckEznj4f+IFYLJutZKAAABEKUmI8ZJ/avn1RFXH0TqfQHHxuJnzam1zCq6ftZJa3Qz163tv/3lWrDCFtEP/kMULEHz9DeVvzuH/ViAi9G8RGbX4MdiiMh5ZVMFo4qcxok+okE7ALsSLSpdRl8kKbDI9g+zxQXgdhu/LDa/pLCl9kTDwI2D6hDfqe7NGACYEhR31FC3OjBtLHJnFf/SH1ivQNcMp770XAFaM4JMzF164lPFzIK73Jg/4JZlCFXOXPgDWMXYARVWijkf1JUCStQRfiJejdiQCezTGe7+bm8+fYy0gPmV9jtiwq9YKZVEuFtP4mulyy1v4PwNqvhYLrvlemEiEA12bzHgTtyZDiWqcLfkFjg5Vj03pthZojKokyVb/Tow/92gh8FdrTNikApG/EEqFnefoTcYmGU/cF+PtGRfD/NWk/tR9Bo3Td7q7rRdn+3Syxum+civ1Fje+ia7E81iRtO34pWfuJhbGW6JaYGcOVWSOCuNh4PcjfoYcgYE/Yq5hMTnAbI8aDubyopE6K5qZZlvDfsDjCHgf1GVgmga53Lo4aaUuLWnpfYACIq+vKXjYBje810QRqevgrH9QgsUbF+08XT/8gBCv4fXOziPlhPvLz2Rg+wBcKtNzF8COc+ZQ2JLXbBdw0lxigMPPX7JK+Amqr9moeLwVspWDsNFCzLNr6mPg+Ua1m3FCPZ3PaHs3uy5kYwdKFuHaPWhcE6FucKIO24S+ALiw3AJ/6AHnT+A8gmiViR1NNlHCdruUvhjeU+fWSLTWCRM96wkDiBIIQ1820Pj1KC8xsLhu1J9vgy7jbR0sqcxxyf5+egn4l8fuuFOYhkB9FpRDHrxh+VN+mYjZWHkYlu6ISDVEsAuoNSzm/5zsa7ujYlOQ/6kxv+UkWjl/8VdGYOAlUMPvin1Pe7W9GfNjML9FCan+RHihdnjz6EqadeGOYv0aHQ9tt+xEH3aSX79WhRcX0LwtKMvsRNQi23wqQ3Fea0a2FpuxcW4kFEYiLR8mYlH10wwlo1/fs0TFCH1S9wyPWKDRX3IZ2Fx2HTHQ4eV7nsfspk7uptT1r+Vy/0K5Ss4k1Ab5U/I81LOj6ys79JMTEvZRrw1dNcbnjNBdvBtuWGnMPqAq/Lt3CHn4I6giGeg5BJxdDo9qRLXNslCEtVbYxVO9jdHtVLtEqK0dbFOLKDmdYZgjzhvmkLc6BfvNhHWM2JflXk6rxzZj9b2LWpMxA3huXwW+2wHVpjRi6Ni6XuoNoXnQMiclS2lww8/hEUjWkri9IW2wLeVSUVxYf1hpFHCWwZMIs15+9nB41eQh+4c2Et+Z/PB1jcUhhXd0RTAqdSQxpKU9D9dMsHOkzECYLqWPRbHzbPEecbZZsWjBbevVWD0r7xNGEilW9T1BjOTS5qk9BdsVeArur7tugRZJRgMMHbqoyCkWpAPEtYWeihYSsEqYoX/2BAABa69au3Yx89JgQ0otlGdn9zRpzWAmcnjHotY2+gPTZxCArXcE7qwbQm7iiqmX+rPOJp9vqCp2vQAfMuXOUr4KYhD2rXkgithmhEZU74bawq0Kbxx5C0kKTD5jPt7bmfu8VYutOdG3IbC8mi42rAWIcTMgh1IcHHggWFnQ5tDJO/HvqMSMd3iyvkTMzYcZGT6Ml2UgMksDgFprjvcDRg95W7p68yHaeBfi2MyHk1a9ssftPi7LL5OdOuORhPGSqvtlq+o8GiKzB7g6FE20GpJuB5oq9dmyS3lX0mQSplTt6l1C3hamwejBeeWraZfM1abkEel6j/It3HWZtJsMSayGYeqDsz5vQa4VGViEf+n51g9fQiThzqaN/udhEGKI0viGI5fokMeuosfgDhlGflorc6Cxl45Lw1jOpXZzw5LWAwMpuBh9wc7C9z7M+hk2v+hKLq8R9hHm1WPrHT9Gys/S0pvppNONMt/gfHd3P4shoUOTeau7RfycA0AbcoMEVtfoH/KpV++yji1k15tmv7PAvl3QVGXtQS0fnnkG+whnh9rc3UUuUV2khH+3hYUq/Cz+/yoKZWSqsg3uDSMuwsh+WdIUXYj3ic3nsrq3trqdpfo4DPWRI7UmsMgvIzhKUpEJBim9g/ZFp/Cx4T/H0gHFZKxmoeQsWReqpDb2eNxLbsHsORkujBGVqMAZ8u3G/v/fxm+48Xf2tje/FNIJX0l6qkY19NhWWIrfG6b4TbQJiUT9pBgijv/hubqA/hqxk61/3KLpCWCjI6XRuG8qXoRiWKBopino6cDdntP10NRyzpZOS0Z7FJH4DttAkBH7o1YqjfyCR0TpamV641xMWFRdpDFgOK3gcskXZyI3BGbUfvN71bMGKpaiqLMwVOqPi99+fszCIlCex82Q2rLFahmchjmAy4Liot0EVCFRvJAY13KdeFRUBRcDtZQvsfndw9VnzlRR83tGTG/+07YXD6fLw+yJ2RK+Ne+HEuEdxGq1S/mqZjq88TF8ZWUoTAPCQ13A7cJgo97SP0z4TUq/FKW6TdZi2aeDmiOo1eW49AxmcCtgS2UHHj5lF3+XzIu6QEK+zzRjwHHqAss3bGYPwjwpXd+FD2eE3KxnFg20DRO1pH6CrONsRNg7aQv8JFUAjlyUHoMhLkrzVVTzxq2KR3hwbwYlJ+mm7j1PgH2/khJFugIkGngj2SZagq46RkGZNVgPQ2MLNsM2RXIrwNWIcZTPmZBaydaON2qpz0Dm1sJPzhoQNQYhXYHyoHbk2l+wp2UAAAEwL0DGE8R4hDXkupzUYYGogtWYK1vFnxAZdfyK2mg/hwbrtPhjyxIIUJJghP9yuWoY20WmomTW0zhG7MmXb42zxNt49WR+8KH35hkGZQg1JOTtE8XaCcA0kvdVfTddMKXenJtCyM6qafaLwL+Pj/TdYr4Brbe6A+lxvUIMwP6QgBnkDiXLAShafkbIikDFO9MWZVE6nH13SmsL0gTUlL1O+7G1HUu4gG/5nilazAE6lQtxERLsKRkJSAvIA6Fb/2h7XrvLgxdFF64dKYcDnbTGg+ZFL00Ip2ER1kXRgv3pDMT9NKJtPCbW+4LwzOby2tQ2gDtjMtzodkMYdSWPiKJwwH//akPw7vGmed+OPAO93RsqT/Vc2pSRdDUnb7mkfjD8z19DCXBRJ5tkL3qov2WWVdwbannw4BlBuqQsKD7aEDdBpd0hq/HamLhor19QK2uMNqrLIB3iFPApuLcIUvFSyPTBMCxb9WG8FG92bxLE9PIU2OevPaWYB2STIGlT4SZyJBEMsyCc829e8QfbcE45CLv/tq0JW5Ec0hBjjv91ZpeA0T2rWS1rIrqLcY9If3xoQ/QtfoT8mcDoftvuFSo3oZE+kpfJPEpiPLvSElP9Em/V3RRIJc+5mpZS+56KLX9MqNEbMgeHPBogDmMdDg5yuKQhbgpNuVhp4Y69I1oz21hez0rpqKGlMkcfrQbvLY3atji+BIHh2rX4e1RnKviBJxhiuRiAHdmS8cFDQU981uOxlyZQLxbQX5R9ZR0+AsYS1kxZ1UbquolzLqXgtFmwwmocB0ol2QGdPN62An8mfHlqa1NNSdVpBk4+5VA2URz6fMCflW8ShP+LCNADGm6WDSRUEWmI/iIcP1/DkLKCQvwF8msBthDfbjNJ9ACVFswmP09De9nW8NfWqWZTn07Q/3cnEtWlpZiewzQkVqPc/kMboKSn7gRZW9ag0Xt6Udfo6wtp/ENZRq+hSf7rkEthP3GBnDvWt/bQSSIMaVzEqaWpvyHDwxclxzpd7A5rUdw+KdVpiN6pf65N3XvOOSB2lYbfbA1YP3FL/I3ZpZO1p5TZCH3m+eZkj/HGfc023WjOEVC4FsDKHqz4wY115HIR0kDWYexUKmIy38P1u57k6Yb/dQb1nZVUdWp34KvcEhGq8X9j9kixq6eA2jsAAB6D/It7b0/cEJ1V0i587u6TNw0pN+CMgzauPEuNN7xgN9JC6+FSxyZ/PBaYvXKKVhgx4Ds6hleZ9bD82yUWFgM92Wrm629R5I4YjfAzvIVmEn9L8Hs0Fuqd6uQVYtbsiMxEcl1Vox5s3CETPazdjjqw/A87xHvNhEdtxZun/YyvwFR+0GeEC7AJg5BsjdT6EBehgvCz5dcpD4im8adWlVrGkJLgmTNpttYIQyIf0suqYNE2lfKzvPN1f1+eX6wsc4lvuZ3kPKm4oAsaJBA7aTxypqIG7cEBvBFlugHQwbvs2t+nLdWjmq4JpOefykwnWtVeReNn7MnM9+ylSwo7hjnSLJO73cIAKO8dB/RmoBMfQvP8tmURiihp2bSA94fnH9em1tocex9xwqonxBrZpGRsUXnlwWcH8bPXLvKJD3iidqq8gXGctvqlsNyvQyqM7gwV7t9qlIU7S03e1TFLqv2+xFVRs441Y1VF+1iMi22gWxcaxDhAGfN88myoKz7+VL0xl0vqvvZzN+JdXjzwX2XkmSE+/X7voo0VPhcXHz3TQgnf3F/i0G2N902hXwsuBPCSApQwyKj7FHfuTVN+9AnX+X8Va3KGs2rtat2SRBNB06mo+OwMGrarjoh097jdNwm/0Qx4pL6yoKOO3IkzihxtoEdt6vgzdyQucX7VbS3+3ClSo3yfNS7iFbP2jXt7sgBx4rsLMFkQgiUmNvJoDYer1oKG8I65BoLbQcGNbCa5hAxcXwZFhocHxN4YshTJ78I/7ZRYTnmzw0GdPyi6o+K2ig1QALX37zAbY0j/AbviWPCrYJiOoPNNlPZ72yjCNLz2qw7hwfdEm9rl3q+c2jhZOSXU580KdkjWdN18HNMDzDqA0pmK76GLrFWbNX0O7UFl0Kiu9IFJZ4zJoYeB1RkHNbMAfNw4brlIdJhh5tpBrzEDqhQaapZxnnE9ofq273QuGp34iqJLID8RHnaMLOTd90SEPyqPI0Sg4gt+saLA/0T6i6yGCdBqu2DA5tbsVIS4M3jouM15FqIa6WXKq/JwpuO68oQLwHA+6aHz8cIWJtx/hcI+ZPOeeHJTQH5eRYjfiSWUu4tlUrZzZZSWGzp/hJlw+FqbE522tHiZZErkzUT3oZfDieKL5WGqaMY8KxrWc1tT2BcdNp2qJTpUzrZmKVCA0fWaS3fT1/oyvVuuMAFx2eu5eZp8CAiB9k5vFVsMCVQ2XYUTBQHMdCQPMN3cLVRjUKAAAkOebr1zF06HD7bvKK2kNn/TptCah6y2FwCvC8BdToX+u0e1+zmlEdB9l9Gl+VsayWsUCjks3l91PpnnZc9cVXhZ0zvWWQ4ZfYLf/yRIrf21t+G9i9kLfVeQdOdQdvYW0o2e35aXO5Its98m+3ZgLuvoH16TvlcceAwD0QoI5zEy+5OKkxOeTFfoG8ln2kQzwli1HjlaN7P7cAqRHI1axR4q8q4wfTi61Q4Iyn/jQLnn/NzFh36NMinZofmu6wnK+ZEWql7FH3OZQ7ILCAFPSha2nAlhrWGmotl1giue42mvBJZQbyjBOsJa2+hhngE71irRHfL+QriQowwVb3ZLR+gnplp3+5miAqA0DhgqXz3/U7uLKKwkF0k2X02jypOM4eJ7LOxcjs/kkgsRtGuO5Z7ZlBOdojFtuac0dixJicPvMQ315a3pBm6g+0d4k5ztltOHjPOFNfYcd0ACiiewfCyiY1ZmqWcA+ACp7ING/iUGOOu88n5Sx7BoEyFgpw/5KO/mTHa64IP6d6nRnh8aMRXFj2/X0dDRBISF4W65IXGtW84qNK3pVd9dPKr6FLeED9MTJLa4i9KrsL8PLrzLcG0QCGgyeF46dMjDgnITXwktw07CY+WA1eVDYk8fiFi7lot1AKSemaUSQA5C3hlRy8i8dSUzKdLPxCaUkNotJDBcNaEUOEskJyxqNvWo3h7IgHMI4kETyY2rjsLXPla+exi/YWH9x5mwFEeu/v49ckwzEZkfNxAsp4BnSJY0XrG8nxYf/IFwyy4DzkNGh+0V8LdeM3GUnuUPbWSvXVG1c+B9KPuqVfgY3X5u9fB3n/LIpfECXoIjskraYBh4wPXgB5m+Vwcn83owq4qDBWgg8YZP+WqYyy2Ts0lwmKvunl4Sw0N7vFf6wKSVjtweMuf08svK+J9GaGB6a1eVj7qIwVUwgV8Lw5Y9WQFPFWRd4Jwe6Jq9Ct9lwXFU69Am6aM++i6UclVA2GIUQOhD3I154Rk+jTrHV+ZEvUpq4C4QKDhkUWWNPrscWtW5Z0X5unjWVxjcxpZHJBHGVPTrIhHKb+ljXJplg0jvo1XyUVRgJgTcyj5AI44ETchIalOQ5gUv6r6o0lCG9C0zI2NOLsCEcJWZibqeaEOFrqp+jAXt6xYsA2ymEhiXUYNez6fqyMX2xli3ydmkzMbaL/pXTO7zExI78YAmOMg5QFnYXxBP5w0WuMfvUZQb5Q9+SaBIdtLZ9+/DpTaYSj8RwlRzq7Ezl9TtfxWWflNdR6CfvktZNTm5/UmVHMmtsvOge3WKdZOl82CP7sFUI5jKzJcRQB5Kwj18l8wQ7/Bnl44/8xMP4KM34anS0fCk+hcw5fdVQt69uRmdWv51TmVw5hAVd7p0Ga/hw1PgBHzZPmRSj00ABKAcweOfjvHagl9KTAmR7pXdgPbBUNwVmZ9E1Ycg2da2QT3U+ZGfkZQj+VHlHfgv5q0eNTyrfIC0RHRNYpZBGV27HBYxGhFP4BmrjVQo8Yl73703Dg+9xABuJjJe8ghku6MINsivR+3cR8E/N0ge9472zbYxLs5dyNdpDY4uMdVu1+WAAABFqb05mZoCJIfk+P+tyFShZz+NYx6SFvQb9uLvJFzxwIEdJ1rjUPP6AOUIZAd+oPbnKUjFhhjqPZGvk+kpuZbvl9NSI9AjueiaIrf0iur6XLHRnhpIJN5gUSbNHKFBkzOdJgWsqsI1NcHmonHX6AmM/yYucsoaSejJY5Petiru6V1xO0OEiZB/T27ARyBp+PTzKEXFtoH6aM1S98bUCPBnnjSyNknjvMCN1roYUpJHBvIug3FgjYCXiwg6olYsbeHJFm+Um078YREbUxopJeuvVMlChBeKN5dlbNp8hfKJLFdAKDCfrvvCeUEUgPF2dXgLJnPMMdN3R2Vj6w+HtCy+HVEfDXDVT0YXtkCYKhBH7PzzPb1r5b+/xZKDFPrjaD0Vm5+fPzNolY2Iw0HYveJiRC3LWDn8yA+HbqDA+Lo1G8U1EgVaJZxwuwbwL+FOTX96BpVJdw1Jogv8tNsJe6ntq12BqAyVhXRkwzjmtWx/VLVY2nZy79tU9X88wcwX0BxAVglo+BD343ckawnwtXl9X0PDVN2DKQyYvhrG80DF+khsiNO7TFQp3mO6I6+GUYepYBuLATSzQG7WsL7wLgcN7TVtN6dGAmgc0K+EZcoy9mNj9ZVRvRCC0LBkNJI54fxY1uInOEbL8hrFHo1oCwlXqNRU4zFcEs2MsfKMJfIW8NGD/cUAJaQyanbX6ARV1Ycd3s9sKkXQbmgOjRZvTRudooXA/oadlplKcoWfTi+MLz+QdvAdtjqWceEdd6ik+QP19TpqcOCzPlbfyLd5+KziTFP34icX/5e65sRQsu/s4uNjl7fmLRgHxQdS+kaGIuOeHKA9cXGMtTmioEn6ewa1l+wNglfYGwe6jlUfy4bhs/ydqI1D34xJuSRSsJCgoNuHAL7/Auyb+NkzPPPfue1oRIiwz0p+ty+TxTMEk9QXQi4irdiR08fLpHpLDeLI0buC3RIFFlX2cZDEhb2bSTcu0b5pgbj6Yjkw1+3+MR6J/ChfeyqZ+FLgrt/mYPt0R6g/B8i1ZUaWOrFrOMrJkLBSNBir5nK7LDVoYV7TUgPzS22GcoNoJozY2ANhYc4rKoDWoHg5AdeSNM30fq9b67LTPATZTj5kucMYmkNk38EdJUuF4n/3+M4DkCi+k1Nvm70gA/pYQ3WjGMAJbuJrXoRq+0IgBHnld8YNdVossvvwvLRpOOCqxr3/dK25sJlLwJwIGbaoHQdZVoJEAO02s8VmyM/5BmJCAZATWaW+ciRn5AVlVrAqhcdTVFNe+O0EMmugvtqvtOEsvzpsV9yqz5yMpo3jsbdP0ZNNQrEHneR2PwtU8E91tWxc64HB5brFU+adImBT/RfnHmh3cWjEVmRyXxaudmKE8ctK0lqI158RFN0axNRW2pYM6Pw4Q6R5OUdDJFfN/JPUtVGbHWsO14HuBW7EM0vk9ynhI2QN8WIHo3um8x8jWBdVSkE17i6iOjV4QJtTL6IVvE3Axpyi9RaFPxFEQEmzoTtJMTeJJllnhMbIAyfugU6pCckggFnQ8Cea1kqtByozk+uZNsH27d1lI/gGIncrJVDI0hoa70qhUUAwvZHIsrfXSbibwDcR6FNerShvKcwTRwtznjN4uAYsh822qMTR16S9o1stwfbPAVPBKdjSo7+QeOwbysyTToXPKZj1SIS3SSsi6nfQ9ivvI1/xHELFPBe/x12TQf2wlX1HJrXNFj1dQX0Mksk0NDDDG17aaCJY5tW03y2rkfXMo9zGQhOJvh/+6cZasn2mMkFvvkuDy8AzIOqFCAADxtNGZW/1Atq3+x+V4wU33qLFaWcwyFP8Yl52Kar13Htb+wStT6+BaTfiogF3npGqVTTuP2IpoBrsfC3z50LiAco1d/4MSSk+xoVpf2V2MabZ8P5ityS4EEoPJ2J+7Fcvg5Hc2mTRDYZIBNORSUhpPB27F1dh4QokqYHvhNAqWW9t6NTCbgdacvaxGZaIDeJrlf4aSWb5055PQw7mNz4GL8ynTnM8rQPCUEyt0nMkHmNkhbquZIxJgWsGJwP3Gb7GLO5pyYgvDxHHf0zTbOR4hdlcq+LjchCRv7aJSi6rjOHjTaUMbpg4Q7oAywXKwwe/tw4DImgMJPuLyCxxF99kmNWzppoOQn/VVZgNKz8PIdHFZUA+fgRPBr3XMnpb2/R0+QtadabB3bWPEow7D4Ex/lXOZgMQZKxrFwXIHiFP2XJWBAisx6MedpVLDmOOpNcZpRqUs8Aq1agW6leALm3Fi4bqIyqMuApmuLkKOqehCRETxEHK42o53g8smwTf1rhy9PO8Nh2+R+Iznr7s9O9Q8bO/At7R1qVq1Y/rTFb7xWUAugqJcL45mPIemmO1NQidI1+9FukV42VDLMQSxGtoFwBaJTcLBMuDMn/HkhlUY7yRYX+EUCq/uevf6Tsftp4Tuvq+CggMO24UARER9ovuYcL3rX/gG7LdqJzGTG5f9EEplD9mhGQM8PNwaI4Ay50eslLJ+uEW/uRNSapX271l5TKab8HUX7S6a/M2alXHXObAparIe42ulr+79s8b6/0eI6+fESNXoqgj75bHe5AbmK42fitqtDxG79NVXDx0NuV6cg4RPNOJSnemBmAciKZ4EejeXepAEchUMTizM54OUyHbX24J+/hnS3bMmsdqtTwTH9tEUSQCiyHtBDyxtt0Df8sY+WZN6pq2K0eKX47p9TUy5/9HDo3MTeR7GyCPyErrMKoStLvG1UPff+eho/VL7MUNOY/u6A4dH3Vgh09If2gQdXfmjeyf5g8mFpposHRZomwCcmpyVoyNHhptAmba1jdk0LbcHkXDGp6Gs9DqwyPAw1ws8dLHnjo7kOQjsRmmQ0kN+fpCQ/z++udCsmPRzRtNRpgRnfZ+tcoL5ybJRPqpS5EcpXknyzn1V1T7GOuT/ONqACuLj71+4J1ZG93YF1v0B1cpO/QN7mwrlGtpaBWwoaet8DO3RjHbnf8tYvlQQR9N72BVcQHogePegliTXpWOdTlmPraNYaym4Tx6RCcuGusoNiHCgGQmQR9jOuWreMJW4u0EgFnJKwh09kusxpUdjVpf4U8OPVyvpPBi4o9ZZMXaKR63VuCnXsx+GaeC2wodQ9eYQ21RKPP1gvamuF4Wn2+1uig5Fdq/Fml0Mn5vx2oCofLTAw0VmMQwYHpfBWYVj6CZ1JhB59wO9vqi20VCLiH1A80zlhB47vmLuAaOtf4x483ZSwNaXdzLtwL5Nu7H9nj1PM7vQutxxLDHOE41FX4rra50QTAk8ui+SPOo0xNnnS91uyTEremY2RAO1OQnojZYsI8ImPwGTbH5ivEQNyyZ/9vefqxgG2zSxpgiBbrZIFxFJXy5dqWYQTaGPhhE4CDg1xPYq6BfdlFtbQC0hFAfUgX9V1kE/CDZwWJXvra7cm3Va4qrc71d9YZbgRQGL8kX4+W0ixraaSWZougzj/JAs9uFNTrWP1tLM34lfRdtFO5ZvjaLq4uV5RB8KFGLZ8Bu+g/H7RwkG3nQdOx9Y+qsayfYJ3zWzUILAjV83naHMIQqPpsyv9a+O3GW7bCkkL//vqk5v5St1qIKLGPNEOrsHTjkjVuwaeANgGNvVdsiwWim94phB2l+u31GK65hczW6aCFi8GV/fsDJompCiS3U79vSOocQD89C7hcdXqB/fz+PJ7C7OfTKEZQCpjUnZqvARh4NbcJoXPUpaQxnNxaxcOIMRX535SEzkNlKXuqZtkjm//y3nm57ofoA2qGRGX0YyPBvjAhSZuMpACD/pxGk+E6lhIlFFbfcVEmsnLjxHOI1EW2L8FEEqobIQwGb+z2c28td/LfVTfDdF48BLtIoIDm7pSCe52alo2XSRvZZU42jqiMzn6ylDkkPFGhQGupwtXOGDd/jbUK34eUtjcuVFNAAslgN0g9Gw4pKFeXQ6QIABUbBuH8E/Bj9kDAwxL9pt/V4+O81OXVTTKCMKbcOnqaSp956mzCQoYpEJKRFb7U6Iys2CWR+vizQyMwaSHsuEAH7NFTKASyCEh1WRXjmXMkNscB5uo6wH1LRPVoUchyc6ObJzamzEmbS97KZ1RfOwfoTbOh5advGaK6uzRdYxktDfdy0YrCaUhlUCXrYSTpcWGgLVqFFeU963D/O4Hi5uUn84oR0tfzwazjOEZH+1tLF33KqyPKOj/HnyHD862y1sweuRP6j0Rwhv3dZefDmyfEgHOnEOOAQF1hAM4qHshLcyWxflMQRUaTJHBC5ClDL4HSmISZGG2ZxgU1S3OMlBnz3Mq4cX9BOWCTrqKsur6hcUoOZWc5Hyf1x6NWRI1ykCwUEvzh5WOOhyMEuJlx7vL/HtiIKYhS93YgsSZnT/9Wc2MtDxuXZBHbsEczGk0ws1cxn0WWhHwou7d7X+Tlw0bHUd6aGVuUpbAGMWboQqLOC169EQwOnGNY8ahu/VOCNtEA6vV7dTb0S7XFJ8Jey0hSRd8HifDXgeVOGljVd1DKNRjd8XLhykNGWEwvvg0wTJfKFMzCC+Dwfe4pD7qksQctM7pxedCFu1mS8ptbHQi5ldNuemMesXUm1XcguEyYO1KfrUyk70T6QF0lSNALiEvFYreJUbvwqSPndiwxfgtqaAYtwL9NQVyWSEUapANwkR61bQ1ZH2kgCTuSVdNVCROiJ4V3Q1usCsRfWU1Rd8XTH7ovsYEBmvaHk+OZ/fAgr1F1QWJISB2wAjpA9ykdEQyZ7l+tJwyprrlGB8Cr0sKAXvQ1VApCSfCD8L29vQCySpQjQjmcuY3IMIMaARtAbAYwKZDEZHMuu0C6yByrJ0Rx8wG2iBD0iCOuPVXECsKA4ZQVCE4KHwepvrbX4UlezBsnVOeAJVi7P31BrfwrZ/Fa/4lq9lA6HvFQA1osMUDwki6XQ6duTMspjIZtHBsDiWVa9XyTH8NclocrF4/PWBKZcik8fdMHIYmUe7fROtAiVnMSEpxrJmhwYzNCWW0Ddo9HODoHmYif0UdsgpQLaO1QuLS/ZwOyZ7VL6+x7Cmzb4OtROG8bzh4TFPQ//vqczOFXdpRopHgQY+oLKMrYAhRFPR2tzSjX5+UgDxqXyENMjy/ZF94I0fO0UXXBsekMimXrUqV1knaqzc9DpL6nYPBH/n80DDr5yBBWrVrw1OsN92TEHJChIC10x/piIFUCfCP5ovUJDDgc+Hh2L7RhKzMKHnoYhRuSQ1nSBlRWf6jfJraQNc8ns02k7ISzzysksb+wS+aROivDmHKyeCF0ev7lLxujYUWmk26Owi2z+l45i77D7u02lDMv4cm0c7ahgiTrPXdorxDHbiNBwwDnVAdWJXwguNWtAjUtkYxF6XfDLJ4LDq5jbgh5QbxvwjFyrU/6pR4R9Z4g01mBPY5HadwPq1sk5SQ057aXyxyYo5PzNJTpcLgHfSonzmZDnbcBwBRQ+RHr7nG7/39xvWx+bZ6q0vN/Vg0Z89+17bwhZZr1hcESpzGicMnyrqxPEzD6xGjgbzRTIhGsR5PyI8D9XAfbKbPICdl0MDiZBsL3Fj8kVg5Fs7MSekBL26GkV7vhxCr9cOsv3dnj1c6RGCrg7mUTgg1GO8y4AatdVFmS5xEWbXJ9IC4LH+/LssJ0UIkpt7kGzoYADleFlCgkgK4i/WdYOlMtx1/XPl2UQlFcp+ntI8ShrDylXovMjdSXK8yYQ4Ph33O5J2z4oeZHyFULD8UNRKp5V76783qr20MqT6yd1HgZfuk/bVwl3oH7+ahJwiGY0XvciP1rU2Ewvl7qteJpODsM2+FdAOb9Tdt6Av99Vnv6MMJ/824rGduqT89/SO+CLDUqf2QDGjAK5pcq4Nqwv4rPMswXcM266rXAi1gjmVUTKHQmwBtcXt6BuIk4P0QmITAXs5tnAcccmbYgN5qS1I+PbKz7cExZ23xMmITlIf5H7msRQ9yBdpVF5PGWigib391jDLQ+9GLLipYUJRDu1qg2QbZOpBIQACKmcKhggq18922PAfp3++6b24ZDJ3lzGfJF6hi+sogm/LDKK7OAQKJJjcOHZje8i2RZoDV6r8lhG8QNpmbo+sDP/HKvQFEn2gFXrECa94nqLkgKSgCZft0GG3axw0GFVYYTYF4HLJlOyHNpsDKYA2tMg3+DFvgzAAih3elh0irU65Qe9ywdx1BFW0n9k4H61zshgAR6+c6bobE23ycSyXg/256Fk23rP+DmW6YWQ+aS1M2QJ79MLsf/8o3u8l0HsKyPV1ia4TOz8bqAq3Oid0B+RE9yuuolm+Z49/d0X7ehqPXKBTO6JwEhza9fgsgUx+yTJjBdsfA6kY5d4Ukq5omLdozy0EV0OtW1mi0pVsClSSVwJi5lhl7T47MdScss8/Nme6UGVUEL0LLaCwYiMVBqgdj4ENAjElCY+jkP8kJBd3resTxTgQEerJyov9dvr1dw5X3igNVpl6yJCTjF1elvDS46Gk9uRCzheMYBGz0sCzNWBl8Lv0QGYPKU2g9KT18iTcTSZSFSjH2k80ntFsP4q/YN6dMmp1NYRctb50XGTrE1N0YqoXUuWsGC9x6ema66rgUJk67G/GS0I8RQaqxH/NS5a75V5YKeHqpl2lMw8nL3XsnpkWevxhdwuH3cLVKDajJbd9O20msfpBRIKyoOYLuCE8c87QLyCqavfMClf2Z5TTbm/KevmZVpuKZ49s/+rshmGo2ST4bubRsFYBxQe7PYGRqkzpZkvOjyJKTb74KSuS68y3TqwHy4O+7EXl0m9afacEaDfA4saga8YRHfg5wl/1vYlf6p6A/y4MMc19bNAORIJThsrQ/J6jwcoOXURTziwMkghbqPZwRhpGI3Un26omBmmjx6meQf2koFZEhabmF9XoamMqRuLoJ9VcxrvOZskf672fQisyFn2CFdV03/NZsVTQ6O6iOvfZsy4P6EmRLkmi1NBI58XB2GXzasC79k+YS1B16r0sEWIXzIVn95eoVTYh3sA2Eb4ulqm0zZWMt8FGRpRliU3gsf0eh6VTysODhfbv+AR/uYe1+9fIBLHACLurnl3UbPsJfgHWmQGBoeVgGrbXog1Wczd4ByXzDVrfHGW3f8t5weAY/i/DFQbaZCcj7pTnWFZsAqTa/ppOo+G3UTofppPwGSGT9yHI9ZocDzPONdKkZiBKGkTdLJNs0CA9J7NRRG5bKDtNHgYxrT0QKhhUNXEngpZvvzZGTfwuNSR647i6bvA2ZBC24r4i2LuO0SwMhgNrowk5LnzW+NMo6Oat2MWR/dV5Vs0NnPDk2lJGxSuFKuNI+1ZaWcmm9AuMaug9+cRrHD9pMHUA/HpV6bxxG1qrYBw9c9nZTUuT4++efJ47wGP+RXu7Qwg4h0MMyASfSMmBBctiioUvBMJ1uITAFQgxkATBfa5HqljVr5icEdF4MhNaOLa91rx1g6zczazYONOHCQcupjRNQA59Atr2CTPmyfmBYn/MtBHmhX/C1mINa+JIdvXi6XS4bUYs+DMPllC1cAWs2EjMzWrBNlwbdhn0iFKcV+OuggvniiKno29Ki+1nqlGZdTW9co83BlgkblIG4d6nxfZuM6qXAqNMhAQMu2iWM1GXU728SGNlP48xhRZ3Mifv1eKLm89WWHBc8uI4jkCnmtk9pOXyOes48wyudZNRi8xr4zhJYxm0g7b/o+k1XnRjhU2WbFw34KZ2ffHfaeDGs4kWa5UM33Sz/vQRcCyo/4AAAOTT1G1LjmHEN0Cezb+chAyJU7V0A0wjOm878QLylgOkvajtACZanjG8LVDHNeHsvuyHYrB061uTRX+XBLJT+DC02BAkDGR9phFtGfPfEJI/KqQ0jdfKV2FJTyEmlPUNUHmFZG34kv+txu/pqZoWFS7Mqm9ZoAOhB22l72XVO+wo9NH6GjCryvoYpePKhSWn8yBgeLkkKfybqnNkIDvFP3JlFS3nTQ+IxH5iV1zPLAV/RHab4OOYjC8S8RhP05fN8V9+lY5d5+W+BrM6LsnejgHPW4jdc6Y4PqiOXsoJqNAOcC7SL0xdbMhDxA6hopD2rXrqM9djFk8A/PGNm5MwJqh/3jT4pCWWJiv/hnXD5OfbA8HnBEm1Bd+AAl3ykIO7e2tIMMABDKNNFtx0uGZtXjZz0/YKsIv+K02Wq9bXqnNPuHRnD+wKieou9GQtvTnxAJKUVel49f9A1gEpwjT6qPFXjEEoEAopPuoloRDUwc+tifSCHcLFbXxFYl1Fe7ZTvCQeZ1l00P8fYz0tb6Bc572DVFsFnDm69s1KM6SeObCWL3rRmzRhazMb1XBNms4X2q76oXRK98SqiQVldZFmMEo3V9a8GAru8HI9ReNhqv7/nwn8GLJeuudobFZwkS/y42TwW8CE9R17b+5p14oie9dJRIy0VuZrBQ277UZKAJUxHuzB6MhPbL18LdiLv5T2V1VjkOD2N65hI4vKZDT0hdWEqRtZ6IVpv8OTzjyfGTg5msK5I/4W15FkDWjvKQUlcIO1Jiw2yek275YXuDivDj3A2f5mQrgQ6gNMVLcffR4jblO3ae0x72IDvIlk2LPMRs675C7QngRbyLTtLjPL8G/5E+N4KSVDKUi0OUaGWB793E1FgQsRs6BxMdkh6qv25r0bbmIAQRG45ihkBmrPKCcV8jF8qi1JAI3+wvESCQ4zWo3CMxo3BC8qnrio2GPZ4cv1yTgjNQYhUPl+iBRc6OB2zMYKVfLGPtN6LFUgcC1f0JIaQU5t4Ovuv5PNBjpuc3v9Pi/xCs7DStA1Q9yM71FSwXafcZfha14PYdTsH5eKP+5ceqK1INDo4b522syjIBH+eQ69nulcBI05sPGFw/t8dBTQH5DbmoHG+QDaC7s8tr/flQBI3sQoHgZpl1piLYntRA4ji10b94shbe/JOQlJcKWws8wGsJqgvp7e8nK3Fxlo26CXojeiV9UpNaJEbEXASuYtmNATxqESXDb3BjPvKaPsNax2XTxoHvxzA/sBUyvJijkoa681OOYQRhe/Ne8d6DQCqD0+nQRbtomTT4SXOrgAV2kYVcTKoLKO2/F2Ag30qLnihiQ+gYoCn+S4urtWzOdfMQtwXasm3pjUBGIk78DD6PSFncUam5pkYNx9jTDo/vA90ah7Re3VhkuVMnnwiEP5boCD4zQ07xTgQm3J+qoh4z4S6L1yeVy4E/M/a59KtZtWqrpc6gjDXPZ79H4j3/a91phabAAAATflMygFPHDb/Mnm6+pzdrJ+qfLUt96lVbRVNiKsPIOBnzPtto0vY7DT9ghScVCj5g89aV0oN1jy1giM8lBRwaps9d9w6I3wNPsndHok/DlbTO6OdcRWXXLhhNbE91KMPb0PL7KlWi6OMJsNmdb9nYpuUDjqTz3gheUpP+/j2Bspl+eXIZhtyPH4xtBMYcuyZngoEsw69tRV4eadLHuDpXcT9jgUNiiZsjFcHJQLx2a86QqCkRD3bIcRWH6eCU9LUS8NgNIndMLW5I7EQpmtpVMwUvKrTbJNiJ93P1yDEcmKvWTpB1QxAHOiplx8uJA8u2ECSCx0h3oZiULgM06jV155tuvkBpcPAZWYgq4R9dV6CRiLJWO+5Fya7j6GD5G0yCKcufoBLCz8mzy/uJkjlfTa/b/IjinveHx7X8xIyTuTe2pXnrP7I2AUuYhHkqIwBhX1Fp1nDX/87j3wO68eJ9gHQINDX6KE9OlrDrNC8WfVbAmKd/g782S1vL23o7h0tImLe2eJv3VyXm0BlFLCpUjhPKuP1DrnLVZVOS9i4WpQFYFbLeSl5CAb72VpNqoiHtyXUWDuPDJsM0/NdK31T3ikLcnQgu6gzsRA0fZashQCvI63vlgnSoEMpAxvPo3AljfXPFMRsN1xlwZlcv8FO2cV9D3TS9w36YFNyt7SEMUxTb7CoRwPkZbJunaYIaHyEFJvf/8Tpkw6N6lRCdLXxtqeOYJON4mhuaLSq0O2J4ngP5ZuSUWdNS8KowMgQNjsQ39KkDVGr0WhaH4lDNov1cuEv+7C/suL2kQsw2gFHhXbvK8kdwf2gbnDwssFaP+XK+un1VzmCQ2TJArDmyEOjUSeiSc9r+kNck6mRMM55A1Gl3bHAONLAvSGKOhiRQP8iXRVCtL3Ya+HuEdspbTx+jfx4HoO1cl1A+v4S2ChClO/hhXZ0kBDqhaIiSkUFGX0SmcdWN2CdCpfw5ElZe+wmLOyFc3jSQSTrum4mzdEX2FDn7RD1Jk6FmMk12LFT/2DxIkM/sMJnZgfNr6gvvUtIxctnaE/Iqti9XbDkZmL4w78f1CHLa6HLdEUAT3aBP68k3N8AW3FZ5/AH7H0iHKlfqZIVhm+sXKyxElZ9YYPpM6VkJrBq6iRxNYnDycqgPvRjq7SXEyxO6DMEicKmh6oT7wvVYnC1UvNijM3fjGTh2DJdpx+ctqk6A7eOWhsH9PgAAGhTfwYtIwubHXR/4moyaWCis1Yxwc1Zwq7ZtU1PwEqUEF9UhPA86obzH30ETEV6zdxtckvH2GVmmTfuoIFSc5S+fvDE5Yrz62FDEwu7etqj/PJC1n9dzzRBbL0wfbj2kuGA4nHT8Db9ULpvnKp5SWa679pSUjxZl+tpAkw0FiQ8Xoip3H3mowUiG48Gw1xE0S50OpchOObvLFdetMgMTF7dzReuJ6XvqgsUZTIn7T5DMWCz32S+w2gjkhKtGN5ZOe+AWFl1DoQZrVx1SRgdi5TDfb8gs3at1ElWLnEFJtIzsX77QV2gmJ5BTNDvs6jqIHd1PDQDdFzzJclfO7DY9FVE8cER1xApYwNoQyb9N8kV8LaL3HGhG+j2XrRGPfr2EJ4BNy/Ka0x2bueEIl0VPk5rXc2AwRehY/OGAa/APVjYIOjZGBII0MlaFX/W7tvjYKnUjyBwPqCkHq0N+Py6sT1+BNMwMIubJn46Z679XEpKhgIaHf91nOZrvSY/iXcvxiIWQYNK+/kNfjhWmoMdQpj1noirvIiHa6TmYV9vWgkF60gCI1nOiQPOiLyqE/BPjkbclfGOrkdUlL+bnSuB9FqSaqyQpMVU/cYZs3pQ1KGFOCTvgN30FFfxJvwYJCCwPz2yi7aGcILDsOKBlMvNoRH14Gb/RRyLO+XZIcSygtcjO7RTxaGdnX/YdNXFxFNsx4wJ4KBy3Z5ASoKVAwbU+aAy0NwcOFHElV1A703ReHF41j44Ses8ZIXETqhN4mI+aYmjm5FQ15aZQ1BGjwJxFUP+65nG0O+mmxD9sDGpjZa/E4cKQ4OTcfZdQWmO3vnmobI/xNGYR6KMEEX2eApdirNbBWxBblx5ZKf6p3mvspR0J1uDojBbjItF1BCA4H0UxOYea+zAU9u183MnyY0KAw5yq6fm2vZ+ESKFetbK2GydJabtr3+zdQwRxU/fC+sXnROZkx7bp5FpjUZQn71knMhH0Txivw4LcbG8ekND//eedU//LvxsJ4SBQePjGjGOHWY+fEPXLaiWcrlg+iH7WthWBFTGopgVMoULDnjv7YG1jZ+W5as3zekhrjuW3k2z8bkrg11DGfwB1MqvRws3jf0wZ1QLIhjv8KhTy08h7LXd41l3/o3m1vqvyipWkEN6BxhVVK4+IxZAyj2vHGAAAEzmGKHa4VZvTNQs9YZRRNNs87q5ZIiWSSeS8j7OF/us8auKbhvDwimX22u9EThQ9LG36Ue3Cga8DqcTlpgobfo5q5AXHa/gFz89gJ9bPSVQ78y5nUEl2C3jE3filt/xxplBmu1KmD6hHEm9Inv3H4xusp5tHxTTIgXkogQYb4He4FzUJRrPkPwk66xJP/lndb29Vgf44HDNm0fMwU1OEKZd+PyAS0L2qbhbssqJn45Bh/SNBmcUCCJdJ21y2i5PbVkfaO5YMr/ayE2NqjMYQjOxtgll6jjISvSvgScgdA7h3uVCyAsxUQYnIcnz3oQeFPdCBIjt1dXdDvo/me7QPiCLZrQIziOlNhkx7xFoCqqLjCZDle8nD0PL82LwwgBSqOju65V8vo0L03LhF3dIZpIz6gaK2xZWwiA47+I6gXhOGYXkXLTRUyhijp7c+bTTbprhf6CRFFO1eGhjxLNjoTgk6PChlrDfZD/cr59UCmKyODs/kb+TxEJxU0iB8l9XcvqN5LrKLIUCoB0rHdGQ81sBBgGiSQXkzbrQHlFpBN1DmRQxF6YSP2Esu81tTh0tiI/eUZv78ZSZz0xL3cO/CY2GkEZ4vS48p5N+8rcmjTUgRU8EqUrdEZvSNryYotn1P5uBqGS+1oedy4j2VnVkRTQcU+LeAdtqSux8mJ+thAYg2kzpIOdC4tI7QQHpurBleoLMNu0c6oq7j04vcZ6AsUwxixVYC9S47T1kBF9LN9Ys2mimKEYTIdD8g44I4VUgm6A/ayi6BWeliBJ4JASlg/6FrAj6A1WaIF6UHsUgEqHEq3hS4+pUu6ufTJq8uJKRkbZMv3Owp8BDvxn7vAisQshpJ8myXQA1k5KlCm6zAPAE48lcc4T97UdIYhiIqpCElKt4yWH9ag7Jwa6rdOiYyoPbK3TaiRcFlhgp2o8j/4/rpwF+c/COb+nnwPLmrEkdd3qRy2tR/Ufip3Mv4lLsAaTOmR+xVvuL8H8PhMAmsY6mNoHRvZ6Hk7i2f0LD07e5QxYYC68hQdgaZ4MNngUpuC66kiPkuFciFv4/99S1seqsnW8bm6LWMynQVlOT7PA2XlD9S9tIhHK/sshSnuPFGcfrVOCj5wWEfLMKzFGN/Wnu4/z6leMmC1osDxvbDaY1h81FgyXqeZo/sIKcW4TSiS/teROsAkCZy8C8wBuzhdi8gAACQ7JrQSbE4Eg3yM8qtY3wfnyisNXMzneH//U7B1I9+xA+bfr8unjhf7nTMA3VaTQFQR5T9iLlqJsgT3xrz+m1e8kItFiRzIPczI9dSQ9UBx+NeJ+rgCSo4+vivKLKvDZt5mKl6ECgsC6XLeqJKqb+0LzDXp1T4k+0Zo6ZQ7WkoK/KmqGwjpKzAhLZOtSC1RGStstgMcWCvgs7PRV2A0uY5fxOAwO5gJJmDwK5anFhw2r1C/xWqc6067r5imaBEkT2Pu7PfG1QMVQzp1AQi7zhicW2Ei17RLxi4vmt+hTs2Wlm2bJXPUC2e6fugN7Zj3qfFmboYUR+g+cC2yFloTyoInyZd/VoTw/Gzw6DB388CSJ0fWOAmCQlmOiO6FDnvx1VI2gFywfUx3gFdQOJPP+yAxHXwK12htzIYbIR6UoEBwe9Z48vC0fYeiE8b1XLghYj55GRkoINcdbJPft7neA5attPlcIIhs/DjnsO4O4U7OWu/GXkGNcswGHcDZQxERfe9oYATv0Njy8xng3E0cAh0DNR3QFlYOYnPoYjEHYyfafPD984NIW+JLWPYBI2aZ4j4CgNe/bDZfGfk67MBgN9NgaYJ7ttzadmFN2AcNy9/jkrLOmMbcwHWB1WEGhNp6vA1NHGv/L4xhFzjwSWyZbvnwS3nDIh+b6O3c7VfS4Al0fCGlBnSVhWp3SEZqZTDdwehx53X62OomEFIWXOP7ZerzxihnKWSSBl5ZDnWgu5QilIWlbp97y+Wylb0H/T5sBwZCgf3DthjG7ADyrEwsxoTd8s+UFRkSj8Xsbe3aFvfShpeeYGlRvoOuY7iNRukUtEB9bexNsL0DN1gehpwcTCLIVC4XCXBj27HBZ3G40KnqXmX7wfdWbEjXBz2kmxPtfGhQcQwY6cf+s/OnfehVVfJcLQsl1XJhpwp+FDmSSIsbH3bUdYHSFOxC6p6E9xIccqRYDS2nrMtU7h5FeZ+AWS+Or/Q2EYbVE0/PAHYh18pJLHmWz4a+U+8JVdYKRTRrCuNW9Zdbp2oJ5IwJ+IFvVs9L8BdoXFoff5D3/k4Mp78mA3/8GjxPhpfmhI/HiOM52zW5KC2mk0mA6rwS6OsovPKLpTU42lBqCWlg3tZx2cT6nMUZe7nwWXdee1cYToPMZsY9SjIOPYCEzjGVeFnD/askA0x84FCF/+u416cYPskVu7nCnbuXa7qdCiZ/sMjwGMOShH/T1IHsg2MRXNuiaSt2xa8+cOO96qvRVUBM+9trmPbAxnw0ylWhCdtjlVKrrH9GpIEOjYr3jRWtCx8DrSFzislr6O/kDxonTdtVLWjHFAAAgsLXZ4czzX9V/VGliXFXukXhfEcdrnJYLMIRdJHwJ22cJ//7qMPuWU+mKXH+4tVi5EqLj2KKuskppzJQJTZFKN3fQNJJiZFPUd1HpDZRTHwJ22/Akwj++SCv1HlZGr7dP7uQiR5TwRoID7LnG8oN7/P4IV7iwPxmAV65MaIAXE1ATJDCJ74QL0tKbns4wFy1nFmOEKdWq5RurISwTyPplgWK13pf4+mcsIQuH8KVXPXqm4N2wbTpEboyKNyfbekaeg/mDwgZvEfnmKHJ7g7x4tSFJbzME7kxOrbkqwvvNa10BvPi0Cg4ueqHDZvgNBIs2vtf1lhLNl77h+lGthyYxTCXL6M6PDJM2quy6LPl5e+ZrqTBmkd1Py2QkoPPKUNf2kEn3a0f/6EVR8gKc+8RI18h5e+yt7DDi0Bg8dRc2w9+ny74nqQdwu+zhnn/9X4vhujc8dLc8Wa87LMVEFhcAOGnYK3HKm0pFyYMzGLn8Xim85P7YfrUlC0a5HusDccQ786MCNx/Q7CWQoWqRHUEOAvuGdqswQIpoYcN8vTsePhDIeo0Xb5lbkIwZ0vXekqY5BM5Gv3chPtY2pRj5ocpyohWjNW7cslxi5pjTvCiFLm71l4QEIc+N/zpI6vOev9WX21yCgN+SiMwN/sn74gS3It44azES552DxTNIi92oVs/9wV3Z5ZjB6VZa4NKvAP4yiJJqO5pYQ7CCRgW71Z+97VEE9v2bfHRFRmQQ6gGnpj7OQuR/FoMibyNiXQyB1on2LJnkmZ+GpvBQzCWDpY3TvaNkm1hYYYNDzLl+8oRuaQrAIO0t+kerWGIKdxFL0J3oDsBE6JmU1XIt73thgABgtLKZuEC3y+7gDbEbtrnsZ1raFr+72gCAQs2wvPa5HOTMpXGmxuOheYP9Dz67x85gzSOwvm0BldCUW8PHmdE+WAMFvFG4GPoEAqzxDDxvCCgiUPkULv+OQEUEcCGfEy52cvyf8NOfi2FQHDI8Cg0JVtzkR3kKXfomZptk7gxH0zW86jVmHI09c4czykg7e+deZl6nKmSgisUBDAhTrsS1M6ZMyfvaPAvnm3ltvnMVTeeyHPLtsgVRHlC9LEtNLLqewSXQ2QERbql1okAsEuZlgfVPae73qSS+rZ5Znf8P7JmQ/Ok6qDIJZL9dJYB5R3jnXNnC5N83DhnSSyHZUUSNhGx2NSImkXJTDfHyQaRYhcnVlnjEBLlYFHWIhn/jYI5h+d/d6fZR1sAC7vBzAf49XD5c7oW0A4qXb5FjZatSB/RJJmiG/J6VEyutQ57A9XAF4YMWM/R1qANzVzT/iZuNubKyGgADTidHgDNtnfImQVYVTqWas3MFYQArQbXDa2OXQ5KPdT6n31o9Q+LS1uXNeBA4Wj3RkcVmOYi5Wrc1Ux40yAS2Bi2v/fMtLOGMvnnrq17DzEg9G5QBcxiUrWSb4NZ2h2FC29pdubSBDwsqiWJJ26RORtTdQ9KhLTaBUipkvHm5cvpI6jnAFUK9Et0kgogeXD1e42NgCrNtNivAxmdm9uUAP9Cxb/16/dvxvMkZKFOorP8hFqzCc3N/dZ5Ik7iwlQxDOqxKklALXT2v9XDIIsCbtfhNCL19NNXWkjfTAQKQzU0Hlwr9sI7SF9keZLUoDEBnJWMVduaK8prOSMYuhK88bNMQmzPm9wmsSwZHV6llUqrmjvCfbW0A8O8t/DAwNit3O3jlzoJkxbKcdnCxTKRf3cdK8m2qlB3oNbGnMyl5FIN6V4qAGqHbI9nceCmrLGQJ2uoySsYQMa2bBPLoXrhmPJ1tLNGoovDlaRsfEZbJoLZeuUO3xbggCP3JIPbIQ+mWAzUyrJHw2h7wXLcniVyQ3ONmLxy3AU4kVwDGVXn/J2AMNm3VuC0M5KxbPotaXRaXyNjOZmWwK02Gsy9BYpjHn8UfPNyGWz2cxR9MHl8zXB8lbXclmyMUGYT9ZaP9TkVxPcNbi4i3+gM0I6IfLVWBMKPn4ygY5rpIVtlMXI/6ssVtn2xr7iSAz+ChTG6ZGYKyDGsRriJIF59LnhuXl71Nt8Rxh5mnsMF7WvT5lCeelulLRWq6wgJS9P3Ap3ONRp/K49j5SwWRQidIjsxKONKFJjgmqp8TCeSV5bTIW57ixxzmXaoE/g9mR9/hTGj5Tv34frOn6mxg2CMYYgJ0rZDRZnWAK3EWG5/Mzzq9SKrThb06mJ6GqPydQeKD7Bm3rLYvovwl9WqGGcKNftRKySLNTUTcR1/lDKh+PYn9sOZw5cACWFMiIL/vZEXp8vNL/8himyMBnt8ltN1c+Vinvzacp18gVrfbCOQDEQOE28UmriZNPd49orvLuiZMgM2XezcyzrQHyL6EPWWIzeya1z5r3hBoD9eU/wsDvysVdec/Il8OIozmkG+VJu3KSlhAcdV0Z0i3Bg476fKImhKPuopydZl5ujcfBV9xpy1CJFpOYE5SjbN5229VDWtnvStAw0wv/iNvGxTsVODnVMORHDCTGbDHyjXWKgiYfZyLVyKUWJkRVrEoZHQkzVJNsqeEa+bEFUBDEHBuBkCEsasCDWv1NTaUlEIXOeJKMH7bmaJi5YmtwDimU8IxuC8FiotYhq19zo6rheMcL9ZQP9Yvv2Gu8+1Id+vlk/wElM+dfC34i+ZYHNbqbfae4GJgCc1hevp5UBiYv5eVBNryLoFRzWXxtAe9zCIM7Q7RFeu8bA+qssVof074zR2yiIe52Ukvmei/2ssXAPClNXfivrDUzd0vY5InO+wSWaoRKYY24he8c7ZuYioixBRb2aje2W//RQ4huCrKb7Es18nLEn//R5xZcoTClySfMxqMWJwAAA2753z78DdfJNC0vpzu9APHRpU5dTAEsdCV0Pg4SWCKC/PiW9vh2o00mIDrme3DxUQUv942shJg5hWOsiIzp9ex5vRlW8bQMo8gvNcyBiS4POIPzEARvsdc23CLg+7HM5rAlqDa49SAZFIQqE4X0FJ2LKN5d0mK8OqyrGTMrSgJtfNLo1Ca2hET08SnoSvgp5UqugF0MkvAW3XBcs5dp29V56wXw5oXt5ILLJFXoi+dEfcT+NYdzegxIZDEHEtxzGNkE20EqoYX5PFZYLu4QKIKHmSxspIeuzGfo9n67R1dO/KFGkcNrQmR4N+lsRnPmbOT9MDqYftgjZc3i5BqGBed4qKShleD3D3D3lIn6S59o2DXG2s03MUhpxRLKlYNrQ6JOsM3U10G9Fg94773cwPO2EzzJWDR/v/ZKhh4q8S8L1EjY7Pgp06zbOFlJIDxcwRsL6dP2B4I6q0X/GxfTgBLCRGRkX0L/933GEAMMBAFWoeF9mU8ywn7V36zpkIKfjufkCea4CUKLvGPernD2Pdbpcn/7i3c5+eiYKfAgmjHGf0UVXWWtyyEYhqt30MI8k9n8VzYEnRQYQrEz7R+w1RPajRJIJPYiL5LMD7X5GwYiZwFUeOrBYVZ/ruEdW4QJGWNdHQ0cBJ078HzUhjZdFEXvvEOkz2dtQLRMkahFlYv3i00soiTIqXmfcJM+bM/+ScyzDs1WE5Itzikhr0M8n+l0ZLmcZFBWzobMnNofGDAQJsQKlN7ycfzZi//UcnVBB9DnJHqBd8YvVr02k/dzct1+2oBOoZWvuHszExVBq6OaCktgEy/SG9DBq4PakcoqP04/+DZD/N5pGLwbTpAZnZyXDstmyOWHxovER2pSDhnZ/B1QBGySmySrTkOg8uAn/uX3C6Kdj9P85pssYJSsxUM/1e0/XWw0KRrgXhkLFcwgQE/b5nNOnRqV4pfVWvMm56vulZx/zO9uVTbuVvDipblVIMncwXyzE+TOvNRFwPpBzShDXQmwBWJ8pnkLsaWFob7MbPlGupHdzPZCr6SMTJ8PK7Ucz5Y2dhSnwX7pHjPVsTpWu27JqwcOH8mUQRKRvXtKMR8GYtBIxiE4+pJoHya44WsW6XvxBrBcw8mxckZsvXPKp4QejP39SBKczpf+N5akIGqiGyDiC6qsJBLMHm5uDw0WaQHH8SOxlQpd3hSUjlm8kkdDuvfJ2f9T0/tyl4WZUvwm5sNQkyIHDJhqWjMw29R9XCpNdIKl4dYEW/VbTcAR3GBzOuooHjqNMiLETGiOWgJHAvwne9m9jInT9wP1CwvaTVvLAAAA3aoBvnNl0VuPfs4VjsQQYpgi2PsBqEDUVlNLLNiLdjZHBtImSmZdHi7Et4/g9mppmrk9x0v7ttp2vHn2cQCvXHOHiojwj75W1leypg5aqQ9PxubqfT7pbllMp+7LMH3mxMm6zmB9VzaE6WmwRYD1yJY886vfcXplJwEO00jOf7cNZ6S7NiS4ljdBCfR/k6z6VjDnqyaQ3mPJNQGp3aHmmFPJJlHqGExPzj9rFvHphyuYh47RS7hxqoH/3E1nTo0y8LoSoIffWaa48cApQkcESTfHrJdGg4i7D8GeKqbJSOhcq0m8ihU2RAI6CNa1tOaPpoZVkeKnOEgy8xJpdiJjMLynkFrhzDBwfHONNDGqvsuPD+VhsY6BrRthUs/4XXVktB318H4eNRi+y/ZeG2wj7rDihIO/ZCsGwrXcxNuBmH8gm+zItVyaWoam4TfMrZ9MQxiTowZXb20Z7HKIO+i2xRnktdG6qyHJsdQYiLXgzjAMgKaUzWApMNdVJZ/Hb+9wZxNsg1ET3UBepf8qs8ssveGZnzNiEE0NI44kFFbLKjSAb8n6sqZ1I4tGcRTeOOx+yd4ZDhU9uuMqgdQvOFaiY8zOUcKBsTyiz3yfK4MZsBhVozQxhyrNFmcXMO5Ua0HGPmo6RmeFkI1Fq8H+6wSeTScF/c/oW+1Jv40Bix2DMe0nE/QcC5TaheaabRpdIYqRCvhGXfkGF4FBesL4A8EJOtuf5qCC7J5OTyZXdUCPC9jPUH+nC7WapHwk+JMFyuLxJoFtyPQvBlc/fkB9zC3eibOBBtq2tDVu/IsCtGY+EXLl4PfGCS/4BdIBTiw7lCjYlheDTL05BQ/t5R5a/dCLuGNAv+r8ukqrzxxyl7/MwdqI/F78p0opkLQAvoazRwJ9zNmSaMCqSsDBSP0xnyJaupZD/hDfSOEDVQ18xkJhzqQ2qdPfKpx+xEfKBKLcfxUM6uYplc9ey9YdX08tyEfc527Nx4W+5B/jlqkrsCUDU2eg+6fIjy27/pzMx8MLeE/3s3t6PcL7ihXqG5HT/ryzurYqQgaqa6b+z8JJEp2Eyrrmc/Gz7qRTe0xZlZdgYVrjp5seHjs3STbalX0kfz8INI6HDwj09OsE2ZHR7HRbbOJlYqmcqZ2NlOg07CNEzHg9pNFwkwBq7605je4QV/khFyEZCRQMF35vmQW1QEqKgNACYPLV3nQvdOvgSCOz7MRCjPJ0wukYjqXuNQgp/sxx7q/c3mo6TFRH+QIrOziujMMJgGAQk7DHZt6/RWq4wfavnR6j7rbijEjOenqqv4pqfGqVpoD6yixBAQiCEc7BEBIGQEGqHlrMi/lsEnL2NnAAWji55eVzSQOyjR4uQYLYRC6vTD2hz1A0sfNGzZkB1JmHiQmE0KJA2YgtiIn7t0wy4jp69ppiVDOfTbsimiD+dlDXkLimLOXOw7DN2d0XCQtjRALNwIJcDJ5uyVmOuCtuiK5GpNUvY3J3rkcyn+JhIPvqGvpRQpmSJ1NzBaXg9VqrIOSZ8RD2X8r48zvh7104RB4Dc0US6WJfaSVQEM94S4WcgPblTqm7x5M5vix/w5nw5F0p9MzWGjGL6izRk1tijpC8w0RJyMTCVUU5CMhBlI6xWlKMAhKkphmhiMts3njUYNM6NSsiYPLco3v4SnNOM/OwDhxTclQ/3E1RsP2XpttGJa7qqV2QukC0aALr0Dvsuj/ySZn12qlOFfBUCmdosiDptQP8pLS5OWrslktiIMR9jLXt+15kUPHtLnFsQV483f7EB0uPVyuBdQA8HmZDgQTznciGQEX3YCb9UE2wG06rGnM5DbaO7nXLNLYKkaGsGudo8Qw1wdE5goZWXDVkq4ioZBjsitwNX896ND66W4nQPF7euv2a18t6q5Td/fwHdb0rcpNDcAugjJBBEXzdfmwv1SvpmhC3dlek2IgrnWJlqckmm/mta67HSpLYsRPvv9NdqNyp7FvSsxz+TwfN0L1LSsqsAfnAI5aU9AH79497BLfDYXsVvDCQlyzvAC8rE6H2xa0OdH95Eq75tGLVi1vNdFOMJsUeIk96iiTcBsPefCw650bdA8C47VDuFm+6ZRd31FiHcqTT4a9WeHuWQA/N57ISRCq4EJrwvL7Bb/kg4QbF5rtpqS3+z5vNZusuphiJDDRgrYHEwJt2kig/L+Qj299j5K4ywTranKjqh/qmZlIEYjUm2GCpMcRqMT9lfetjmR6eZL7oB1RSqVPnjCVDAqpmbEGoYmrHlyFDHX/0Td92U5PqBUwNuQchuMgZ1N27aw8iLxlqKdkp8fdaExkDKmwSzmNQCufeIiXajHo9KMOi2shdrDQWD75JQZgwIn9NEwb6JtUNrWkSE4gXkNo/tTlOYXCMFLJ2XK309d27pGimfkVD/Y0akUbPi1DbMhwkpshmFzF9sdw25RqvZPGhfVmtuU5vNv1dks6wcYDTxT2VhRt5ajbJYqyWX8syOsLWriXKZFcNEleWheDEVYyF+8f0w9MOKhNkZfUMx4X8/r5YWCJsgYxBSP4UET4QuiriHrmOXzD1QH7W2zU9fzRyeY9qEzqhBJknhBYvgNWcopvTCJ3I+R54E/mwryqKg8bL/VNX/A7v4tqtFImUl4aR3P1b5cS8c5wm6oo6cpXCp0huwoLfj/P2P3euAOC0YCQvcEk+zeJEdvlSg6XDStuQARsyjSG6HZV5N37FP4pvvBgAAdF0nOSomHTNQemf/hrhalolXKPb6nPb7slxYPCx9UkvYcYSYlDrcwK7kcxV+TV0AN4QZDG6mtXw8NfuSb/yXXYh9ba3y2BGXsbt0E3n/KmTZjOg0UW41phPELjcvf7zLBRkXiyOEydQLRXFblRNooKNFzW4jo0uftBkxZ6ofBZRNcah8GHj12Oa2gq1ThUETAaF3ZAR9wZVrGuF5h6J5pO42GZKscu8rEFHnYeyNOAfMSxyvjJbJ0kjBH5WgaLTWtaysPnXfqvNeHC0B6ZvAIgnrb6iFftx0qmfsvDtWOYJgmD19gMGsnqzG/8ikcr/VvDEvR1cI6dNdoUgcrlGDG5T9fwIT5sp6E0H61+4cD/EI9jD4kbXOOKod/C8AVUk5E+ExwNnjyfOMSTdRb06fZeHkWjBK7psohBmGtQwyoh5m2z/kR//RfQw9PYlT5iL6VwAWPTDyeBM3Pm1zhRl9JjnYG246IXxWxyW2IADzV3f/zRLGKpECYOB6hVsHgQeN3V+sMFr87/t8HYS8AFCnmyw1lv/9PLxpkNgGR2678Te5gEnXndST6phiBQrt8i+Oite5uWIi/c+zTpR83tZ9mEiwBufXI2/3mzeomIV5vPn7e+A0sFghvGwQapbyLAZzfnf4c9+DHzyUtYOkoN8UFoUgMP3cqKlkwHb2XuGtAGOtO0pWmonh4zzIlm346NpHLPopIsaCMxft5xeRLhnpZP6ZhqkfvkeQei1IgGzIXNNTyVTwV74p3YN7Spr3mF1/kMXGl9ieagWu/lBNs0Trbqt/ytEphefyFDLZUOkfDCUbiaku5qI1xvyLnzUDDuoWJbwef/CpW1k0GcMkAWZ4eR1oatHOd0kqdYjfNS2Vu8Lw+40DflgliSoFVBl0+RfYp4f4cQMB0Cv/flybDuOyPUQPWj1JqC3c9SBpx9nBlkjcxxZ0dcO6gXsliHT7D0IZiDy8FV2/I2SyOzVjz1FB7qpbNwhyFHzt+L26kIT2v42dGQLQR5bk8xImBsgRqyaAaZ05BN54uyBV5kwxVyu+vGkdHX+/IdrUTeToqSdPKBPRZ9tyI0gTPEp7oc+iunt2gZecBqN4yw/AAn5dLecPuBmYpyUCeInajm+C50x3JY21RrFbJ4Jd9MbAnjPabKv61KEw3WVuTG4NuHJJpBAANvcN5DKYKdA/Ei2F3VWZtY7MIFRCMaShO6FDoAUXWDRB+Ej7AbtN2+ZfFNyTk+psrNu55VILtYbLibMnIqe148pkIzqNdt+UbBF59EWvbkZHTk5jJikxlbPhsVfL0G1nocup/AbtWAASppYA9WXn4zUxbnBcB6SKwEaTGsPmSP9j/h+4e0/AAAiT3PXyn44ko5vZCMPTDsazdU5ja/GWig8CJ8PgmLEzKz/QIqPbYDgb+LQrcVvWY5CaPbU7YNka8tCU0WyY43bUm3wemZ+Rac7J7ylre4+6856s/ey9/sKA06tEV4loRsuzmqOb84cxI82nN52E6SGDjoUDBAnihxOnGxKTlQ7/f1RUnTUqMI2aE69zvquRSt4MPOIZnMdjQHI7/fnwmmChQ8VMKDyAr0fHpmQrebb+7Fuu7oHEGxfxegNT1npXC838eRVugecCQnFFDKU2dOa+bt+fv4F9L5dbIQVGtwwHn/xzda7nbidp4Pe2MQ2A+7qJPdyuScrzhpWlrWBK8vrQc4XuWefeglFoET4ySEFbTFYlQyyUAC9lOugXr5BfcNcOO+Zki6PkZO47tKR5o0hFD5mtB/rEqh4ywYo2bTfBNoiri1h7D0wE7agQ+MewbJzlYVHGwd1XSo5MbDi7Z0liD4hkWJJ3+y8K50WLN9X8y/VpaGVkvqAt7QgUzpWLN2mCiPo/0gE+ERRelUF84i27ekEmqcQYRl5EJI7pzvD2EbYe1FxUp0h01bP0zsLF9BnhjTaTU3EjgdMZaD43C7I7USkeG6lFrrppnegkR0+MPx/+nW5ixdPdJ84TSe8R0WQMy2P8LkHmGy/NW3wwygCteqwMjPIYzIYjIF/4E0iZa6xLYtVdHFQPnWi6yBTDM8dFALyab3aPD4F8k87/XW4q2tCKZZU+nAgsjHzrcIBLcqePwEAIZPbb+GkT7s9BWYugR1D4epJniXbCfvu+XGtNiDYphb/ZzgHUTLtFtPmrqYQrf8Zn3/NS9GpZgKup16BKQX8dIhcEWXwo3cm3iFxJXu7/YZ+fm6FUfwIulT6+d3FIwUleNVoXMPaTVsifWdQgHVgJcQBEmaky7KKlY2K2BJlGVXMrsMKu3gSiHpWdYamrIlL2wJD9tWY3BTtQVrTmrtx18M5rJIfmKzvPRIqqsAimSXykajMlj+odnQaM9dLpLFBX9fifFKpK4KQneoFTAcDvLyRwxVFHbBcuAEpZh1jYMCkwhs44nybkdrXIpfmF713WalPbXsaz3QfNbzHnm7eBBQdIM2WLV3sci2+Ekq0HaqEl6IFIxTxmgFjGMcxmIk+a06NNMFbB5U/6UKC8Ag4jo+XXPaG+M+P/CC4wCbgBCA3HYtqf5gfQkuvqk4BcDjaFTb2cxyZcHi17qW/7cCG9813NAUPxFBqC2Q/vStuJMLT+F+JPq29rTr0IuvhrkQME8E/6mxHHM6wA2huaJ2SQ37d7AhhYio0YkyBQpLbX/Tl+MgV4t2xOw4eD+Zv7suCbB+a2aYAAEWpa4GvXfhuEqCMs4uqL8mwhnF63jkL/gm9urkCVNMMDIYCnrN1ROGmD3VvfHGomdH/Y0626NqB/iLRoppfuyIrh040KBmuRMPr7PDxrTdNVlYM7jM9TkYmI8RgsE3q0jByccg6lIAhB5P1eIkXg+NwFj4OrJDwZEMf7UP8ag7i/vu5dbVdRayio0TfZwupG1AmP5zGr4345TQLQZdvqfg5doJdbSkleO2qCQ/ZyGCTuIhYCz7thd9HACIMd+yRnK9QLYi8nAq+eSMQo/fvZ2d1wPsN0gZZyKYbqfgWD3IJbJ54/iuO9jk1gFKAdIRiU8246tZ2/8mvuDAGfcBvFQ7ApvswycZnhBbU/mERqZQB+kNeYfx73+Zx+Vn/BdaXn2p8+uC4PLh8+e0mxpX5hDaKWU4izaCDijDhPWn0q4xr/nZRWmhVDqCoYqHxYs5wialgxwiovH/g57EFttSQl5+Y6mK1KDa34Mmt4CAjmxeSmcoARf1oAC+ivLgH6SWt/Ha4Rwgzn50HB8a7sElun9dMryWO3HjPTPKYJ1x0eXazH8oEULn8xxejkAwjVs45Mj4RmoawJFVBChCeMqMI6z7efOhcd7LNc2QKiqxLfmaf/Zj9PRdeFXhuzKqrTi3gacWonZx7VoNsF5cgO8zzZ2KYqccMCwiGbEIKfRfRVYmAxDmsMYiwAPvSKJcHiIhNRlFZoYhOkyZ7+slltWehdXATuWvxw54BxLWLtznhINKJseKFLZ1vt7NMWfwEL9XMxDX3G2ZCdwS74kqSejpkf4eZbUdBXxw7x9jq419iYzAr2jA2ozh4OrZI+3yNNTl47q4aBBCoBEah7gD6Mphp8OwMpMmLOZYyC4vmrOd7747qee/k/EHvxDCBajAlMz4RWtIFXFqUofg4PfBtAwR9D4hKOgYJ0Zw1ZJRLaA1Dnh5fXDV9GrIzGA3G0RMoutlkNVvD9k7dJVllMxKQGaC5HoajZ5238/tvIOq9DK0x6ypVC0ysRqy4cT6kl372abyfFFVKyzCvBwC6y4gHrFjiBp+DXnrJW3QtNABRFghm8qlX+TSGqfkAGJlXtanGx/ktQDkvPc/eGJh7uO5eL9gz0w3iyV/ITzy+Xi6kgyVwazhY1KAmWLufS7oe7xVwP5WbpYM3KEeGAfmufcHzqCMsW0Y1ErkBWBNHtrFekyaygSFRoHdq93Uud/kAwNqi9RdQ/AKuZIIFv8i/Q9F2YXjxJTGEh3g1ydgdqMQT2Ab5FdsvMTYJE5dYgjp15bCbmxdadYvWB5odUHu7zjbYbqkt5IUaTT6rKgqlDAGRvzoWGZ5I7WvynWTMND5TWt34AKyp8DIiPnV65McszAFm+M2DmneCuAwHGmNVYjy6FqIAHwsKVhlL5VtkwqWnPsRAZ4loAR0O1EwQItPA49+fSLEbQynfsMrl3DwWNq9p+cAdCv2evdoZ8kFlfYNWEOfBTSPIqFADaon3JWpdr8Lijy76Ns7RahMoNHgLOj5FA8DYBvdlvB14sPnjAe5wsnVBZjJu7Bf0nqY66g1BdZvaErcFD/O40TMUxn4NNzoF+fs1l9sVgufa632bcsdKTeJhSXFDMTKg0Yr4fLDKZeBiYWnxoikbkT3LCEWI1FOYue7FGxJSRArPDpULPn25plAQEv1PSpnhR7NUofjHakN11y2s0xafxJ5p2TkrO/UpU9Pdm80yRWGfBKpcTw1t5hlQLpDr2W7uAd1LFzi9RET/p2g+379LL+gxWiQFXaiHKbaGYMGE6Eg8LCfX+pIbqekIpBC0f9J2rHSwVFIReQMOv26+8oaPYAfvSQylnnC6q8bcqxZxIcClMihgc4MZITWszl2BDdQ6l2c44PfIV7KoptY2ShxvCMhD3YyDZj0oAz3x+SfNjjye/XQ4SlAiFCGeGZg70Zicf7OhvQxRsnrfCw8a0Cr0JBPqRSnEAACd61ssJXoHSOX30LXz+Un1seVvWjQ23xwAdwsDK9ntUjNUD3bxENq4I5IvhIva096DVICgFP5cMVKCn8+eHx4QnZULmVnKwjOOmuPWVQhKIoNf9P77LlKReqrSZvlYYvqri6iCLoazOtGg4wVIxRm43fqIoRMQUW4khav46Wroe9HcoIXPffetGRYwr4UOXEBtE5m5ycGtHEYXxNiJJBu3D3ZOCI5tYj7adySeo3uCOcuK9UlJQkuy+lMUfJXm18paLMWY33zMSv7Bw1JyHJUs7Bb4WE3dDnCkDBlvgql5aCh5qYVYqKloE0Nf1A6LE14B/JO3G1zyVS3GXhaTOIICW2K+PqQQolIFhCpy86rbu9TSf/2KC632fPoIhism6kZr7uU0G7hYylewznhk56Q+OrCBYo75CQJ48o++sv4L2y2dAaxSylemCHMsiD8aGK+PCtqQuDaNdb3crSWfJpsO8BKZC0i+M5t/6rmZv8hMdUSwEGQ/LAVdsPZrPJ/f/NgrMkepZahjS+DKKQVhDk8Y/uokkxQc76g9SyG+m/O2MdVwMZkleSSYE2q5ivizIOCqUk7g5UK3L854UhZ+y/ghswuRzwDP9DJgee3GBwyEII46gVV0J+g5V3icUxaKP2QBuTiUvBydYSwUHM8v4ISCklqqh0iP9bc91pVnrPZ+DVW+69Y6Mco/Cy2q55cPSkOmTU64lc7aCChKuiu9zmeWW9ptM5IPqbbcaK5cEVwRzQRBc+ym8XEk8AZBee5ahXMNqLhcwhTIShHQafsUKHprN216TRu5XqiGVXqVAti5MlqwX45Wr5j5iSpRQqgrzooVPop7y1tRy554fmOleYU1QtzlfNezRRwvdCh6oGgQGQEOwBmLnGfnFWEUcCGQ8iODSQI6R0zj71M3T5jpMX4HDrvXdcmYB5FZnRJhdJiuSUQHXE7P1keJqQg+/JYhuMii2z+wo17VzkASoEtWP67DfW3oEksGYEhHdvgR7m9cG3cE/zUJcw7XSf0v1VLMZsaMBiHGTEbgh8OI5QjiTPoc+FOolp8i5HMeajAcUwpDJLw71UcADyrBvC/Asplv8RdFe8YWhkvxmYPYiwHox1NAGsY1D9VF7EONgwopGRBQLjQZ7xfLHEvuDilKA8KboDktybVYVXJxcXKwIfiIKVf39yMeJ0d2BFWP6+8aEI1Tv8m1U9CwB6whIzcv5g/HOjJf3Rjh8qbE8j77gmQxS8D1PfwPKJtGaC0t67AR11egMQF9qS2kzqB/RqiQJdIBiNTZkmbEGQIlCtGaHMN0qrmEOKozJk0pwMFsqSdO73cseUlO8gAfBZcIZxWEzMxMh4s5abZpIeV6Z73DzrL+cxRWFVSoa3SBZIzkaklcMeQFF1YwzYOfWnf+qQ6SRWXK+4bQQletFFWFt5VFT5jmhMWkwQU3LuSWDfOVXFTrWzFInpEN4lDCvOFMpzqz/172GIvpwU9taCEtaJaQbVh7c3nYOp7gNfk+y8naKjSM7sV6Bj/Gu5AomCyAHbRur6bt/Vs8UI4gpFqVt9bUD1p7taXMrdtjNSprHcu+XY4PO2QX3lDJGCBC/3z/mIdyFrUP1hbbL+v6EXfBu5Q9SnzHsUbQu8Kd3L3uV3fGPhiMMrnKd3/7pUzaDEPILBNXjOXpCJqGfuvHqzyEH54sGK7B8Py83WeyS4d16QKviWbPUXbWgiEzg58JO+ygRkN904XuTNeHlrG29BOzLCQwUZXTSWWOcI9FiL81nVbpuH68IltK+VMW97pcREboQz367ep8WQicWSRTMufQ1ZYFeUY9SXQZKAWihRkVx1kTFrxqiMT+arg7gj6LZ3tpw9vQ7PZxqMeTXqnxJjF4hhMkLcFRV4pBIvaElMvZ/M0hPGbsK/aoMrY1XtH6mJidCpzY094GiCMxoZ/D4WmBX1wYgj+2lzCAQQ1yWZQXWhqZt6ph/mOskR1aLl8RO+Dc6T8U8uuFFgmHvpVHnPBbSj2z06x/5q4Bco9AFwea3F7pNZZF/Idhqlsh2G9zCTFZ7BOdlTvSpFU3FPIGkZSciG9UNibB//c+/9gBqwi/E3C5KKXVe7PnhqnCuJ0jo1eGLImq9n5PrKsyP+O2s5jWWAGrdRuW2DIo1OcD9x99KYGs40QjqQEgKmE9pD07lWPOnt0nkRHvw/pzqVlB+7Bl74t37ROcQ/OWtfb6s3uYWQiqdZU9rtv0i4pAdxX8YOF7sFnTW0zoPSLt8jCDFIj1Lw+i60Xv3Ky3zJKJBIIfPM7nMdoa/fKzQ0YUGx7MWS+zX9eUgsuLPUT46OT7ur3I5Yb90igXvjg47ZvAG9hJ75x9KojMEMcgyoO1Y096ySegTuI4roFfMvHjl2eMeUOz1ICc+ytiAu6Xzl9GBq71n2AqYtD/YkAA5vKeOQtOIV1Im/t48rfvroFDnFe6sKjUPfpS37+W6xw91PMknpc4GkRYmdvwti5EDElhBkhlv8dfkb6N6rkUxRVwGN+eNz2SQbdrj61jx74Umrw1T7TEYLNciX+f5uKFgMSz7OVBUS16R0/xQLFtir5Z6+YtbhA4fC4U6fGb5H0Lxw7XoEwaYOJMiNpNUPrR/uW7LFrx/THD7uPD+FBQ/g43ECH6zwUKqIFeGB+o7wF+cI/taW12QhSJdJgvG54oUBFU8iDqVy6+gZRRkeMCji8IefO6taXs1jB7yzZGcwFTMY5nmdjVbQPfY5su4LpbtxtEHU4ED3qcYFbjf30BqoL7GvbU36Mpzy+RWvEfbk6LZRPpV+UWD/+1gR87+TS7scs22RfNKfsXpeiFoOpFHBQN6x5okkxX4Zhdim1N1s5ljl2dTNC8nRlgzVaPULxxeqFS1EPdjGeVE4cVr7TWC0LpyqwYAxGYSaSKQESHN8isIJOqxoJgFfUU5zkg2kJwQzW2A9NKBnua2SV826/8lZnEZFkRaA7WDF5BvQdOwK3ONNFYkr2I8nLki1gD1fOQDKL6WWo34NbsaycAG1ViXT0HczdoajCkyRlZmjpD7w9ZmUDrZpcvRxtzRocZvNS9zYTnPr27BIihImZANb9RMCffRYceklfFJyStuZyDxKg05nNf/O4iqDdFedTrh8Ur6PSfO5/RGpJc+Iy4yYZOIDV4ksXQzMvVWRKrKmr67jHLo6fueZCtsrQSNkyrWtsW70mIQ7O5AgZkLZMLAxncdQmsAiB450Aa8QxA1re6fYZoxSrB5djSqbmL8MgEnJvx4huP99DisYkuLNZB0yj3PpM4HsLGD91oFWMWlzM3MOPAJRujI1KX/C/QMEFJ2tGWhbkWj6BgrA+774y+zVr3wIDVKcRux/8zM/BRRlK2penkODwwjjCYa38YGwewIkmu/1t9LKHoe9BcCjgYLFefTMFowRfxAfY0/umngEKLe5uuqvPUO9HC5/uumQXUxPS45aN9GmS8QU8xjYra5DraYklcuzEbZy9+c5k/ly3sYbkpl2vTGNt31ktwta5YlHR+RQJyLCxGoktGPEeVwEZOcF5AT/GAVSDxHxOvJXFV3QLM3nqwwNi9snJznlWuEbEW9nCFOZsEPCxIAVqIFFFIe+gvaaSckUKaY890EZmjyV7u9dbg0b5ZeIv0P8UEUGWiKW2M2DeVZGmLi24kBehKOnBhBC3a/XgKFF0dMVb7HhKZM7voq7ukw4VO+zmYyShY/Sr4PWpUZppA+odQfi1PZNU3/s0srcx6XUq5LtNsVpz93XWulxbDThwSotc+D6jNWUbQOCzkoAM5EtpX9PKHkS6cxQSSGox3Go9xB/QBPgFV5uTIxCzfNO+mwUjQwbH0lPqNskNj+aMqTEYqGcHSxFN0G/AN9k05XMDtlLCr6ViKHPUOJIq5wfHv/di6ANFeeGCj55WPIv8tiN42AgXg0vJV41dpdBQL2h7+H4TU+9E7n5J0Kw7HeLs2tESG3fVLY4OaYyNZ+JM7BAhJqViNyiE1LdYuAI8FC/nhOZoXmSlBWphicAjwjIi6Lgvk+YppIWmTicYNbKy++CFBA3Kx2qVXgHbCLSqP4OMhbsjeRyftjw291LeuNlHgaYTVLaMvvLmyX+LKyFsBULLP15Qa6BDeVmZvLnM6wgnzkbRU4u8lljWeapScqRljSMHVAalUtDQzLFHlQoDpi2DorP99Fi3oocJVriWCQipCr+Rex0Ayl1H8Oxt1XmR2HK9gaycT1AWDNN6ohaDIPwWvgtErG1TdNERDGhvuxGGjjAViOiQsyS/rk9dFro2Jmb+5loyCZ4EL2vnohSDJQaLMAmqplQEOxprP8DBGMI2b1L2rxbyq78K7dobRhCMuedeE8AiGUU0r0Uuaxj1oEqrgJ0g0x1lKJlgHEKIoDZY+54phiqaj+LNf9szPr8RE0L+rJd3ucNC+r4nE8pSe7usUgvOc/FPo0E9Fn9jAwTl517FudvxCHsrokdd2vGUxifJS8tQUaMKHhH+095A8JfK8+po544KWTLbL4q/7VIa9B65Wrjmxojq5L0ts10vAGS20s1o+gb9E2airwQ+UGcn3Cqw0AQLKtfMBo69Zd+y8BBVUvOpIwER6VZ2UtBhKh0VlEyTbqwAsC91DwX8ieX+MGSgKQqeR+I+lq2gThIVJ3oOx6Y6XNLacafEKMPSeFm3vs2zPO3NuB4ARzu1KsnXOwoRVWS52SV3Ec9beWMTh5XkzSLBHqkRiPz808Nhj9UGIlmaAXmVfJ3bMdUr6/GRTZTLxEHSXEHdigOXsODKPrRzUna8geH6p8WhocD6fzYLS4P27kg6PgvCK/e24raIAN+XK64qNhiK8UvQmDFPGAYGL+mYyjMrbU65YWGohFe7YFoSFDyUh79NONvbpI2gvAiYiJ2PsofJwyy1tCHXVAsxOhyKnpbq4OKFixDIKFVZVTxYEfF93gVcRL8m7eS1R5oeU1O4meTNfyQ2U6avmdVlp+U0ADExq8vw+z+JfoHIVk93eiw/J67BCjGdkZdy57sCUIR0TpwlBomFbQ//p3lkbnkX9795yq6+iVJpfnlUYf+Gk9/xZaXwwSL/yr4t2EE2HH1wCQaX8OAOqgAiWCihGR6UozeDZcbBVIoA9FQsk9uTEoqp0KXDudWvM0XJJmbvwjya6yIDGJv9l7nlLJ7wJh6ckk8PmXmpKq/XPZxJG0ctLvyiLZRiPPSj3vMVjtVQw+9FAnKqHdS8WJzRggK/ASeIiD2HtYEgtfXfEg41H1bXxz5sb5Mj8S3huZIyq6SutXKxpERKTRKKInhY2Vm6LYXUCLcXXbRWdz9oRxzz9oNYGRkvEgxMZGeUo3IpCi5AM5OWnHFq9PM7A6drxkUWV1WwqPkc9v+81bcFtLkBlk7bQrUVnjOZEV2b8pf4g7l6bwiU2y7yF6yNA1ZmfSyVrSL+jTH5OeBcjO5pO25epd9SzAhge+K8u5ukDuT/u8BxbbWQsCQjnV+mQG4EZ9i/eMYcL/i9z3B0oPIas3hwZp3mgRDNSFnFRQAEeXxftR6O6C7dEe8c8OXCnXZ2pGDnYgcXw9lEIA7uRfOvqCsRors06RM3E/0g2dy+yVNLllfRJB0aUfkTFIbGM5rtPuWGGHB2yqyd4WpFTaHnC0OovmQx/QJkoExi3ZhZXtNW3bD8sa7qCkQzxH56KrhXSbwlFKUou4tVqUVV5rqVDEqw3a/wDFK/KGUZMDh2/5ehD1xRd8rf2E4EQ+nVm47RYmPgjxRQwx0NNnmrkbZbClzojngX54cH8mAJDsGfo6wihE1m3iklko+GXPbzly+KoI7WgdA/iZjhk6KbmP/ktAxnv+2VlmKh3xRNyRCf1gCfmAbGq1Yt6W5fhauwIG96CrKdGcR/fT7Zmtbt0innD2KetjVzE+PclffrHUN+VGIDVNYq8mDVGqGS8Rtj3hspfBKMXBMQNy72PtGY1mQts0FXEEbcLodmlMwrmz+rYF8qc8il7/GSNopH/8E1hAQVO6rZqpE/WfqzzxHRi4+fWy5aA8a2rgJ/SAAEaJYD/WCYoRWDc3WpOZmwM7Pi5tl2dBLE5J/8xZkJQyHT2URa8mOlo7mCIRyq5cnsBQY47/rQoIlCHsG1rLetaM4wgCHYCERay7NS7bCKcDCR7CK5Yx00/IHFyq2fL8+5x6ly0VnSsyJIVnU+q/3zxXeVdHN23+QOBdnDJXroquWq7XOM0KI8IcxXhNShoPQfUm3fksAdYqA1o7seFWEElt6gdc6kuGT/nubmoV9GQXQnaf4u05hgc3TOcVjnhc6tUDQBd2Iko05M5F/lSJkWc+5D0B6FNKBHuKwhLpHTzKwASOdu7A62jKGd0zcHA9VZeCJ0d+x9Krf9WN8niL4s+xMUqehhmIHvlYGcWXTXSwCtw0yWgcaYW7OaYsKIw3sMHhKabFQY+NEKDn+2RlYvQVofgL9hnP+v3qoo9M1wYtGo1oBW+NCUPDnMEKtAAeDggQhpoSuttmhy3ft59oGtO6LfhQ1PdawgQCw/8ucVfb3UT7DB4mx1QVcWIngB2UNOGTqrFNnF7jy3nxJLKK1VdpUO8c0Nk3+iNQ6HJY44+xsCjcJlp4kLUCSOttJtmqtVP+/tioEqQQGn+W67y2IJ/RxMkyevb6D91L/B3g8CwIgHGxAunOMNJXwbtixdDhYtWyjdS3n3JcmKykPOO0N65V/cbZmjz1ZbBOWHf0MuUnRS4SKjq9TG4bTh/4LVq/3fyK9HHvVuH32XQrPBX4qli0xq7ZC05bNpKdBmlZz0VDSncndvXuWfJ9OujIDuD33VXIp5AHjvMUswmqx54zDQw4vKDH+14tOz81mNbGNgAAHTeWScC2c2L6qEH7dZRx2d/2EmmV/LtJKmM5DLegJDsLNl+6G10OMh5Lbi8FA7t0KXVU65ESajULKEMfFQqE+W3snWkl+FVhDIevxDiobGbF/anab4L9DRwWdDT+M8km45u6Ph703GbYmJIfM/4IROtKXFWZUxeNnKvbUmKWqCa8mWMjT+gK9pP6lV9Fdqa6uOebDLnjXYUK6l2G0CoA2Dfx10nQaja2pinHBwyXJItLXZn3qWGf8x0j2xLqRM2+O/X5oidQ/xHzOS6Xv4N7pXfRs89L7hAduBtApPtFB6bfHAPMOc3Lgqr6sJUZsUcw5md2EbTyjn+8LzqVGaTZk3Ds9qOCz7vyRufxuUkWzv614PuLFLYVnvqH5bSEEAnSI4EEgmQU9q4PowZCyVLmZ+pu35/00r3gCUShpgLG3DJhyqd8k3zDQB2ieYUfB9ey0eRcvzQHW3PbnKlvVF0bwHAvUbABmxCPxEaFaRd1v1vVC1rvG0LSABXzcV7Axaka5PbQA3BlN0jApraDUTZMWHtzIKrrucUm0l8Z/PWlE39qNFKdSO4qWeSikeKdy5DEfJxzbNAJ6BDANlR15VYPhOeQ4sGg8RFTnjQ9HvlZH9lVun1yKB9WdmnSWxBNyS5I3YpZ97d8WrhXgjXBrOfrvksPR5Kl4GO+ZlotQngN19Ocjy7e8ajjrpiQVOJ+YLoJj0mCzdlZXtAACpcXuq2x46EBK/jHTyD9QmfekSojdLUOGs9ULVja2Z2aQyeLRNE6lXckRsUhuBrnsBtYUjvkhWoHA/rUZqhQx5X1uXuewvJ8yMCkAyMR2d69GB72lQzN8PKX/dnX62sW88S9/vtIDZxeXEdbHKL0tYiPMkElsrhsacQH4elDLJTxK//nFEBNmG0K9I7Wy2z0cIAdxGVsyoldP+PkSZdx8OT5mqprtuL53R7CGcKXosOoxSDEWaEX9uMcqpN6b3cJaKu6EeOmn+RUttDadnZdbe2qzu1kw7Nfi/gpJZn5IIaFLqQYnFK0MTRStqTD//vT7EyV7SLgQ5BXGM5j9EqwapgEfj0nZHqKuucnm9zqSgTyT3Jti+1S2XR/TY9k2vH0AOB2YH/2QF8pIaz0wYDk2E55jl1nYRdQ7G9kP+9WxEJnuGavfBqu6claQsH2d7Be58zEGPW2xGa5a/Ood2MYi7uf2r70FFztv0s20xM/zlBDt/3/Fgsb6HKDLRGiVfPxOjF1iiUqbbR1C9cThb6A+iGw4R3UOfUzOFb3DsLHyAYfULQpNsGkZbSLWj73/V8K07NGy8/LmNI5eDI5yOtMlAt89nFWrQIkmnGRN7HawaPE07uKnBJbcBhZ7yangyZSm3gwERxx2Z8hhZrSjGtntoQSMiExTH6LuQNRAmeTVwkSHd+MqVzcn1IqjpChC7L87/0sNK3EJEAAAAAqgAcKwcgqmRV1tzymyXm+jiXCtnguTftWSGseNYW7XSVAvqbxNEQK2Tae2V1vsrhI16Vzcrtn2LzfnFbdE2VDUa873K4xUMcIb2KNSU9jc9sN+gox7P12sFfEtz0aQRgXa92jCARU4PeGOCMaAxpISdTY7XjCCZgswe43/xLk3x0EyHzawr/dWiXGDiWtG2Ef4zg6zUxF/agJQhaegUphANQo41rip8PLcpDijqp3vien1u8kWKcCrhlaU1q4aY8pDd7RA5CG57q+vgRMXz6t102iQ11W8CAgzMn6md2tBuW91j3LO2LE2JYxk/MGoJj7wmWy7ZvsRmEkBVd/y1WvN/ATc8k/vnyw2w/lEcas9EnAzZqBE6ZG6bqFDkvoKu0z/WbGNS9MmrdFqDmc/Xn+d0Ez6ros55zgT6zSfY6/9U+f/n6OvuaqNHZOwRGVHrDQH4RQGBZ/2N6oPZCOFbshymfYtQq3tVXQEHYHDd+RRpbG/OlPW54lMY+MXHkI0uVlo6Xzstt/da91fhC4nYJWqA2QpDe3oKxqIkHu4gKPaFBrODnDsER/gbawaduly3INpQkgl2bfpWrGxokt3aDAmPSpJGEMo2t2fVEfHXBR0+Rm87yQHEdjkBNlVaDNLGwAAAXADAAFvaseG+ykxiQdaz0d/5Epjvxe7PGaHS2nQAyGxodi1Ed9Bfebt7wxAezHlEeYeiTzYVEfcJ4diTGov5MlZVq4j9A7wYLE88Qf+NbwW+TyYrvKDVAx7e3XKqTOiIt1AIuh5XJN6FejUv7t8Pa/oN88xRQO1rb3sg8Ie0YJWo7SxCHSyfh1otMTljLs0IrQbf38k4p59ICSwQz6dovmDsiAn3TySMx6Q/cbSsqgFKFwsJIDWjYOvAavvf/JLxuRqAiedOpREU9Ss1iRL+f6tysuGNwLyTu+wdUQbsQHSG8zzQ9In5ZHUcuO6iKamsdrBAYnfeL+6r79422rP0EBbJazWk48r7oocztYUX2W1OclbfcDs5COAxv6XuuUkZpvlm+1uTps9LwyW0RJm6O4kjOzcqYeX89dxdG6IufZqgpBHz8VlNdhVjnUMXfqDaC+1juGgwcCCYcr4DDDR0DROqIcoqN1heEXKlMnrgHoZ5YcNP31llaCD0VhAMKUbz3K5d8SC2wi/9rp+tVI8nE1hHVz5Yn3gbsh8a1/06zheAcT0gbt6NWz80TG+uwE4ANtXHRmmoBb+astuZZUhWv+VPHMLqUI1PSjTYs3mggjMeepgAAAAlmtj4r1K6ZLLJV7gMWBpCJ8AAz7S27dnBARDz5m9Wu4lN2ijnzmFZDOxuP9tZ2LniTu8Gb4pxNJXrw0KhqstsD/UCWE9WjaGKVOBwBj7sXDiumYtCZV5uSdirqsKX5ZLS9w3PDeUtmlzFU/Dba3QPLejnZ0TUN5g4VmsPWY36Ad2UZUfVUHOAS8lEjF2eSKNckNDLWRdhxE7lzu0kiMIDyEc2y3By2DWB0MK65WpoOAMFkpICq9XSDNlP8GroByK684u8z3CqsMJYWlTo0Nhx1Mndag72mtn1A3oY9ph8U2JJaxp9m1WbJrJ2CuZW2wrT/ri1NHHILjAHOyqYmRe3Bw3aUfHoT8kFb8p5TEybh6tT5KkA8tI9VhMIqwQtJR9/HtAhu1yzczqmYvPTwKq+CcNnJryk9g7xbvo6jRy+n8YN78tHw36PSYVc+iqPRAo6ZZvx3qUNu7p3O1eq0OghzvHTaTI1zEuP6k/9c5fyWohVYHZPNnr9ifCMU8zx4NnotY9sZD8V654zdIqYyj+UlVUcAEnAAAAJsHtBMehLbfEOUO6CMnG5DwyPWqUbXNhiC4lZlhV8CrCRYwKu06Xf0VU1AKRbAHQS/IFbeWfKdbq3a8UYfCbeoos11HkbVF0eKHqM/+dNhGZf7Rsn7CPnY4gpUynFrk3t8E0+KpEpVhz1bd/xtiNpwKyEcFnxnU1jmYkEgR5S65Cg5ltzeyaEyaFkInRZlntkL9fiHKpsqHNu08KVFXalIfyqAVakS1QrwF5CBAjKyw+obTpONm/sFxikRWUwdONv7Iop1XmCol9fA38CuntQq5rrRBAz2fvXywbLO2WUhN1qxLD7IYTzpvTQb5Yp88AmKvYTnXpzNampTNI3Nz075x2Go4YT0PURL9oJx9W0Xkec6mGIwWIR80icyjPzAp+iRRa2Jy5Jg/evGsAf9wyBz1WanBrHtzzjpZ6XlNS7JEVf7xyW99kQ/Bh3zuZ3hTtdtF0M7n3X4deidlqwsPmaUxfU16hfdrMHl8kXvDXR0ew4FQHocPb3YCuAAAAAEZ69IcQvEjzdAVXEyk1TsZ/ezWP4FY2nezgF1og450Or1uml7f0Qw0HzMQfP3pr58LywBvI7drK3OtdlDa+cYOh94PgBEcOzVGo9WejBe07aPG1bRc1otXG/p3jrwdZDbVeqzVWo95HTF/4G73C6y1/RACdakgjdUcauHhcZjr6rj3FyQRM44wVEJMZVoezCZtCIvZ2AsKsR6oNjKMBBHvnSifVaao0Rg3eYpIUK/SVbNBpZKoRTmaWSdThgOQ+JM894QEyt7XK3sc7OiwAISa2MlPwG/nyUrTfXNmja4j/Pz1iw+t+8+yci7yK8OafURO9yNOCwIPJ/SywPmvqHQAoX8yhJCMLXd8521Llmgm/c+RZARDN+40gGzMZTJ9v6GxHGOk9EyY+SczoJgBi2sAHADUW3AAAAARK5JGQsHr+M1Llk01UmGYhQ2fJl1Y/UwY7ZCSqAJjlns46k4eEX4+jXIapZ+2a4fg89xQMoKKRVxxiwKJHLzw4TVkNWoWlPjiVv0QEejvrZ5J9NbPgzbf+ej0R5xvR3E2K+9LCNdl404YRNEGB93Ln35c7i5LYMReuZVzfio82ceilpbaYyJwnr0igvUBaNDJtsOvohWEg+ZKSRRcBbzGUJvsjJ0UQj0tOaXAQvRudYRht17nonH/s4e2I17ohAKxGdIH2Kkj4JwvV2LRJ6pyNX68jTmHSqV7/nSNlkBIge/Y/cYTC9N0aiXmNIi56i60tMX33xfBmA1YCyywlkGSxDSTadwthDF5AcMzHfrs2g0g9IQQSoC12B4atrnnMlEo2PvOyOqDleulKGosFYJkhsSwAAAAAAU8E+CBdpPFOM1fifM0K5r9+7BHHeB3m3fYkihsqLxovGelFz7uQ0PhxrvJuRtGifcNkq0EYHIxGPFzZ5SHDE02Zlp3FKOrhvahCfjU0QF6xqdg8wwPSsUmz47So5eO9pSb7MG4p3VDRpGPg2ZrzK1XSgxpOy3qT4IObNH0cP6MtnddiDnS7ofuC0QBr4ji31ZXlpqQmTXdVNVF3nlkkY+ZoUpJmfCgFR+pB9wUQOCKp8xx4OXo9xTINkpM1rsO4EjTxeuYLaVczbA+tta/JNKunvS0uEDWS+t3J12fn4F4XK4njweitWovYEOAW4KTz/v+UzQvs2wqLNWw6dhuT1LURqtXnluVOyh2I/8wXOxEjEV98S1R1oAAAAAAACrf5Ixahgmq6kWkihPRHN4U/jQc8HGyZwK8X4Tvfy3PoBzn1WgRZ5iuRc/0s5N5kTpXp6zniT9uhzrohmANBImv/XYsFNfMu60iyXkRt196Wx/+U1YHTE6+fyGdyMYC1lw66vKyPUkoD0p+SQNFW18jG7t6hiG+ylkbeuadTRjGxd9mTKNdufXixkGQ84nxt2yIB6PWwNh4sPDvR87ip7c+ApihcivWxkWUVy+bs57/340SUKS8DXjlEq9BP4m70mrEOaF/8eo1vLx3uSBwAAAAAAH6wTath1FJBVitylkSIjrlaCjT9ZVjOenOlvzm8jqbgsSQIgd5F8WnPLom3BrKhftNOeWy5IHxt76cvj6gvDf/PP18sxQ7nKzNmXJKpVrC5ByqYQFx7+LQToJNn1XgwqZznit/YaiV4Iu+daRc3aZRFaQufr+xk8FeFSd0os5KIsD9/AAAAAAAFAwcu70MyOl5p7w6jK4LNZdCsjf6YKxKfM9snyydNhUc60AAAAAAAAAjbolhwBKZaYAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABFWElGugAAAEV4aWYAAElJKgAIAAAABgASAQMAAQAAAAEAAAAaAQUAAQAAAFYAAAAbAQUAAQAAAF4AAAAoAQMAAQAAAAIAAAATAgMAAQAAAAEAAABphwQAAQAAAGYAAAAAAAAAOGMAAOgDAAA4YwAA6AMAAAYAAJAHAAQAAAAwMjEwAZEHAAQAAAABAgMAAKAHAAQAAAAwMTAwAaADAAEAAAD//wAAAqAEAAEAAAAIBwAAA6AEAAEAAAAIBwAAAAAAAA==\r\n", "BMW Cap", 100, 4 },
                    { 2, "https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/61CDLRzp3-L._UX679_.jpg", "Trucker cap with M logo", "https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/61tzcdhSk8L._UX679_.jpg", "BMW Motorsport Cap", 100, 4 },
                    { 3, "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/1", "Formula 1 style T-shirt. 100% cotton", "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/2", "BMW Michelin Crew shirt Red", 350, 1 },
                    { 4, "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/1", "Polo T-Shirt with MPower logo. 100% cotton", "https://i.etsystatic.com/27989904/r/il/00d1db/3891512070/il_794xN.3891512070_gtkc.jpg", "BMW M Power Polo T-shirt Blue", 350, 1 },
                    { 5, "https://i.etsystatic.com/23923551/r/il/07971c/4617683391/il_794xN.4617683391_q1eb.jpg", "T-Shirt with MPower logo. 100% cotton", "https://i.etsystatic.com/23923551/r/il/835b62/4617683235/il_794xN.4617683235_m6i4.jpg", "BMW M Power T-shirt", 350, 1 },
                    { 6, "https://i.etsystatic.com/37986977/r/il/9b1aa2/4318736693/il_794xN.4318736693_lsqr.jpg", "T-Shirt with M Power logo. 100% cotton", "https://i.etsystatic.com/37986977/r/il/e787e2/4318736695/il_794xN.4318736695_s3rv.jpg", "BMW Nascar T-Shirt", 400, 1 },
                    { 7, "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/1", "Formula 1 style shirt. 100% cotton", "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/2", "BMW Williams F1 Crew T-shirt White and Blue", 400, 1 },
                    { 8, "https://media.karousell.com/media/photos/products/2021/10/26/bmw_sauber_f1_team_petronas_in_1635244034_24c7ce71_progressive.jpg", "Formula 1 style shirt. 100% cotton", "https://media.karousell.com/media/photos/products/2021/10/26/bmw_sauber_f1_team_petronas_in_1635244034_9a80fcb9_progressive.jpg", "BMW Formula 1 Petronas Crew Shirt 1", 450, 1 },
                    { 9, "https://twitter.com/lethatsinkinbot/status/1645096175643623428/photo/1", "Formula 1 style shirt. 100% cotton", "https://twitter.com/lethatsinkinbot/status/1645096175643623428/photo/2", "BMW Formula 1 Petronas Crew Shirt 2", 500, 1 },
                    { 10, "https://i.etsystatic.com/18363295/r/il/1e6e84/3642840659/il_794xN.3642840659_bbnu.jpg", "Formula 1 style shirt. 100% cotton", "https://i.etsystatic.com/18363295/r/il/b9fff6/3595223088/il_794xN.3595223088_qz13.jpg", "Vintage BMW Formula 1 Crew Shirt", 700, 1 },
                    { 11, "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/1", "Formula 1 style shirt. 100% cotton", "https://i.etsystatic.com/27063922/r/il/73a350/4507326215/il_794xN.4507326215_m9h8.jpg", "BMW ZaHub Hoodie", 500, 2 },
                    { 12, "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/1", "Formula 1 style shirt. 100% cotton", "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/2", "BMW Puma Hoodie Grey", 750, 2 },
                    { 13, "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/1", "Formula 1 style shirt. 100% cotton", "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/2", "BMW Puma Hoodie Blue", 750, 2 },
                    { 14, "https://i.etsystatic.com/20117301/r/il/9050bc/2846453454/il_794xN.2846453454_e3id.jpg", "Baseball jacket . 100% cotton", "https://i.etsystatic.com/20117301/r/il/5c3766/2906860089/il_794xN.2906860089_526g.jpg", "BMW Baseball Jacket", 850, 3 },
                    { 15, "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/1", "Sweater with M Logo. 100% cotton", "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/2", "BMW M Power Sweater", 600, 2 },
                    { 16, "https://i.etsystatic.com/30153332/r/il/61ae17/4530441920/il_794xN.4530441920_hy8s.jpg", "Baseball jacket . 100% cotton", "https://i.etsystatic.com/30153332/r/il/2ce944/4530440406/il_794xN.4530440406_oznq.jpg", "BMW Softshell jacket", 750, 3 },
                    { 17, "https://i.etsystatic.com/23964418/r/il/f5bcd8/4012547068/il_794xN.4012547068_3fh5.jpg", "Baseball jacket . 100% cotton", "https://i.etsystatic.com/23964418/r/il/3a0206/4060192343/il_794xN.4060192343_6wit.jpg", "BMW Formula 1 Petronas Jacket", 1000, 3 },
                    { 18, "https://i.etsystatic.com/34793067/r/il/f9b57e/4543489472/il_794xN.4543489472_ny0y.jpg", "Baseball jacket . 100% leather", "https://i.etsystatic.com/34793067/r/il/ef595e/4543489458/il_794xN.4543489458_59kn.jpg", "BMW MotoGP leather jacket", 900, 3 },
                    { 19, "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/1", "Baseball jacket . 100% leather", "https://twitter.com/lethatsinkinbot/status/1645092748364595202/photo/2", "BMW Z3MIII jacket", 1050, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AwardsArchitectId",
                table: "AspNetUsers",
                column: "AwardsArchitectId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeInstances_ChallengeInstanceStatusId",
                table: "ChallengeInstances",
                column: "ChallengeInstanceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeInstances_ChallengerId",
                table: "ChallengeInstances",
                column: "ChallengerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengerMedals_ChallengerId",
                table: "ChallengerMedals",
                column: "ChallengerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengerMedals_MedalId",
                table: "ChallengerMedals",
                column: "MedalId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_ChallengeStatusID",
                table: "Challenges",
                column: "ChallengeStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_ChallengeTypeID",
                table: "Challenges",
                column: "ChallengeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_Id",
                table: "Challenges",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_MedalId",
                table: "Challenges",
                column: "MedalId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_PrizeId",
                table: "Challenges",
                column: "PrizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ChallengerId",
                table: "Comments",
                column: "ChallengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostID",
                table: "Comments",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentChallenges_DepartmentId",
                table: "DepartmentChallenges",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FunctionId",
                table: "Departments",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Id",
                table: "Departments",
                column: "Id",
                unique: true,
                filter: "[Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Functions_Id",
                table: "Functions",
                column: "Id",
                unique: true,
                filter: "[Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Functions_SuperArchitectId1",
                table: "Functions",
                column: "SuperArchitectId1");

            migrationBuilder.CreateIndex(
                name: "IX_Helps_LocationId",
                table: "Helps",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PostId",
                table: "Likes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Medals_ChallengeTypeId",
                table: "Medals",
                column: "ChallengeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ChallengerId",
                table: "Posts",
                column: "ChallengerId");

            migrationBuilder.CreateIndex(
                name: "IX_PrizeOrders_PrizeId",
                table: "PrizeOrders",
                column: "PrizeId");

            migrationBuilder.CreateIndex(
                name: "IX_PrizeOrders_PrizeOrderStatusId",
                table: "PrizeOrders",
                column: "PrizeOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PrizeOrders_RewardRedemptionId",
                table: "PrizeOrders",
                column: "RewardRedemptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrizeOrders_SupplierOrderId",
                table: "PrizeOrders",
                column: "SupplierOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Prizes_PrizeTypeID",
                table: "Prizes",
                column: "PrizeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PrizeTypes_PrizeCategoryID",
                table: "PrizeTypes",
                column: "PrizeCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CategoryId",
                table: "Questions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuestionId",
                table: "QuizQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_CategoryId",
                table: "Quizzes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RewardRedemptions_RewardRedemptionStatusId",
                table: "RewardRedemptions",
                column: "RewardRedemptionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_SupplierOrderStatusId",
                table: "SupplierOrders",
                column: "SupplierOrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_Id",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Functions_AspNetUsers_Id",
                table: "Functions");

            migrationBuilder.DropForeignKey(
                name: "FK_Functions_AspNetUsers_SuperArchitectId1",
                table: "Functions");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuditTrails");

            migrationBuilder.DropTable(
                name: "ChallengeInstances");

            migrationBuilder.DropTable(
                name: "ChallengerMedals");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DepartmentChallenges");

            migrationBuilder.DropTable(
                name: "Emojis");

            migrationBuilder.DropTable(
                name: "FAQs");

            migrationBuilder.DropTable(
                name: "Helps");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "PrizeOrders");

            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ChallengeInstanceStatus");

            migrationBuilder.DropTable(
                name: "Challenges");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "PrizeOrderStatus");

            migrationBuilder.DropTable(
                name: "RewardRedemptions");

            migrationBuilder.DropTable(
                name: "SupplierOrders");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "ChallengeStatuses");

            migrationBuilder.DropTable(
                name: "Medals");

            migrationBuilder.DropTable(
                name: "Prizes");

            migrationBuilder.DropTable(
                name: "RewardRedemptionStatus");

            migrationBuilder.DropTable(
                name: "SupplierOrderStatuses");

            migrationBuilder.DropTable(
                name: "QuestionCategories");

            migrationBuilder.DropTable(
                name: "ChallengeTypes");

            migrationBuilder.DropTable(
                name: "PrizeTypes");

            migrationBuilder.DropTable(
                name: "PrizeCategories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Functions");
        }
    }
}
