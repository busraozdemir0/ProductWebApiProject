using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Interfaces.Repositories;
using WebApi.Domain.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace WebApi.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IEntityBase, new()
    {
        private readonly DbContext dbContext;

        public ReadRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private DbSet<T> Table { get => dbContext.Set<T>(); }
        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table;

            // Ef core entity islemlerinde yapilan degisiklikleri izler. Biz read islemi yaptigimiz icin bu izlenme(tracking) olayini devre disi birakmaliyiz.
            //- Boylelikle daha performansli olur.
            if (!enableTracking)
                queryable = queryable.AsNoTracking(); 

            if (include is not null)
                queryable = include(queryable);

            if (predicate is not null)
                queryable = queryable.Where(predicate);

            if (orderBy is not null)
                return await orderBy(queryable).ToListAsync();

            return await queryable.ToListAsync();
        }

        public async Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
        {
            IQueryable<T> queryable = Table;

            if (!enableTracking)
                queryable = queryable.AsNoTracking();

            if (include is not null)
                queryable = include(queryable);

            if (predicate is not null)
                queryable = queryable.Where(predicate);

            if (orderBy is not null)
                return await orderBy(queryable).Skip((currentPage-1)*pageSize).Take(pageSize).ToListAsync(); // Skip ile ornegin ilk listelenen 3 veriden sonra ikinci sayfa için ilk 3 verinin atlanmasini sagliyoruz. Daha sonrasinda Take ile pageSize kadar (3) veriyi tekrar aliyoruz.

            return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table;

            if (!enableTracking)
                queryable = queryable.AsNoTracking();

            if (include is not null)
                queryable = include(queryable);

            //queryable.Where(predicate);

            return await queryable.FirstOrDefaultAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            Table.AsNoTracking();
            if (predicate is not null)
                Table.Where(predicate);
            
            return await Table.CountAsync();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking=false)
        {
            if (!enableTracking) // eger enableTracking false geliyorsa İzlenmeyi devre disi birakacagiz.
                Table.AsNoTracking();

            return Table.Where(predicate);
        }

        

        
    }
}
