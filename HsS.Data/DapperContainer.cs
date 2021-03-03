using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace HsS.Data
{
    public interface IDapperContainer : IDisposable
    {
        IEnumerable<T> List<T>(string sql, DynamicObject parameters = null);
    }

    internal class DapperContainer : IDapperContainer
    {
        private readonly IDbConnection db;

        public DapperContainer(ApplicationDbContext db)
        {
            var connectionString = db.Database.GetDbConnection().ConnectionString;
            this.db = new SqlConnection(connectionString);
        }

        public IEnumerable<T> List<T>(string sql, DynamicObject parameters = null)
        {
            if (db.State == ConnectionState.Closed)
                db.Open();

            return db.Query<T>(sql, parameters);
        }

        public void Dispose()
        {
            if (db.State == ConnectionState.Open)
                db.Close();

            db.Dispose();
        }
    }
}
