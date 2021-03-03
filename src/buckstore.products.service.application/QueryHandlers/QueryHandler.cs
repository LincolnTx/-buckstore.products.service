using System;
using System.Data;
using Npgsql;

namespace buckstore.products.service.application.QueryHandlers
{
    public abstract class QueryHandler
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("ConnectionString");

        internal IDbConnection DbConnection { get { return new NpgsqlConnection(_connectionString); } }

    }
}