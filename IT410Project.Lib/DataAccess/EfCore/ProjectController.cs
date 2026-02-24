using IT410Project.Interfaces;
using IT410Project.Lib.EfCore.Context;
using IT410Project.Models;
using Microsoft.EntityFrameworkCore;

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
		_context.Projects.Add(newItem);
		return _context.SaveChanges();
	}

	public int DeleteItem(int id)
	{
		Project? proj = GetById(id);
		if (proj is null)
			return 0;
		_context.Projects.Remove(proj);
		return _context.SaveChanges();
	}

	public IEnumerable<Project> GetAll()
	{
		return (IEnumerable<Project>)_context.Projects.ToList();
	}

	public IEnumerable<Project> GetAllWithLimit(int limit)
	{
		return (IEnumerable<Project>)_context.Projects.Where(p => p.Id <= limit).ToList();
	}

	public IEnumerable<Project> GetByDateRange(DateTime start, DateTime end)
	{
		return (IEnumerable<Project>)_context.Projects.ToList().Where((dt) => DateTime.Parse(dt.StartTime) >= start && DateTime.Parse(dt.EndTime) <= end);
	}

	public Project? GetById(int id)
	{
		return _context.Projects.Find(id);
	}

	public int UpdateItem(int id, Project updatedItem)
	{
		Project? proj = GetById(id);
		if (proj is null)
			return 0;
		if (proj.Id != updatedItem.Id)
			return 0;
		_context.Entry(updatedItem).State = EntityState.Modified;
		return _context.SaveChanges();
	}
}