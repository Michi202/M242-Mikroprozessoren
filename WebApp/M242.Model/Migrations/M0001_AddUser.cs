using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M242.Model.Migrations
{
    [Migration(1)]
    public class M0001_AddUser : Migration
    {
        public override void Up()
        {
            Create.Table("user")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("createdate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("modificationdate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)

                .WithColumn("username").AsString().Nullable()
                .WithColumn("password").AsString().Nullable()
                .WithColumn("nfccardid").AsString().Nullable()
                .WithColumn("buttoncode").AsInt64().Nullable()

                ;
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
