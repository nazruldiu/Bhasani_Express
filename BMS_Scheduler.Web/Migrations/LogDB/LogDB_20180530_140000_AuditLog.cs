using FluentMigrator;
using Serenity.Extensions;

namespace BMS_Scheduler.Migrations.LogDB
{
    [Migration(20180530140000)]
    public class LogDB_20180530_140000_AuditLog : AutoReversingMigration
    {
        public override void Up()
        {
            if (!Schema.Table("AuditLog").Exists())
            {
                this.CreateTableWithId64("AuditLog", "Id", s => s
                .WithColumn("VersionNo").AsInt32().NotNullable()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("ActionType").AsInt32().NotNullable()
                .WithColumn("ActionDate").AsDateTime().NotNullable()
                .WithColumn("TableName").AsString(100).NotNullable()
                .WithColumn("EntityId").AsInt64().NotNullable()
                .WithColumn("OldEntity").AsString(int.MaxValue).Nullable()
                .WithColumn("NewEntity").AsString(int.MaxValue).Nullable()
                .WithColumn("IpAddress").AsString(100).Nullable()
                .WithColumn("SessionId").AsString(100).Nullable()
                );
            }
        }

    }

    [Migration(20220811120052)]
    public class LogDB_20220811_120052_AuditLog : AutoReversingMigration
    {
        public override void Up()
        {
            if (!Schema.Table("AuditLog").Column("Changes").Exists())
            {
                Alter.Table("AuditLog").AddColumn("Changes").AsString(int.MaxValue).Nullable();
            }
        }

    }


    [Migration(20220811120752)]
    public class LogDB_20220811_120752_AuditLog : Migration
    {
        public override void Up()
        {
            if (Schema.Table("AuditLog").Column("VersionNo").Exists())
                Delete.Column("VersionNo").FromTable("AuditLog");
        }


        public override void Down()
        {
        }

    }


    [Migration(20220811135052)]
    public class LogDB_20220811_135052_AuditLog : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("AuditLog").Column("FeatureName").Exists())
                Alter.Table("AuditLog").AddColumn("FeatureName").AsString(255).Nullable();
        }

        public override void Down()
        {
        }

    }
}