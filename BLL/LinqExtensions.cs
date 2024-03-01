using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Summary description for LinqExtensions
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Perform custom paging using LINQ to SQL
        /// </summary>
        /// <typeparam name="T">Type of the Datasource to be paged</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj">Object to be paged through</param>
        /// <param name="page">Page Number to fetch</param>
        /// <param name="pageSize">Number of rows per page</param>
        /// <param name="keySelector">Sorting Expression</param>
        /// <param name="asc">Sort ascending if true. Otherwise descending</param>
        /// <returns>Page of result from the paged object</returns> 
        public static IQueryable<T> Page<T, TResult>(this IQueryable<T> obj, int page, int pageSize, System.Linq.Expressions.Expression<Func<T, TResult>> keySelector, bool asc)
        {
            if (asc)
                return obj.OrderBy(keySelector).Skip((page - 1) * pageSize).Take(pageSize).AsQueryable();
            else
                return obj.OrderByDescending(keySelector).Skip((page - 1) * pageSize).Take(pageSize).AsQueryable();
        }






        public static IQueryable<T> Page<T, TResult>(this IQueryable<T> obj, int page, int pageSize, System.Linq.Expressions.Expression<Func<T, TResult>> keySelector, bool asc, out int rowsCount)
        {
            rowsCount = obj.Count();
            int innerRows = rowsCount - (page * pageSize);
            if (innerRows < 0)
            {
                innerRows = 0;
            }
            if (asc)
                return obj.OrderByDescending(keySelector).Take(innerRows).OrderBy(keySelector).Take(pageSize).AsQueryable();
            else
                return obj.OrderBy(keySelector).Take(innerRows).OrderByDescending(keySelector).Take(pageSize).AsQueryable();
        }
    }
}
