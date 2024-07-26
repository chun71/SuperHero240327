﻿
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Repositories
{
    public class Repository
    {
        public static string connectionString
        {
            get
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(baseDir, "sqlconnectionString.txt");
                string connectionString = File.ReadAllText(filePath);

                return connectionString;
            }
        }

        public async Task CreateTable(string tableName)
        {
            string createTableSql = @$"
                        USE [SuperHero]
                        IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = '{tableName}')
                        BEGIN
                            CREATE TABLE [{tableName}] (
                                    [CharacterID]       BIGINT NOT NULL,
		                            [Name]                  NVARCHAR(50) NOT NULL,
		                            [FirstName]         NVARCHAR(50) NOT NULL,
		                            [LastName]          NVARCHAR(50) NOT NULL, 
		                            [Place]                     NVARCHAR(50),
		                            [Action]                NVARCHAR(5) NOT NULL,
		                            [CreateTime]        DATETIME PRIMARY KEY
                            );
                        END
                            ";

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            await dbConnection.ExecuteAsync(createTableSql);
        }
    }
}
