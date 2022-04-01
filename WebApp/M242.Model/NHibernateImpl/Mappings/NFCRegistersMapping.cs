using FluentNHibernate.Mapping;
using M242.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M242.Model.NHibernateImpl.Mappings
{
    public class NFCRegistersMapping : ClassMap<NFCRegisters>
    {
        public NFCRegistersMapping()
        {
            Id(x => x.Id).Unique();
            Map(x => x.CreateDate).Not.Nullable();
            Map(x => x.ModificationDate).Not.Nullable();

            Map(x => x.Nummber);
        }
    }
}
