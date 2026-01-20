using IT410Project.Interfaces;
using IT410Project.Models;

namespace IT410Project.Operations.SqlServer;

public class SleepOperations : IDataAccess<Sleep>
{
	public int Create(Sleep newItem)
	{
		throw new NotImplementedException();
	}

	public int DeleteItem(int id)
	{
		throw new NotImplementedException();
	}

	public Sleep[] GetAll()
	{
		throw new NotImplementedException();
	}

	public Sleep[] GetByDateRange(DateTime start, DateTime end)
	{
		throw new NotImplementedException();
	}

	public Sleep GetById(int id)
	{
		throw new NotImplementedException();
	}

	public int UpdateItem(int id, Sleep updatedItem)
	{
		throw new NotImplementedException();
	}
}