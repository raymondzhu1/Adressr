using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adressr.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyID);
                    table.ForeignKey(
                        name: "FK_Companies_Users_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    ExamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TimeLimit = table.Column<TimeSpan>(type: "time", nullable: false),
                    MaxRetakes = table.Column<int>(type: "int", nullable: true),
                    Private = table.Column<bool>(type: "bit", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.ExamID);
                    table.CheckConstraint("CK_Exam_MaxRetakes_NonNegative", "[MaxRetakes] IS NULL OR [MaxRetakes] >= 0");
                    table.ForeignKey(
                        name: "FK_Exams_Users_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    DarkMode = table.Column<bool>(type: "bit", nullable: false),
                    ProfilePrivate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Preferences_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfilePicLocation = table.Column<string>(type: "VARCHAR(300)", nullable: false),
                    Education = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileID);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionID);
                    table.UniqueConstraint("AK_Positions_PositionID_CompanyID", x => new { x.PositionID, x.CompanyID });
                    table.ForeignKey(
                        name: "FK_Positions_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamAttempts",
                columns: table => new
                {
                    ExamAttemptID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: true),
                    ExamID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAttempts", x => x.ExamAttemptID);
                    table.CheckConstraint("CK_ExamAttempt_Score_NonNegative", "[Score] IS NULL OR [Score] >= 0");
                    table.ForeignKey(
                        name: "FK_ExamAttempts_Exams_ExamID",
                        column: x => x.ExamID,
                        principalTable: "Exams",
                        principalColumn: "ExamID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamAttempts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateExamParticipants",
                columns: table => new
                {
                    PrivateExamParticipantsUserID = table.Column<int>(type: "int", nullable: false),
                    PrivateExamsExamID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateExamParticipants", x => new { x.PrivateExamParticipantsUserID, x.PrivateExamsExamID });
                    table.ForeignKey(
                        name: "FK_PrivateExamParticipants_Exams_PrivateExamsExamID",
                        column: x => x.PrivateExamsExamID,
                        principalTable: "Exams",
                        principalColumn: "ExamID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrivateExamParticipants_Users_PrivateExamParticipantsUserID",
                        column: x => x.PrivateExamParticipantsUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AnswerType = table.Column<int>(type: "int", nullable: false),
                    ExamID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Exams_ExamID",
                        column: x => x.ExamID,
                        principalTable: "Exams",
                        principalColumn: "ExamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    ExperienceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RoleTitle = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsExported = table.Column<bool>(type: "bit", nullable: false),
                    ProfileID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.ExperienceID);
                    table.ForeignKey(
                        name: "FK_Experiences_Profiles_ProfileID",
                        column: x => x.ProfileID,
                        principalTable: "Profiles",
                        principalColumn: "ProfileID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileHobbies",
                columns: table => new
                {
                    ProfileHobbyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HobbyText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileHobbies", x => x.ProfileHobbyID);
                    table.ForeignKey(
                        name: "FK_ProfileHobbies_Profiles_ProfileID",
                        column: x => x.ProfileID,
                        principalTable: "Profiles",
                        principalColumn: "ProfileID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileInterests",
                columns: table => new
                {
                    ProfileInterestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileInterests", x => x.ProfileInterestID);
                    table.ForeignKey(
                        name: "FK_ProfileInterests_Profiles_ProfileID",
                        column: x => x.ProfileID,
                        principalTable: "Profiles",
                        principalColumn: "ProfileID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Openings",
                columns: table => new
                {
                    OpeningID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wage = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: false),
                    WageType = table.Column<int>(type: "int", nullable: false),
                    NumberOfOpenings = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    PositionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Openings", x => x.OpeningID);
                    table.CheckConstraint("CK_Opening_NumberOfOpenings_NonNegative", "[NumberOfOpenings] >= 0");
                    table.ForeignKey(
                        name: "FK_Openings_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Openings_Positions_PositionID_CompanyID",
                        columns: x => new { x.PositionID, x.CompanyID },
                        principalTable: "Positions",
                        principalColumns: new[] { "PositionID", "CompanyID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlainTextEmployees",
                columns: table => new
                {
                    PlainTextEmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PositionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlainTextEmployees", x => x.PlainTextEmployeeID);
                    table.ForeignKey(
                        name: "FK_PlainTextEmployees_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "PositionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PositionPreferences",
                columns: table => new
                {
                    PositionID = table.Column<int>(type: "int", nullable: false),
                    CanPostOpening = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionPreferences", x => x.PositionID);
                    table.ForeignKey(
                        name: "FK_PositionPreferences_Positions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "PositionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkersToPosition",
                columns: table => new
                {
                    PositionsPositionID = table.Column<int>(type: "int", nullable: false),
                    WorkersUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkersToPosition", x => new { x.PositionsPositionID, x.WorkersUserID });
                    table.ForeignKey(
                        name: "FK_WorkersToPosition_Positions_PositionsPositionID",
                        column: x => x.PositionsPositionID,
                        principalTable: "Positions",
                        principalColumn: "PositionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkersToPosition_Users_WorkersUserID",
                        column: x => x.WorkersUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoiceAnswerOptions",
                columns: table => new
                {
                    MultipleChoiceAnswerOptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MultipleChoiceOptionText = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MultipleChoiceOption = table.Column<int>(type: "int", nullable: false),
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceAnswerOptions", x => x.MultipleChoiceAnswerOptionID);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceAnswerOptions_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmittedAnswers",
                columns: table => new
                {
                    SubmittedAnswerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpenResponseAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    ExamAttemptID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmittedAnswers", x => x.SubmittedAnswerID);
                    table.ForeignKey(
                        name: "FK_SubmittedAnswers_ExamAttempts_ExamAttemptID",
                        column: x => x.ExamAttemptID,
                        principalTable: "ExamAttempts",
                        principalColumn: "ExamAttemptID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubmittedAnswers_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceSkills",
                columns: table => new
                {
                    ExperiencesExperienceID = table.Column<int>(type: "int", nullable: false),
                    SkillsSkillID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceSkills", x => new { x.ExperiencesExperienceID, x.SkillsSkillID });
                    table.ForeignKey(
                        name: "FK_ExperienceSkills_Experiences_ExperiencesExperienceID",
                        column: x => x.ExperiencesExperienceID,
                        principalTable: "Experiences",
                        principalColumn: "ExperienceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExperienceSkills_Skills_SkillsSkillID",
                        column: x => x.SkillsSkillID,
                        principalTable: "Skills",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    WorkTaskID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExperienceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.WorkTaskID);
                    table.ForeignKey(
                        name: "FK_Tasks_Experiences_ExperienceID",
                        column: x => x.ExperienceID,
                        principalTable: "Experiences",
                        principalColumn: "ExperienceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpeningApplicants",
                columns: table => new
                {
                    ApplicantID = table.Column<int>(type: "int", nullable: false),
                    OpeningID = table.Column<int>(type: "int", nullable: false),
                    DateApplied = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningApplicants", x => new { x.OpeningID, x.ApplicantID });
                    table.ForeignKey(
                        name: "FK_OpeningApplicants_Openings_OpeningID",
                        column: x => x.OpeningID,
                        principalTable: "Openings",
                        principalColumn: "OpeningID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpeningApplicants_Users_ApplicantID",
                        column: x => x.ApplicantID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpeningSkills",
                columns: table => new
                {
                    OpeningsOpeningID = table.Column<int>(type: "int", nullable: false),
                    SkillsSkillID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningSkills", x => new { x.OpeningsOpeningID, x.SkillsSkillID });
                    table.ForeignKey(
                        name: "FK_OpeningSkills_Openings_OpeningsOpeningID",
                        column: x => x.OpeningsOpeningID,
                        principalTable: "Openings",
                        principalColumn: "OpeningID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpeningSkills_Skills_SkillsSkillID",
                        column: x => x.SkillsSkillID,
                        principalTable: "Skills",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CorrectAnswers",
                columns: table => new
                {
                    MultipleChoiceAnswerOptionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectAnswers", x => x.MultipleChoiceAnswerOptionID);
                    table.ForeignKey(
                        name: "FK_CorrectAnswers_MultipleChoiceAnswerOptions_MultipleChoiceAnswerOptionID",
                        column: x => x.MultipleChoiceAnswerOptionID,
                        principalTable: "MultipleChoiceAnswerOptions",
                        principalColumn: "MultipleChoiceAnswerOptionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmittedAnswerSelectedAnswers",
                columns: table => new
                {
                    MultipleChoiceAnswerOptionID = table.Column<int>(type: "int", nullable: false),
                    SubmittedAnswerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmittedAnswerSelectedAnswers", x => new { x.MultipleChoiceAnswerOptionID, x.SubmittedAnswerID });
                    table.ForeignKey(
                        name: "FK_SubmittedAnswerSelectedAnswers_MultipleChoiceAnswerOptions_MultipleChoiceAnswerOptionID",
                        column: x => x.MultipleChoiceAnswerOptionID,
                        principalTable: "MultipleChoiceAnswerOptions",
                        principalColumn: "MultipleChoiceAnswerOptionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubmittedAnswerSelectedAnswers_SubmittedAnswers_SubmittedAnswerID",
                        column: x => x.SubmittedAnswerID,
                        principalTable: "SubmittedAnswers",
                        principalColumn: "SubmittedAnswerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreatorID",
                table: "Companies",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAttempts_ExamID",
                table: "ExamAttempts",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAttempts_UserID",
                table: "ExamAttempts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CreatorID",
                table: "Exams",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_ProfileID",
                table: "Experiences",
                column: "ProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceSkills_SkillsSkillID",
                table: "ExperienceSkills",
                column: "SkillsSkillID");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceAnswerOptions_QuestionID_MultipleChoiceOption",
                table: "MultipleChoiceAnswerOptions",
                columns: new[] { "QuestionID", "MultipleChoiceOption" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpeningApplicants_ApplicantID",
                table: "OpeningApplicants",
                column: "ApplicantID");

            migrationBuilder.CreateIndex(
                name: "IX_Openings_CompanyID",
                table: "Openings",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Openings_PositionID_CompanyID",
                table: "Openings",
                columns: new[] { "PositionID", "CompanyID" });

            migrationBuilder.CreateIndex(
                name: "IX_OpeningSkills_SkillsSkillID",
                table: "OpeningSkills",
                column: "SkillsSkillID");

            migrationBuilder.CreateIndex(
                name: "IX_PlainTextEmployees_PositionID",
                table: "PlainTextEmployees",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_CompanyID",
                table: "Positions",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateExamParticipants_PrivateExamsExamID",
                table: "PrivateExamParticipants",
                column: "PrivateExamsExamID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileHobbies_ProfileID",
                table: "ProfileHobbies",
                column: "ProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileInterests_ProfileID",
                table: "ProfileInterests",
                column: "ProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserID",
                table: "Profiles",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ExamID",
                table: "Questions",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Name",
                table: "Skills",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedAnswers_ExamAttemptID",
                table: "SubmittedAnswers",
                column: "ExamAttemptID");

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedAnswers_QuestionID",
                table: "SubmittedAnswers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_SubmittedAnswerSelectedAnswers_SubmittedAnswerID",
                table: "SubmittedAnswerSelectedAnswers",
                column: "SubmittedAnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ExperienceID",
                table: "Tasks",
                column: "ExperienceID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkersToPosition_WorkersUserID",
                table: "WorkersToPosition",
                column: "WorkersUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrectAnswers");

            migrationBuilder.DropTable(
                name: "ExperienceSkills");

            migrationBuilder.DropTable(
                name: "OpeningApplicants");

            migrationBuilder.DropTable(
                name: "OpeningSkills");

            migrationBuilder.DropTable(
                name: "PlainTextEmployees");

            migrationBuilder.DropTable(
                name: "PositionPreferences");

            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.DropTable(
                name: "PrivateExamParticipants");

            migrationBuilder.DropTable(
                name: "ProfileHobbies");

            migrationBuilder.DropTable(
                name: "ProfileInterests");

            migrationBuilder.DropTable(
                name: "SubmittedAnswerSelectedAnswers");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "WorkersToPosition");

            migrationBuilder.DropTable(
                name: "Openings");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "MultipleChoiceAnswerOptions");

            migrationBuilder.DropTable(
                name: "SubmittedAnswers");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "ExamAttempts");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
