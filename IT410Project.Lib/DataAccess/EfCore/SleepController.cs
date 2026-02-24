using IT410Project.Interfaces;
using IT410Project.Lib.EfCore.Context;
using IT410Project.Lib.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

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
		_context.Sleeps.Add(newItem);
		return _context.SaveChanges();
	}

	public int DeleteItem(int id)
	{
		Sleep? honkShoo = GetById(id);
		if (honkShoo is null)
			return 0;
		_context.Sleeps.Remove(honkShoo);
		return _context.SaveChanges();
	}

	public IEnumerable<Sleep> GetAll()
	{
		return _context.Sleeps.ToList();
	}

	public IEnumerable<Sleep> GetAllWithLimit(int limit)
	{
		return _context.Sleeps.Where(p => p.Id <= limit).AsEnumerable();
	}

	public IEnumerable<Sleep> GetByDateRange(DateTime start, DateTime end)
	{
		return _context.Sleeps.ToList().Where((dt) => DateTime.Parse(dt.StartTime) >= start && DateTime.Parse(dt.EndTime) <= end);
	}

	public Sleep? GetById(int id)
	{
		return _context.Sleeps.Find(id);
	}

	public int UpdateItem(int id, Sleep updatedItem)
	{
		Sleep? honkShoo = GetById(id);
		if (honkShoo is null)
			throw new InvalidDataException("No Data Found with Id");
		if (honkShoo.Id != updatedItem.Id)
			throw new InvalidDataException("Ids do not match");
		_context.Entry(updatedItem).State = EntityState.Modified;
		return _context.SaveChanges();
	}
}