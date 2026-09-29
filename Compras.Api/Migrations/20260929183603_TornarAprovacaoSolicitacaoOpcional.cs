using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Compras.Api.Migrations
{
    public partial class TornarAprovacaoSolicitacaoOpcional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdAprovacao",
                table: "Solicitacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // A reversão exige que as solicitações existentes tenham aprovação.
            migrationBuilder.Sql("""
                IF EXISTS (SELECT 1 FROM [Solicitacoes] WHERE [IdAprovacao] IS NULL)
                    THROW 50001, 'Vincule as solicitacoes sem aprovacao antes de reverter esta migration.', 1;
                """);

            migrationBuilder.AlterColumn<int>(
                name: "IdAprovacao",
                table: "Solicitacoes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
