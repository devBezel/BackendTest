using System;
using System.Reflection;
using DbUp;

namespace PumoxRecruitmentTask.DAL.Migrations
{
    public class MigrationService
    {
        private readonly string _connectionString;

        public MigrationService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool UpdateDatabase()
        {
            var upgrader = DeployChanges.To
                .MySqlDatabase(_connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), Filter)
                .LogToConsole()
                .WithTransactionPerScript()
                .Build();

            if (upgrader.IsUpgradeRequired())
            {
                var scripts = upgrader.GetScriptsToExecute();
                Console.WriteLine("[DB] Database needs to update");
                Console.WriteLine($"[DB] Scripts: {scripts.Count}");

                var result = upgrader.PerformUpgrade();
                if (result.Successful)
                {
                    Console.WriteLine("[DB] Database updated without errors!");
                    return true;
                }

                Console.WriteLine(result.Error);
                return false;
            }

            Console.WriteLine("[DB] Database is already updated!");
            return true;
        }

        private static bool Filter(string script)
        {
            return script.StartsWith("PumoxRecruitmentTask.DAL.Migrations.Scripts");
        }

    }
}