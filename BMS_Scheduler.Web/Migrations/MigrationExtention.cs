using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;

namespace BMS_Scheduler.Migrations
{
    public static class MigrationExtention
    {

        public static ICreateTableWithColumnSyntax WithCommonFields(this ICreateTableWithColumnSyntax e)
        {
            return e.WithColumn("IUser").AsString(50).Nullable()
                    .WithColumn("EUser").AsString(50).Nullable()
                    .WithColumn("IDate").AsDateTime().Nullable().WithDefault(SystemMethods.CurrentDateTime)
                    .WithColumn("EDate").AsDateTime().Nullable();

        }

        public static ICreateTableWithColumnSyntax WithCommonSetupFields(this ICreateTableWithColumnSyntax e)
        {
            return e.WithColumn("SortOrder").AsInt32().Nullable()
                    .WithCommonFields();

        }

        public static ICreateTableWithColumnSyntax CaptureLogFields(this ICreateTableWithColumnSyntax e)
        {
            return e.WithColumn("OperationType").AsInt32().NotNullable()
                .WithColumn("ChangingUserId").AsInt32().Nullable()
                .WithColumn("ValidFrom").AsDateTime().NotNullable()
                .WithColumn("ValidUntil").AsDateTime().NotNullable();

        }

    }
}