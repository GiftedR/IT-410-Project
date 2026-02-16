namespace IT410Project.Interfaces;

public interface IDataAccess<T> where T : class // Allows for nulls as a failsafe or Replace with an async system
{
	public IEnumerable<T> GetAll();
	public IEnumerable<T> GetByDateRange(DateTime start, DateTime end);
	public T? GetById(int id);
	public int Create(T newItem);
	public int UpdateItem(int id, T updatedItem);
	public int DeleteItem(int id);
}