﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using log4net;
using log4net.Config;
using System.Reflection;

namespace IntroSE.Kanban.Backend.DataLayer
{
    internal abstract class DalController
    {
        protected readonly string _connectionString;
        protected readonly string _tableName;
        protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal DalController(string tableName)
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = tableName;

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        internal virtual List<DTO> Select()
        {
            List<DTO> results = new List<DTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_tableName}";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObject(dataReader));
                    }
                }
                catch (Exception e)
                {
                    log.Error($"Select on table {_tableName} failed, tried command: {command.CommandText},\n" +
                        $" the SQLite exception massage was: {e.Message}");
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }

        internal virtual void DeleteAll()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"DELETE FROM {_tableName}";
                
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error($"DeleteAll on table {_tableName} failed, tried command: {command.CommandText},\n" +
                        $" the SQLite exception massage was: {e.Message}");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }

        protected abstract DTO ConvertReaderToObject(SQLiteDataReader reader);
    }
}