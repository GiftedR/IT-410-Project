using IT410Project.Interfaces;
using IT410Project.Lib.EfCore.Context;
using IT410Project.Models;

namespace IT410Project.EfCore;

public class SleepController : IDataAccess<Sleep>
{
	private readonly ProjectDbContext _context;

	public SleepController(ProjectDbContext context)
	{
		_context = context;
	}

	public int Create(Sleep newItem)
	{
		throw new NotImplementedException();
	}

	public int DeleteItem(int id)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<Sleep> GetAll()
	{
		return _context.Sleeps.ToList();
	}

	public IEnumerable<Sleep> GetByDateRange(DateTime start, DateTime end)
	{
		return _context.Sleeps.ToList().Where((dt) => dt.StartTime >= start && dt.EndTime <= end);
	}

	public Sleep? GetById(int id)
	{
		return _context.Sleeps.Find(id);
	}

	public int UpdateItem(int id, Sleep updatedItem)
	{
		throw new NotImplementedException();
	}
}