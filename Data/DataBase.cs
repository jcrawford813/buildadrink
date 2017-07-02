using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BuildADrink.Data
{
    public abstract class DataBase : IDisposable
    {
        protected DataBase()
        {
            Connection = new SqlConnection();
        }

        protected readonly SqlConnection Connection;

        protected async Task<T> ExecuteAndMap<T>(Func<SqlCommand, Task<T>> processor)
        {
            using (var cmd = Connection.CreateCommand())
            {
                await Connection.OpenAsync();

                try
                {
                    return await processor(cmd);
                }
                finally
                {
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Connection?.Dispose();
            }
        }
    }
}