namespace IT410Project.Models;

// This represents one day of the LongProject
public class LongProjectDay
{
	public int Id { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public string Notes { get; set; } = default!;
	public int LongProjectId { get; set; }
	public LongProject? LongProject { get; set; }
	public override string ToString()
	{
		return $"{Id}: {StartTime} - {EndTime} :: {Notes}";
	}
}