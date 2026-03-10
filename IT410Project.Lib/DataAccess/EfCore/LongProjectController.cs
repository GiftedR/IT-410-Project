using IT410Project.Interfaces;
using IT410Project.Lib.EfCore.Context;
using IT410Project.Models;
using Microsoft.EntityFrameworkCore;

namespace IT410Project.EfCore;

public class LongProjectController : IDataAccess<LongProject>
{
	private readonly ProjectDbContext _context;

	public LongProjectController(ProjectDbContext context)
	{
		_context = context;
	}

	public int Create(LongProject newItem)
	{
		_context.LongProjects.Add(newItem);
		return _context.SaveChanges();
	}

	public int DeleteItem(int id)
	{
		LongProject? proj = GetById(id);
		if (proj is null)
			return 0;
		_context.LongProjects.Remove(proj);
		return _context.SaveChanges();
	}

	public IEnumerable<LongProject> GetAll()
	{
		return (IEnumerable<LongProject>)_context.LongProjects
			.Include(lp => lp.ProjectDays)
			.ToList();
	}

	public IEnumerable<LongProject> GetAllWithLimit(int limit)
	{
		return (IEnumerable<LongProject>)_context.LongProjects
			.Where(p => p.Id <= limit)
			.Include(lp => lp.ProjectDays)
			.ToList();
	}

	public IEnumerable<LongProject> GetByDateRange(DateTime start, DateTime end)
	{
		return (IEnumerable<LongProject>)_context.LongProjects
			.Where((dt) => dt.Deadline >= start && dt.Deadline <= end)
			.Include(lp => lp.ProjectDays)
			.ToList();
	}

	public LongProject? GetById(int id)
	{
		return _context.LongProjects
			.Where(lp => lp.Id == id)
			.Include(lp => lp.ProjectDays)
			.ToList().First();
	}

	public int UpdateItem(int id, LongProject updatedItem)
	{
		LongProject? proj = GetById(id);
		if (proj is null)
			return 0;
		if (proj.Id != updatedItem.Id)
			return 0;
		_context.Entry(updatedItem).State = EntityState.Modified;
		return _context.SaveChanges();
	}
}