namespace DittoBox.API.Shared.Domain.Repositories {
	public interface IBaseRepository<T> where T : class {
		Task<T?> GetById(int id);
		Task<IEnumerable<T>> GetAll();
		Task Add(T entity);
		Task Update(T entity);
		Task Delete(T entity);

		Task<IQueryable<T>> GetAllSync();

	}
}