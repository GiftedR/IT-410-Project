namespace IT410Project.Models;

public class Sleep
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public int Quality { get; set; }
	public int RepeatDays { get; set; } // Storing as an int of bits, but will change to a weekday array using DateTime.Weekday.

	public override string ToString()
	{
		return $"{Id}: {Name} @ {StartTime} - {EndTime}; Q: {Quality}";
	}

	public static implicit operator Lib.EfCore.Entities.Sleep(Sleep sl) => new Lib.EfCore.Entities.Sleep
	{
		Id = (sl ?? default!).Id,
		Name = (sl ?? default!).Name,
		StartTime = (sl ?? default!).StartTime.ToString(),
		EndTime = (sl ?? default!).EndTime.ToString(),
		Quality = (sl ?? default!).Quality,
		RepeatDays = (sl ?? default!).RepeatDays
	};

	public static implicit operator Sleep?(Lib.EfCore.Entities.Sleep? sl) => new Sleep
	{
		Id = (sl ?? default!).Id,
		Name = (sl ?? default!).Name,
		StartTime = DateTime.Parse((sl ?? default!).StartTime),
		EndTime = DateTime.Parse((sl ?? default!).EndTime),
		Quality = (sl ?? default!).Quality,
		RepeatDays = (sl ?? default!).RepeatDays
	};
}