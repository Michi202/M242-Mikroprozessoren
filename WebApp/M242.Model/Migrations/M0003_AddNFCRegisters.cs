using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M242.Model.Migrations
{
    [Migration(3)]
    public class M0003_AddNFCRegisters : Migration
    {
        public override void Up()
        {
            Create.Table("nfcregisters")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("createdate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("modificationdate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)

                .WithColumn("nummber").AsString().Nullable()

                ;
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
