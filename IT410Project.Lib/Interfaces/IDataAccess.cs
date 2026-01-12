namespace IT410Project;

public interface IDataAccess<T> where T : struct // Allows for nulls as a failsafe or Replace with an async system
{
	public T[] GetAll();
	public T GetById(int id);
	public int Create(T newItem);
	public int UpdateItem(int id, T updatedItem);
	public int DeleteItem(int id);
}