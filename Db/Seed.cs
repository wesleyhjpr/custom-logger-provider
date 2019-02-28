using System.Data;
using System.Data.SQLite;
using System.IO;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CustomLoggerProvider.Db
{
    public class Seed
    {
        private static IDbConnection _dbConnection;

        public static void CreateDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
            var dbFilePath = configuration.GetValue<string>("DBInfo:DbFilePath");
            if (!File.Exists(dbFilePath))
            {
                _dbConnection = new SQLiteConnection(connectionString);
                _dbConnection.Open();

                // Create a Product table
                _dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS [EventLog] (
                        [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        [Category] NVARCHAR(512) NOT NULL,
                        [EventId] INTEGER NULL,
                        [LogLevel] NVARCHAR(32) NOT NULL,
                        [Message] NVARCHAR(1024) NOT NULL,
                        [CreatedTime] DATETIME NOT NULL
                    )");

                _dbConnection.Close();
            }
        }
    }
}