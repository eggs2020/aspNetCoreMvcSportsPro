using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SportsPro.DataLayer
{
    // Class that implements IRepository interface class
    public class Repository<T> : IRepository<T> where T : class
    {
        protected SportsProContext context { get; set; }
        private DbSet<T> dbset { get; set; }

        public Repository(SportsProContext ctx)
        {
            context = ctx;
            dbset = context.Set<T>();
        }

        //Method to return a list of items matching a query
        public virtual List<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
                foreach (var clause in options.WhereClauses)
                {

                    query = query.Where(clause);

                }
            if (options.HasOrderBy)
                query = query.OrderBy(options.OrderBy);
            return query.ToList();
        }

        //Method to find one item matching an id
        public virtual T Get(int id) => dbset.Find(id);

        //Method to find one item matching given query options
        public virtual T Get(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
            {
                foreach (var clause in options.WhereClauses)
                {
                    query = query.Where(clause);
                }
            }
            return query.FirstOrDefault();
        }

        //CRUD operations for the DB
        public virtual void Insert(T entity) => dbset.Add(entity);
        public virtual void Update(T entity) => dbset.Update(entity);
        public virtual void Delete(T entity) => dbset.Remove(entity);
        public virtual void Save() => context.SaveChanges();
    }
}
