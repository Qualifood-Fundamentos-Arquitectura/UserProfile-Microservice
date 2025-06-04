using DittoBox.API.Shared.Domain.Repositories;

namespace DittoBox.API.Shared.Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
