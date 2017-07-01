using System;
using System.Data.SqlClient;

namespace BuildADrink.Data
{
    public abstract class DataBase : IDisposable
    {
        protected DataBase()
        {
            Connection = new SqlConnection();
        }

        protected readonly SqlConnection Connection;

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