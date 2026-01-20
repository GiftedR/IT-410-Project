namespace IT410Project.Models;

public class Sleep
{
	public Guid Id { get; set; }
	public string Name { get; set; } = default!;
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public int Quality { get; set; }
	public int RepeatDays { get; set; } // Storing as an int of bits, but will change to a weekday array using DateTime.Weekday.
}