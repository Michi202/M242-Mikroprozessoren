using M242.Model.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M242.Model.UnitOfwork
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        protected ISession Session { get; private set; }
        protected ITransaction Transaction { get; private set; }

        public bool TransactionIsRunning => Transaction != null && Transaction.IsActive;

        public NHibernateUnitOfWork(ISession session)
        {
            Session = session;
        }

        public void BeginTransaction()
        {
            Transaction = Session.BeginTransaction();
        }

        public void Close()
        {
            Session.Close();
        }


        public void Flush()
        {
            if (Session != null)
            {
                Session.Flush();
            }
        }

        public void Commit()
        {
            if (TransactionIsRunning)
            {
                Transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (TransactionIsRunning)
            {
                Transaction.Rollback();
            }
        }

        public void Dispose()
        {
            try
            {
                if (Transaction != null)
                {
                    Transaction.Dispose();
                }
            }
            catch (Exception)
            {
                //Logger.Debug("Failed to dispose transaction", ex);
                throw;
            }
            finally
            {
                try
                {
                    Session.Dispose();
                }
                catch (Exception)
                {
                    //Logger.Debug("Failed to dispose session", ex);

                    throw;
                }
            }
        }

        public void Save<T>(T obj) where T : Entity
        {
            Session.SaveOrUpdate(obj);
            Session.Flush();
        }

        public void SaveAll<T>(IList<T> list) where T : Entity
        {
            if (list == null)
            {
                return;
            }

            foreach (var obj in list)
            {
                Session.SaveOrUpdate(obj);
            }
            Session.Flush();

        }

        public void SaveChanges()
        {
            Session.Flush();
        }

        public bool Exists<T>(long id) where T : Entity
        {
            return Session.Get<T>(id) != null;
        }

        public T Get<T>(long id) where T : Entity
        {
            return Session.Get<T>(id);
        }
        public T SingleOrDefault<T>(long id) where T : Entity
        {
            return Session.Get<T>(id);
        }

        public IList<T> GetAll<T>() where T : Entity
        {
            return Session.QueryOver<T>().List();
        }

        public void Delete<T>(T t) where T : Entity
        {
            try
            {
                Session.Delete(t);
                Session.Flush();
            }
            catch (Exception) { }
        }

        public void Delete<T>(long id)
        {
            Session.Delete(Session.Load(typeof(T), id));
        }
    }
}
