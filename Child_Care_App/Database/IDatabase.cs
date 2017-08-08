using System;
using System.Collections.Generic;

namespace Child_Care_App.Database
{
    public interface IDatabase<T> where T : new()
    {
        List<T> GetQuery(string qry, string param);
        List<T> GetQuery(string qry);

        int Insert(T item);
        int Delete(Guid primaryID);
    }
}
