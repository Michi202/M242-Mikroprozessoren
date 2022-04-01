using M242.Model.UnitOfwork;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M242.Model
{
    public class M242UnitofWork : NHibernateUnitOfWork, IM242UnitofWork
    {
        public M242UnitofWork(ISession session) : base(session) { }
    }
}
