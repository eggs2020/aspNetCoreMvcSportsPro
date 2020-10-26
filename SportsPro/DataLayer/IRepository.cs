using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.DataLayer
{
    public interface IRepository<T> where T : class
    {
        // Create an interface class and required methods

        List<T> List(QueryOptions<T> options);

        T Get(int id);
        T Get(QueryOptions<T> options);

        // Methods for CRUD operations to DB 
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();

    }
}
