using IT410Project.Interfaces;
using IT410Project.Lib.EfCore.Context;
using IT410Project.Models;

namespace IT410Project.EfCore;

public class ProjectController : IDataAccess<Project>
{
	private readonly ProjectDbContext _context;

	public ProjectController(ProjectDbContext context)
	{
		_context = context;
	}

	public int Create(Project newItem)
	{
		throw new NotImplementedException();
	}

	public int DeleteItem(int id)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<Project> GetAll()
	{
		return _context.Projects.ToList();
	}

	public IEnumerable<Project> GetByDateRange(DateTime start, DateTime end)
	{
		return _context.Projects.ToList().Where((dt) => dt.StartTime >= start && dt.EndTime <= end);
	}

	public Project? GetById(int id)
	{
		return _context.Projects.Find(id);
	}

	public int UpdateItem(int id, Project updatedItem)
	{
		throw new NotImplementedException();
	}
}