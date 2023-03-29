using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessClub.DAL.FitnessClubDataBase.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dictionaries");

            migrationBuilder.EnsureSchema(
                name: "consumers");

            migrationBuilder.CreateTable(
                name: "Exercises",
                schema: "dictionaries",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatuses",
                schema: "dictionaries",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "dictionaries",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "consumers",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRoleCode = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleCode",
                        column: x => x.UserRoleCode,
                        principalSchema: "dictionaries",
                        principalTable: "UserRoles",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Porpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestStatusCode = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserManagerGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserClientGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Requests_RequestStatuses_RequestStatusCode",
                        column: x => x.RequestStatusCode,
                        principalSchema: "dictionaries",
                        principalTable: "RequestStatuses",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Users_UserClientGuid",
                        column: x => x.UserClientGuid,
                        principalSchema: "consumers",
                        principalTable: "Users",
                        principalColumn: "Guid");
                    table.ForeignKey(
                        name: "FK_Requests_Users_UserManagerGuid",
                        column: x => x.UserManagerGuid,
                        principalSchema: "consumers",
                        principalTable: "Users",
                        principalColumn: "Guid");
                });

            migrationBuilder.CreateTable(
                name: "IndividualPlans",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<decimal>(type: "money", nullable: false),
                    RequestGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPlans", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_IndividualPlans_Requests_RequestGuid",
                        column: x => x.RequestGuid,
                        principalTable: "Requests",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfRepetitions = table.Column<byte>(type: "tinyint", nullable: false),
                    QuantityPerWeek = table.Column<byte>(type: "tinyint", nullable: false),
                    AssignedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    Pulse = table.Column<byte>(type: "tinyint", nullable: true),
                    IndividualPlanGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Workouts_IndividualPlans_IndividualPlanGuid",
                        column: x => x.IndividualPlanGuid,
                        principalTable: "IndividualPlans",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseWorkout",
                columns: table => new
                {
                    ExercisesCode = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    WorkoutsGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseWorkout", x => new { x.ExercisesCode, x.WorkoutsGuid });
                    table.ForeignKey(
                        name: "FK_ExerciseWorkout_Exercises_ExercisesCode",
                        column: x => x.ExercisesCode,
                        principalSchema: "dictionaries",
                        principalTable: "Exercises",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseWorkout_Workouts_WorkoutsGuid",
                        column: x => x.WorkoutsGuid,
                        principalTable: "Workouts",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseWorkout_WorkoutsGuid",
                table: "ExerciseWorkout",
                column: "WorkoutsGuid");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlans_Guid",
                table: "IndividualPlans",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPlans_RequestGuid",
                table: "IndividualPlans",
                column: "RequestGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_Guid",
                table: "Requests",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestStatusCode",
                table: "Requests",
                column: "RequestStatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserClientGuid",
                table: "Requests",
                column: "UserClientGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserManagerGuid",
                table: "Requests",
                column: "UserManagerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Guid",
                schema: "consumers",
                table: "Users",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleCode",
                schema: "consumers",
                table: "Users",
                column: "UserRoleCode");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_Guid",
                table: "Workouts",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_IndividualPlanGuid",
                table: "Workouts",
                column: "IndividualPlanGuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseWorkout");

            migrationBuilder.DropTable(
                name: "Exercises",
                schema: "dictionaries");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "IndividualPlans");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "RequestStatuses",
                schema: "dictionaries");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "consumers");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "dictionaries");
        }
    }
}
