using System.Data;
using System.Data.SqlClient;
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
            //if (!File.Exists(dbFilePath))
            //{
            //    _dbConnection = new SqlConnection(connectionString);
            //    _dbConnection.Open();

            //    // Create a Product table
            //    _dbConnection.Execute(@"
            //            CREATE TABLE [EventLog] (
            //                [Id] int IDENTITY(1,1) PRIMARY KEY,
            //                [Category] VARCHAR(512) NOT NULL,
            //                [EventId] INT NULL,
            //                [LogLevel] VARCHAR(32) NOT NULL,
            //                [Message] VARCHAR(1024) NOT NULL,
            //                [CreatedTime] DATETIME NOT NULL
            //            )");

            //    _dbConnection.Close();
            //}
        }
    }
}