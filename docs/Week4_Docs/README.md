# Week 4

__Response:__

For this week I didn't really change anything. I had pre-optimized the method. When building the seed data, instead of running an insert query for each item being inserted. Instead I built a single query that runs. I used string builder to build the one query. Since it inserts well over 2000 items currently (There is a bug that causes it to insert 20000 items), the query count goes from 4000 to 2.

__Code:__
```cs
public static void CreateSampleSeedData()
{
	Random random = new Random();
	StringBuilder sleepDataItems = new();
	StringBuilder projectDataItems = new();
	List<Sleep> sleepDataList = [];
	List<Project> projectDataList = [];

	for(int idx = 0; idx < 2048; idx++)
	{
		sleepDataList.Add(new Sleep {
			Name = "Placeholder Name...",
			StartTime = DateTime.Now.AddDays(idx),
			EndTime = DateTime.Now.AddDays(idx).AddHours(8),
			Quality = random.Next(0, 10),
			RepeatDays = random.Next(0b1, 0b0010000)
		});
		projectDataList.Add(new Project
		{
			Name = "Placeholder Name...",
			StartTime = DateTime.Now.AddDays(idx),
			EndTime = DateTime.Now.AddDays(idx).AddHours(8),
			Desc = @"Description",
			IsRepeating = random.Next(0, 1) == 0 ? false : true
			});
	}

	foreach (Sleep honkShoo in sleepDataList)
	{
		sleepDataItems.Append("(\"");
		sleepDataItems.Append(honkShoo.Name);
		sleepDataItems.Append("\",\"");
		sleepDataItems.Append(honkShoo.StartTime);
		sleepDataItems.Append("\",\"");
		sleepDataItems.Append(honkShoo.EndTime);
		sleepDataItems.Append("\",");
		sleepDataItems.Append(honkShoo.Quality);
		sleepDataItems.Append(",");
		sleepDataItems.Append(honkShoo.RepeatDays);
		sleepDataItems.Append("),");
	}

	foreach (Project work in projectDataList)
	{
		projectDataItems.Append("(\"");
		projectDataItems.Append(work.Name);
		projectDataItems.Append("\",\"");
		projectDataItems.Append(work.StartTime);
		projectDataItems.Append("\",\"");
		projectDataItems.Append(work.EndTime);
		projectDataItems.Append("\",\"");
		projectDataItems.Append(work.Desc);
		projectDataItems.Append("\",");
		projectDataItems.Append(work.IsRepeating);
		projectDataItems.Append("),");
	}

	sleepDataItems.Remove(sleepDataItems.Length - 1, 1);
	projectDataItems.Remove(projectDataItems.Length - 1, 1);

	using (SqliteConnection connection = new(ConnectionString))
	{
		connection.Open();

		SqliteCommand insertSleepData = new(
			$"INSERT INTO \"Sleep\"(\"Name\",\"StartTime\",\"EndTime\",\"Quality\",\"RepeatDays\")values{sleepDataItems};",connection);

		insertSleepData.ExecuteNonQuery();

		SqliteCommand insertProjectData = new(
			$"INSERT INTO \"Projects\"(\"Name\",\"StartTime\",\"EndTime\",\"Desc\",\"IsRepeating\")values{projectDataItems};",connection);

		insertProjectData.ExecuteNonQuery();
	}
}
```

[Code can be found here](/IT410Project.Lib/Providers/SqliteDBProvider.cs)