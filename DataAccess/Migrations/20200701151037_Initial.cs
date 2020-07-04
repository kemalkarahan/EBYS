using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 512, nullable: false),
                    Domain = table.Column<string>(maxLength: 64, nullable: false),
                    SmtpHost = table.Column<string>(maxLength: 64, nullable: false),
                    SmtpPort = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 64, nullable: false),
                    Password = table.Column<string>(maxLength: 32, nullable: false),
                    Logo = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    UpperUnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Units_UpperUnitId",
                        column: x => x.UpperUnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 28, nullable: false),
                    IsMultiple = table.Column<bool>(nullable: false),
                    IsManager = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offices_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inbox",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorrespondenceId = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inbox", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorrespondenceId = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 14, nullable: false),
                    LastName = table.Column<string>(maxLength: 14, nullable: false),
                    Nickname = table.Column<string>(maxLength: 64, nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: false),
                    Gender = table.Column<string>(maxLength: 5, nullable: false),
                    ProfileImage = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    CorrespondenceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staffs_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AboutMes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(nullable: false),
                    Education = table.Column<string>(maxLength: 512, nullable: true),
                    Skills = table.Column<string>(maxLength: 512, nullable: true),
                    Notes = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutMes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboutMes_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chairs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficeId = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chairs_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chairs_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Correspondences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 56, nullable: false),
                    Context = table.Column<string>(maxLength: 512, nullable: false),
                    SentAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correspondences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Correspondences_Staffs_FromId",
                        column: x => x.FromId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsAddition = table.Column<bool>(nullable: false),
                    HasAdditions = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Staffs_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drafts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 56, nullable: true),
                    Context = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drafts_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Duties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromId = table.Column<int>(nullable: false),
                    To = table.Column<int>(nullable: false),
                    TaskName = table.Column<string>(maxLength: 128, nullable: false),
                    TaskType = table.Column<string>(maxLength: 128, nullable: false),
                    TaskDescription = table.Column<string>(maxLength: 512, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Category = table.Column<string>(maxLength: 128, nullable: false),
                    ReminderId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Duties_Staffs_FromId",
                        column: x => x.FromId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorrespondenceId = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tos_Correspondences_CorrespondenceId",
                        column: x => x.CorrespondenceId,
                        principalTable: "Correspondences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trash",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorrespondenceId = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trash", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trash_Correspondences_CorrespondenceId",
                        column: x => x.CorrespondenceId,
                        principalTable: "Correspondences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trash_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DocumentAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(nullable: false),
                    DocumentAttachmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentAttachments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentRelateds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(nullable: false),
                    DocumentRelatedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRelateds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentRelateds_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<int>(nullable: false),
                    To = table.Column<int>(nullable: false),
                    SentAt = table.Column<DateTime>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    IsUrgent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submissions_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Institutions",
                columns: new[] { "Id", "Domain", "Email", "Logo", "Name", "Password", "SmtpHost", "SmtpPort" },
                values: new object[] { 1, "@gmail.com", "tempmaildonotreply", "Logo.png", "Boğaziçi Üniversitesi", "karahanll09", "smtp.gmail.com", 587 });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "UpperUnitId" },
                values: new object[] { 1, "Merkez", 1 });

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "Id", "CorrespondenceId", "DateOfBirth", "Gender", "LastName", "Name", "Nickname", "Password", "ProfileImage", "Title", "UnitId" },
                values: new object[] { 1, null, new DateTime(2020, 7, 1, 18, 10, 36, 137, DateTimeKind.Local).AddTicks(4802), "Erkek", "Admin", "Super", "super.admin", "495727f352fb312383b64282f338d118bfbcd4b60d86c395b9a1b6b73196b99c772d1db6e256f35898d86a7bfa0adcb406eb2f8de15c123c759d0d3a02abbdbc", "DefaultProfileImage.png", "Admin", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AboutMes_StaffId",
                table: "AboutMes",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Chairs_OfficeId",
                table: "Chairs",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Chairs_StaffId",
                table: "Chairs",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Correspondences_FromId",
                table: "Correspondences",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentAttachments_DocumentId",
                table: "DocumentAttachments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRelateds_DocumentId",
                table: "DocumentRelateds",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_OwnerId",
                table: "Documents",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_StaffId",
                table: "Drafts",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Duties_FromId",
                table: "Duties",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_CorrespondenceId",
                table: "Inbox",
                column: "CorrespondenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Inbox_StaffId",
                table: "Inbox",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_UnitId",
                table: "Offices",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Sent_CorrespondenceId",
                table: "Sent",
                column: "CorrespondenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Sent_StaffId",
                table: "Sent",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_CorrespondenceId",
                table: "Staffs",
                column: "CorrespondenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_UnitId",
                table: "Staffs",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_DocumentId",
                table: "Submissions",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tos_CorrespondenceId",
                table: "Tos",
                column: "CorrespondenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Trash_CorrespondenceId",
                table: "Trash",
                column: "CorrespondenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Trash_StaffId",
                table: "Trash",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_UpperUnitId",
                table: "Units",
                column: "UpperUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inbox_Staffs_StaffId",
                table: "Inbox",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inbox_Correspondences_CorrespondenceId",
                table: "Inbox",
                column: "CorrespondenceId",
                principalTable: "Correspondences",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Sent_Staffs_StaffId",
                table: "Sent",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sent_Correspondences_CorrespondenceId",
                table: "Sent",
                column: "CorrespondenceId",
                principalTable: "Correspondences",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Correspondences_CorrespondenceId",
                table: "Staffs",
                column: "CorrespondenceId",
                principalTable: "Correspondences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Correspondences_Staffs_FromId",
                table: "Correspondences");

            migrationBuilder.DropTable(
                name: "AboutMes");

            migrationBuilder.DropTable(
                name: "Chairs");

            migrationBuilder.DropTable(
                name: "DocumentAttachments");

            migrationBuilder.DropTable(
                name: "DocumentRelateds");

            migrationBuilder.DropTable(
                name: "Drafts");

            migrationBuilder.DropTable(
                name: "Duties");

            migrationBuilder.DropTable(
                name: "Inbox");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "Sent");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "Tos");

            migrationBuilder.DropTable(
                name: "Trash");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Correspondences");

            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
