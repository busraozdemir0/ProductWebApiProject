using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;

namespace WebApi.Application.Interfaces.Repositories
{
    public interface IReadRepository<T> where T: class, IEntityBase, new()
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,  // predicate => ornegin bir tabloda IsDeleted'i true olanlari getir
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include= null,  // Bu yapi ile ThenInclude kullanilabilir
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy=null,   // Siralama(orderby) kullanimi icin
            bool enableTracking=false);

        Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,   
            bool enableTracking = false, int currentPage=1, int pageSize=3);  // Sayfalama yapisi icin - Mevcut her sayfada sadece 3 veri olacak

        Task<T> GetAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool enableTracking = false);

        IQueryable<T> Find(Expression<Func<T,bool>> predicate, bool enableTracking = false);

        Task<int> CountAsync(Expression<Func<T, bool>>? predicate=null); // Bir entity'in sayisini bulmak icin
    }           
}
