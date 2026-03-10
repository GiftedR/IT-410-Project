using System.ComponentModel.DataAnnotations.Schema;

namespace IT410Project.Models;

// Long Project is a project that spans over many days
public class LongProject
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public string Desc { get; set; } = default!;
	public DateTime Deadline { get; set; }
	public double TotalWorkHours { get; set; }
	[NotMapped]
	public double CompletedWorkHours { get; set; }
	public ICollection<LongProjectDay> ProjectDays { get; set; } = [];
}