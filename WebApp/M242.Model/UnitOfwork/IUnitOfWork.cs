using M242.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M242.Model.UnitOfwork
{
    public interface IUnitOfWork : IDisposable
    {
        bool TransactionIsRunning { get; }

        void BeginTransaction();

        void Close();

        void Commit();
        void Rollback();
        void Flush();

        bool Exists<T>(long id) where T : Entity;

        T Get<T>(long id) where T : Entity;

        T SingleOrDefault<T>(long id) where T : Entity;

        IList<T> GetAll<T>() where T : Entity;

        void Save<T>(T obj) where T : Entity;


        void SaveAll<T>(IList<T> list) where T : Entity;

        void Delete<T>(T obj) where T : Entity;

        void Delete<T>(long id);
    }
}
