using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsPro.DataLayer
{
    // Class for various query clause options to be used in other parts of the program
    public class QueryOptions<T>
    {
        // Order By clause for a query
        public Expression<Func<T, Object>> OrderBy { get; set; }
        
        // Where clause for a query
        public Expression<Func<T, bool>> Where
        {
            set
            {
                if (WhereClauses == null)
                {
                    WhereClauses = new WhereClauses<T>();
                }
                WhereClauses.Add(value);
            }
        }

        private string[] includes;

        public string Includes
        {
            set => includes = value.Replace(" ", "").Split(',');
        }

        public string[] GetIncludes() => includes ?? new string[0];

        public bool HasWhere => WhereClauses != null;
        public bool HasOrderBy => OrderBy != null;

        public WhereClauses<T> WhereClauses { get; set; }
    }

    public class WhereClauses<T> : List<Expression<Func<T, bool>>> { }
}
