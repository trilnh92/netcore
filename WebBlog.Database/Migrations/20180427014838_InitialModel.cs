using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebBlog.Database.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    ArticleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BriefContent = table.Column<string>(nullable: true),
                    CategoryArticleId = table.Column<int>(nullable: true),
                    CategoryArticleName = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Ext = table.Column<string>(nullable: true),
                    Ext1 = table.Column<string>(nullable: true),
                    Ext2 = table.Column<string>(nullable: true),
                    Ext3 = table.Column<string>(nullable: true),
                    FullContent = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsHot = table.Column<bool>(nullable: false),
                    IsVisible = table.Column<bool>(nullable: false),
                    Position = table.Column<int>(nullable: true),
                    SEName = table.Column<string>(maxLength: 4000, nullable: true),
                    SEODescription = table.Column<string>(maxLength: 4000, nullable: true),
                    SEOKeywords = table.Column<string>(maxLength: 4000, nullable: true),
                    SEOTitle = table.Column<string>(maxLength: 4000, nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    ViewCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ArticleId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    HasChildren = table.Column<bool>(nullable: false),
                    HasUrl = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    Language = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    ParentName = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<int>(nullable: false),
                    CommentId1 = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Ext = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_CommentId1",
                        column: x => x.CommentId1,
                        principalTable: "Comment",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    MediaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryMediaId = table.Column<int>(nullable: true),
                    CategoryMediaName = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Ext = table.Column<string>(nullable: true),
                    HostId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ReferenceSource = table.Column<string>(nullable: true),
                    SEName = table.Column<string>(maxLength: 4000, nullable: true),
                    SEODescription = table.Column<string>(maxLength: 4000, nullable: true),
                    SEOKeywords = table.Column<string>(maxLength: 4000, nullable: true),
                    SEOTitle = table.Column<string>(maxLength: 4000, nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.MediaId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CommentId1",
                table: "Comment",
                column: "CommentId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Media");
        }
    }
}
