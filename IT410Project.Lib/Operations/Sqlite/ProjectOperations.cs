using IT410Project.Interfaces;
using IT410Project.Models;

namespace IT410Project.Operations.Sqlite;

public class ProjectOperations : IDataAccess<Project>
{
	public int Create(Project newItem)
	{
		throw new NotImplementedException();
	}

	public int DeleteItem(int id)
	{
		throw new NotImplementedException();
	}

	public Project[] GetAll()
	{
		throw new NotImplementedException();
	}

	public Project[] GetByDateRange(DateTime start, DateTime end)
	{
		throw new NotImplementedException();
	}

	public Project GetById(int id)
	{
		throw new NotImplementedException();
	}

	public int UpdateItem(int id, Project updatedItem)
	{
		throw new NotImplementedException();
	}
}