using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using M242.Model.Model;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Dialect;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace M242.Model.NHibernateImpl
{
    public class SessionFactoryManager
    {
        public ISessionFactory SessionFactory { get; set; }

        private bool CreatedDatabase { get; set; }

        public SessionFactoryManager(string creationConnection, string migrationConnection, string connection, IConfiguration configuration)
        {
            //DeleteDatabase(creationConnection);
            CreateDatabase(creationConnection, connection);

            UpgradeDatabase(migrationConnection);

            GrantPrivileges(migrationConnection, connection);

            SessionFactory = CreateSessionFactory(connection);

            if (CreatedDatabase)
            {
                var session = SessionFactory.OpenSession();
                var UnitOfWork = new M242UnitofWork(session);
                UnitOfWork.BeginTransaction();

                UnitOfWork.Save(new User() { 
                    Password = "test",
                    Username = "test@tbz.ch",
                    NFCCardId = "12"
                });
                UnitOfWork.Save(new User()
                {
                    Password = "noah",
                    Username = "noah@tbz.ch",
                    NFCCardId = "12"
                });
                UnitOfWork.Commit();


                UnitOfWork.Close();
            }
        }

        public void CreateDatabase(string creationConnection, string connection)
        {
            using (var adminConn = new NpgsqlConnection(creationConnection))
            {
                adminConn.Open();
                using (var defaultConn = new NpgsqlConnection(connection))
                {
                    string userCmdText = @"SELECT 1 FROM pg_roles WHERE rolname='" + defaultConn.UserName + "'";
                    using (var userCmd = new NpgsqlCommand(userCmdText, adminConn))
                    {
                        if (userCmd.ExecuteScalar() == null)
                        {
                            var passwordIndex = defaultConn.ConnectionString.IndexOf("Password", StringComparison.OrdinalIgnoreCase);
                            var password = defaultConn.ConnectionString.Substring(passwordIndex + 8);
                            password = password.Trim().Trim('=').Trim();
                            password = password.Substring(0, password.IndexOf(";"));

                            var createUserStr = @"CREATE USER " + defaultConn.UserName +
                                                  @" LOGIN PASSWORD '" + password + @"'";

                            var mCreateuserCmd = new NpgsqlCommand(createUserStr, adminConn);

                            mCreateuserCmd.ExecuteNonQuery();
                        }
                    }
                    CreatedDatabase = false;
                    string dbCmdText = @"SELECT 1 FROM pg_database WHERE datname='" + defaultConn.Database + "'";
                    using (var cmd = new NpgsqlCommand(dbCmdText, adminConn))
                    {
                        if (cmd.ExecuteScalar() == null)
                        {
                            var createDbStr = @"CREATE DATABASE " + defaultConn.Database +
                                                @" WITH OWNER = " + defaultConn.UserName +
                                                @" ENCODING = 'UTF8' CONNECTION LIMIT = -1";

                            var mCreatedbCmd = new NpgsqlCommand(createDbStr, adminConn);
                            CreatedDatabase = true;
                            mCreatedbCmd.ExecuteNonQuery();
                        }
                    }
                }
                adminConn.Close();
            }
        }

        public void DeleteDatabase(string creationConnection)
        {
            using (var adminConn = new NpgsqlConnection(creationConnection))
            {
                adminConn.Open();
                using (var defaultConn = new NpgsqlConnection(creationConnection))
                {
                    var killSessionsCmd = new NpgsqlCommand("SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE pid <> pg_backend_pid() AND datname = '" + defaultConn.Database + "'", adminConn);
                    killSessionsCmd.ExecuteNonQuery();

                    var dropCmd = new NpgsqlCommand("DROP DATABASE IF EXISTS \"" + defaultConn.Database + "\"", adminConn);
                    dropCmd.ExecuteNonQuery();
                }
                adminConn.Close();
            }
        }

        public void UpgradeDatabase(string migrationConnection)
        {
            var announcer = new TextWriterAnnouncer(s =>
            {
                System.Diagnostics.Debug.WriteLine(s);
                //Console.WriteLine(s);
            });
            announcer.ShowSql = true;

            var assembly = Assembly.GetExecutingAssembly();

            var migrationContext = new RunnerContext(announcer);

            var options = new ProcessorOptions
            {
                PreviewOnly = false,
                Timeout = TimeSpan.FromMinutes(60)
            };

            var factory = new FluentMigrator.Runner.Processors.Postgres.PostgresProcessorFactory();

            using (var processor = factory.Create(migrationConnection, announcer, options))
            {
                var runner = new MigrationRunner(assembly, migrationContext, processor);
                //runner.MigrateDown(1, true);
                runner.MigrateUp(true);
            }
        }

        public void GrantPrivileges(string migrationConnection, string connection)
        {
            using (var adminConn = new NpgsqlConnection(migrationConnection))
            {
                adminConn.Open();
                using (var defaultConn = new NpgsqlConnection(connection))
                {
                    string userCmdText = $"GRANT ALL PRIVILEGES ON DATABASE {defaultConn.Database} TO {defaultConn.UserName}; " +
                                         $"GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO {defaultConn.UserName}; " +
                                         $"GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO  {defaultConn.UserName};";
                    using (var userCmd = new NpgsqlCommand(userCmdText, adminConn))
                    {
                        userCmd.ExecuteNonQuery();
                    }
                }
                adminConn.Close();
            }
        }

        public ISessionFactory CreateSessionFactory(string connectionString)
        {
            var factory = Fluently
                    .Configure()
                    .Database(
                            PostgreSQLConfiguration.PostgreSQL82
                            .ConnectionString(connectionString)
                            .Dialect<PostgreSQL81Dialect>()
                            // .ShowSql()
                            .DoNot.FormatSql()
                            .Raw("adonet.wrap_result_sets", "true")

                    // .ShowSql()
                    )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SessionFactoryManager>()
                        .Conventions.Add(Table.Is(x => x.TableName.ToLower()))
                        .Conventions.AddFromAssemblyOf<EnumConvention>()
                    )
                    .BuildConfiguration()
                    .BuildSessionFactory();

            return factory;
        }

        public class EnumConvention : IPropertyConvention, IPropertyConventionAcceptance
        {
            #region IPropertyConvention Members

            public void Apply(IPropertyInstance instance)
            {
                instance.CustomType(instance.Property.PropertyType);
            }

            #endregion

            #region IPropertyConventionAcceptance Members

            public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
            {
                criteria.Expect(x => x.Property.PropertyType.IsEnum ||
                (x.Property.PropertyType.IsGenericType &&
                 x.Property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                 x.Property.PropertyType.GetGenericArguments()[0].IsEnum)
                );
            }

            #endregion
        }
    }
}
