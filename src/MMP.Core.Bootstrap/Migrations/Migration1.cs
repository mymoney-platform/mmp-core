using FluentMigrator;
using FluentMigrator.Model;
using FluentMigrator.Postgres;
using MMP.Core.Data;
using MMP.Core.Data.Entities;

namespace MMP.Core.Bootstrap.Migrations;

[Migration(1)]
public class Migration1 : Migration
{
    public override void Up()
    {
        Create
            .Table(Operation.GetTableName(nameof(Operation)))
            .InSchema(MMPConstants.Schema)
            .WithColumn(nameof(Operation.Id)).AsInt64().NotNullable().PrimaryKey().Identity().Unique()
            .WithColumn(nameof(Operation.OperationId)).AsGuid().NotNullable().Indexed($"IDX_{nameof(Operation.OperationId)}")
            .WithColumn(nameof(Operation.AccountId)).AsGuid().NotNullable().Indexed($"IDX_{nameof(Operation.AccountId)}")
            .WithColumn(nameof(Operation.Value)).AsDecimal().NotNullable()
            .WithColumn(nameof(Operation.Description)).AsString(300).Nullable()
            .WithColumn(nameof(Operation.ExternalId)).AsGuid().Nullable().Indexed($"IDX_{nameof(Operation.ExternalId)}")
            .WithColumn(nameof(Operation.OperationCategory)).AsInt16().NotNullable()
            .WithColumn(nameof(Operation.OperationType)).AsInt16().NotNullable();
    }

    public override void Down()
    {
        Delete.Table(nameof(Operation));
    }
}