namespace IT410Project.Models;

public class Project
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public string Desc { get; set; } = default!;
	public bool IsRepeating { get; set; }
}