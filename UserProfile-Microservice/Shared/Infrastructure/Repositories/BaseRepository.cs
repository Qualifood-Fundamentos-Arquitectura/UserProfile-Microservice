using DittoBox.API.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DittoBox.API.Shared.Infrastructure.Repositories
{
    public abstract class BaseRepository<T>(
        ApplicationDbContext context
        ) : IBaseRepository<T> where T : class
    {
        public async Task Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public async Task Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public Task Update(T entity)
        {
            context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task<IQueryable<T>> GetAllSync()
        {
            return Task.FromResult((IQueryable<T>)context.Set<T>());
        }
    }
}