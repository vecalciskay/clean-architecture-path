﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webAPI.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookReaders",
                columns: table => new
                {
                    BookReaderId = table.Column<Guid>(nullable: false),
                    Version = table.Column<long>(nullable: true),
                    Name_Value = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReaders", x => x.BookReaderId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<Guid>(nullable: false),
                    Version = table.Column<long>(nullable: true),
                    CurrentBookReaderId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "EventSequences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NextSequence = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSequences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventStore",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StreamName = table.Column<string>(nullable: true),
                    EventClass = table.Column<string>(nullable: true),
                    InstanceId = table.Column<Guid>(nullable: false),
                    SerializedEvent = table.Column<string>(nullable: true),
                    Version = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStore", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookLends",
                columns: table => new
                {
                    BookLendId = table.Column<Guid>(nullable: false),
                    BookRefBookId = table.Column<Guid>(nullable: true),
                    ReaderRefBookReaderId = table.Column<Guid>(nullable: true),
                    DateLent = table.Column<DateTime>(nullable: true),
                    DateReturned = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLends", x => x.BookLendId);
                    table.ForeignKey(
                        name: "FK_BookLends_Books_BookRefBookId",
                        column: x => x.BookRefBookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookLends_BookReaders_ReaderRefBookReaderId",
                        column: x => x.ReaderRefBookReaderId,
                        principalTable: "BookReaders",
                        principalColumn: "BookReaderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookLends_BookRefBookId",
                table: "BookLends",
                column: "BookRefBookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLends_ReaderRefBookReaderId",
                table: "BookLends",
                column: "ReaderRefBookReaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookLends");

            migrationBuilder.DropTable(
                name: "EventSequences");

            migrationBuilder.DropTable(
                name: "EventStore");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "BookReaders");
        }
    }
}
