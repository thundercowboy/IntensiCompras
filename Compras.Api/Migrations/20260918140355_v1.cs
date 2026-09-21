using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compras.Api.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aprovacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aprovacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriasItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materiais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoMaterial = table.Column<int>(type: "INT", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solicitacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descricao = table.Column<string>(type: "NVARCHAR(260)", maxLength: 260, nullable: false),
                    Setor = table.Column<short>(type: "SMALLINT", nullable: false),
                    StatusSolicitacao = table.Column<short>(type: "SMALLINT", nullable: false),
                    IdAprovacao = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitacoes_Aprovacoes_IdAprovacao",
                        column: x => x.IdAprovacao,
                        principalTable: "Aprovacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Itens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMaterial = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<decimal>(type: "MONEY", nullable: false),
                    IdUnidade = table.Column<int>(type: "int", nullable: false),
                    IdFornecedor = table.Column<int>(type: "int", nullable: false),
                    IdSolicitacao = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "VARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Itens_Fornecedores_IdFornecedor",
                        column: x => x.IdFornecedor,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Itens_Materiais_IdMaterial",
                        column: x => x.IdMaterial,
                        principalTable: "Materiais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Itens_Solicitacoes_IdSolicitacao",
                        column: x => x.IdSolicitacao,
                        principalTable: "Solicitacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Itens_Unidades_IdUnidade",
                        column: x => x.IdUnidade,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Itens_IdFornecedor",
                table: "Itens",
                column: "IdFornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_IdMaterial",
                table: "Itens",
                column: "IdMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_IdSolicitacao",
                table: "Itens",
                column: "IdSolicitacao");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_IdUnidade",
                table: "Itens",
                column: "IdUnidade");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_IdAprovacao",
                table: "Solicitacoes",
                column: "IdAprovacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriasItem");

            migrationBuilder.DropTable(
                name: "Itens");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Materiais");

            migrationBuilder.DropTable(
                name: "Solicitacoes");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.DropTable(
                name: "Aprovacoes");
        }
    }
}
