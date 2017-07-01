using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Server;

namespace BuildADrink.Data
{
    public static class Extensions
    {
        public static IEnumerable<SqlDataRecord> ToIdTableParameter(this int[] ints)
        {
            var idList = new List<SqlDataRecord>();
            for (var i = 0; i < ints.Count(); i++)
            {
                var record = new SqlDataRecord();
                record.SetInt32(i, ints[i]);
                idList.Add(record);
            }
            return idList;
        }
    }
}