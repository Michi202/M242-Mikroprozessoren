using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M242.Model.Migrations
{
    [Migration(2)]
    public class M0002_AddTempLogging : Migration
    {
        public override void Up()
        {
            Create.Table("templogging")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("createdate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("modificationdate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)

                .WithColumn("temperature").AsDouble().Nullable()
                .WithColumn("humidity").AsDouble().Nullable()
                .WithColumn("iotikitip").AsString().Nullable()
                ;
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
