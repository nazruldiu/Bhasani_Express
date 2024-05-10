using FluentMigrator;
using FluentMigrator.Builders;
using Serenity.Extensions;
using System;

namespace BMS_Scheduler.Migrations.DefaultDB
{
    [Migration(20141103140000)]
    public class DefaultDB_20141103_140000_Initial : AutoReversingMigration
    {
        public override void Up()
        {
            this.CreateTableWithId32("Users", "UserId", s => s
                .WithColumn("Username").AsString(100).NotNullable()
                .WithColumn("DisplayName").AsString(100).NotNullable()
                .WithColumn("Email").AsString(100).Nullable()
                .WithColumn("Source").AsString(4).NotNullable()
                .WithColumn("PasswordHash").AsString(86).NotNullable()
                .WithColumn("PasswordSalt").AsString(10).NotNullable()
                .WithColumn("LastDirectoryUpdate").AsDateTime().Nullable()
                .WithColumn("UserImage").AsString(100).Nullable()
                .WithColumn("InsertDate").AsDateTime().NotNullable()
                .WithColumn("InsertUserId").AsInt32().NotNullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable()
                .WithColumn("IsActive").AsInt16().NotNullable().WithDefaultValue(1));

            Insert.IntoTable("Users").Row(new
            {
                Username = "admin",
                DisplayName = "admin",
                Email = "admin@dummy.com",
                Source = "site",
                PasswordHash = "rfqpSPYs0ekFlPyvIRTXsdhE/qrTHFF+kKsAUla7pFkXL4BgLGlTe89GDX5DBysenMDj8AqbIZPybqvusyCjwQ",
                PasswordSalt = "hJf_F",
                InsertDate = new DateTime(2014, 1, 1),
                InsertUserId = 1,
                IsActive = 1
            });

            this.CreateTableWithId32("Languages", "Id", s => s
                .WithColumn("LanguageId").AsString(10).NotNullable()
                .WithColumn("LanguageName").AsString(50).NotNullable());

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "en",
                LanguageName = "English"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "ru",
                LanguageName = "Russian"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "es",
                LanguageName = "Spanish"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "tr",
                LanguageName = "Turkish"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "de",
                LanguageName = "German"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "zh-CN",
                LanguageName = "Chinese (Simplified)"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "it",
                LanguageName = "Italian"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "pt",
                LanguageName = "Portuguese"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "pt-BR",
                LanguageName = "Portuguese (Brazil)"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "fa",
                LanguageName = "Farsi"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "vi-VN",
                LanguageName = "Vietnamese (Vietnam)"
            });
        }
    }

    [Migration(20220704_153857)]
    public class AlterTable_Users_20220704_153857 : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("IsAdmin").AsBoolean().Nullable();

            Execute.Sql("Update Users SET IsAdmin = 1 where Username = 'admin'");
        }

        public override void Down() { }
    }

    [Migration(20230114_115557)]
    public class ChangesTable_20230114_115557_Shipping : Migration
    {
        public override void Up()
        {
            this.CreateTableWithId32("Country", "Id", s => s
                .WithColumn("Name").AsString(100).Nullable()
                );

            this.CreateTableWithId32("CompanyInfo", "Id", s => s
                .WithColumn("Name").AsString(100).Nullable()
                .WithColumn("Address").AsString(400).Nullable()
                .WithColumn("WebSite").AsString(100).Nullable()
                .WithColumn("Phone").AsString(100).Nullable()
                .WithColumn("Fax").AsString(100).Nullable()
                .WithColumn("Logo").AsString(200).Nullable()
                );

            this.CreateTableWithId32("Shipping", "Id", s => s
                .WithColumn("OrderNo").AsString(100).Nullable()
                .WithColumn("InVoiceNo").AsString(100).Nullable()
                .WithColumn("InVoiceDate").AsDateTime().Nullable()
                .WithColumn("Consignee").AsString(400).Nullable()
                .WithColumn("TotalAmount").AsDecimal(18, 2).Nullable()
                .WithColumn("TotalWeight").AsDecimal(18, 2).Nullable()
                .WithColumn("VesselOrFlightNo").AsString(100).Nullable()
                .WithColumn("PaymentTerms").AsString(100).Nullable()
                .WithColumn("PackingSlipNo").AsString(100).Nullable()
                .WithColumn("TotalBox").AsInt32().Nullable()
                .WithColumn("CountryOfOriginOfGoods").AsInt32().Nullable()
                .WithColumn("FinalDestination").AsInt32().Nullable()
                .WithColumn("Measure").AsString(100).Nullable()
                );

            this.CreateTableWithId32("ShippingItems", "Id", s => s
                .WithColumn("ShippingId").AsInt32().NotNullable().ForeignKey("ShippingItems_Shipping", "Shipping", "Id")
                .WithColumn("DescriptionOfGoods").AsString(400).NotNullable()
                .WithColumn("Traiff").AsString(100).Nullable()
                .WithColumn("Quantity").AsInt32().Nullable()
                .WithColumn("UOM").AsString(100).Nullable()
                .WithColumn("UnitRate").AsDecimal(18, 2).Nullable()
                .WithColumn("TotalRate").AsDecimal(18, 2).Nullable()
                );
        }

        public override void Down() { }
    }

}