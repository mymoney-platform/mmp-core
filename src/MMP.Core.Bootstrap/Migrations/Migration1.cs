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
            .Table(nameof(Operation))
            .InSchema(MMPConstants.Schema)
            .WithColumn(nameof(Operation.Id)).AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(Operation.OperationId)).AsGuid().NotNullable()
            .WithColumn(nameof(Operation.AccountId)).AsGuid().NotNullable()
            .WithColumn(nameof(Operation.Value)).AsDecimal().NotNullable()
            .WithColumn(nameof(Operation.Description)).AsString(300).Nullable()
            .WithColumn(nameof(Operation.ExternalId)).AsGuid().Nullable()
            .WithColumn(nameof(Operation.OperationCategory)).AsInt16().NotNullable()
            .WithColumn(nameof(Operation.OperationType)).AsInt16().NotNullable();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}