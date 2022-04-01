using M242.Model.NHibernateImpl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M242.Model.Util
{
    public static class NHibernateServiceProvider
    {
        public static void AddNHibernate(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var creationConnection = configuration.GetConnectionString("CreationConnection");
            var migrationConnection = configuration.GetConnectionString("MigrationConnection");
            var connection = configuration.GetConnectionString("Connection");

            var singleSessionFactoryManager = new SessionFactoryManager(creationConnection, migrationConnection, connection, configuration);

            serviceCollection.AddSingleton(singleSessionFactoryManager);

            serviceCollection.AddScoped<IM242UnitofWork>(sp =>
            {
                var session = sp.GetRequiredService<SessionFactoryManager>().SessionFactory.OpenSession();
                return new M242UnitofWork(session);
            });
        }
    }
}
