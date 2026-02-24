namespace IT410Project.Models;

public class Project
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public string Desc { get; set; } = default!;
	public bool IsRepeating { get; set; }

	public override string ToString()
	{
		return $"{Id}: {Name} @ {StartTime} - {EndTime}; Q: {IsRepeating}";
	}

	public static implicit operator Lib.EfCore.Entities.Project(Project sl) => new Lib.EfCore.Entities.Project
	{
		Id = (sl ?? default!).Id,
		Name = (sl ?? default!).Name,
		StartTime = (sl ?? default!).StartTime.ToString(),
		EndTime = (sl ?? default!).EndTime.ToString(),
		Desc = (sl ?? default!).Desc,
		IsRepeating = (sl ?? default!).IsRepeating
	};

	public static implicit operator Project(Lib.EfCore.Entities.Project? sl) => new Project
	{
		Id = (sl ?? default!).Id,
		Name = (sl ?? default!).Name,
		StartTime = DateTime.Parse((sl ?? default!).StartTime),
		EndTime = DateTime.Parse((sl ?? default!).EndTime),
		Desc = (sl ?? default!).Desc,
		IsRepeating = (sl ?? default!).IsRepeating
	};
}