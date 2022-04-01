using FluentNHibernate.Mapping;
using M242.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M242.Model.NHibernateImpl.Mappings
{
    public class TempLoggingMapping : ClassMap<TempLogging>
    {
        public TempLoggingMapping()
        {
            Id(x => x.Id).Unique();
            Map(x => x.CreateDate).Not.Nullable();
            Map(x => x.ModificationDate).Not.Nullable();

            Map(x => x.Humidity);
            Map(x => x.Temperature);
            Map(x => x.IotikitIp);
        }
    }
}
