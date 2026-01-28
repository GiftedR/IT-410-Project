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
}