using System.Text;
using IT410Project.Interfaces;
using IT410Project.Models;
using Microsoft.Data.Sqlite;

namespace IT410Project.Providers;

public class SqliteDBProvider : IDBRequirements
{
	public static readonly string ConnectionString = "Data Source=Data/Database.db";

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
				Desc = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec malesuada arcu lacus, quis hendrerit nisi condimentum quis. Suspendisse pretium luctus metus eu feugiat. Mauris odio est, mattis id lectus vitae, vestibulum pulvinar augue. In non finibus sapien. Quisque facilisis augue sed libero fermentum consequat. Cras eros velit, tincidunt vitae magna eget, interdum aliquam tortor. Praesent vestibulum sem nec facilisis condimentum. Duis volutpat metus quis arcu ultricies, ornare ultricies elit gravida. Vivamus semper fringilla neque, eu mollis orci rutrum a. Aliquam erat volutpat.
Nam varius semper dui eget vehicula. Sed in neque ut nisi scelerisque vehicula sit amet non enim. Nullam bibendum ante est. In hac habitasse platea dictumst. Quisque ultricies lacinia urna at rhoncus. In facilisis libero a ligula pretium, quis mattis enim pretium. In id ipsum quis enim gravida efficitur. Vivamus sed ligula purus.

Cras dignissim a nulla et rutrum. Fusce consequat sit amet nunc et pretium. Mauris urna diam, commodo ut tincidunt vitae, condimentum sit amet est. Phasellus vestibulum, dui at tempor condimentum, augue erat sodales quam, sit amet dictum quam nisi vitae tortor. Mauris et egestas felis. Maecenas suscipit tortor massa, id dapibus tortor tempor nec. Mauris faucibus iaculis eros in pellentesque. Fusce malesuada molestie ipsum sit amet posuere. Aenean posuere, elit vel sollicitudin ornare, nibh eros sodales urna, quis ornare mi nibh quis eros. Maecenas et pharetra ipsum. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Cras enim tellus, aliquam sit amet turpis in, faucibus ultricies ligula.

Sed sollicitudin non lacus eget lobortis. Duis ut feugiat velit. Morbi varius est eu dui laoreet convallis. Donec lectus magna, mattis at nisi venenatis, condimentum commodo massa. Phasellus ut ipsum cursus, elementum lacus nec, pellentesque urna. Vivamus erat odio, dignissim non purus ut, facilisis vulputate sapien. Nulla aliquet magna arcu, ut fermentum sapien auctor eget. Aliquam ornare mi aliquam, dictum libero sollicitudin, varius odio. Nulla vitae quam at magna sagittis imperdiet. Aliquam vitae porttitor leo. Sed lacinia sed dolor et varius. Phasellus eleifend mattis nisi, eu gravida libero mattis vitae. Nunc blandit turpis tempor felis facilisis, ac mattis dui tincidunt. Maecenas finibus condimentum sagittis. Duis molestie arcu eget tortor dapibus, at accumsan purus consectetur. Suspendisse eu ex et lacus tincidunt efficitur sed ac diam.

Vivamus eget tincidunt nunc. Ut sagittis nec mi a faucibus. In sagittis risus sit amet enim faucibus placerat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Ut posuere rhoncus odio, eu vehicula velit mattis quis. Etiam hendrerit mauris odio, vitae elementum ipsum scelerisque quis. Donec convallis erat eget risus luctus, et mollis eros aliquam. Quisque venenatis bibendum velit vel facilisis. Maecenas non tincidunt quam.",
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

		// Console.WriteLine(sleepDataItems);
		// Console.WriteLine(projectDataItems);

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

	public static void EnsureDatabaseRequirements()
	{
		if (!Directory.Exists("Data"))
			Directory.CreateDirectory("Data");
		using (SqliteConnection connection = new(ConnectionString))
		{
			connection.Open();

			SqliteCommand createTables = new(
@"CREATE TABLE IF NOT EXISTS ""Projects"" (
	""Id""	INTEGER,
	""Name""	TEXT NOT NULL,
	""StartTime""	DATETIME NOT NULL,
	""EndTime""	DATETIME NOT NULL,
	""Desc""	TEXT NOT NULL,
	""IsRepeating""	BIT NOT NULL,
	PRIMARY KEY(""Id"")
);

CREATE TABLE IF NOT EXISTS ""Sleep"" (
	""Id""	INTEGER,
	""Name""	TEXT NOT NULL,
	""StartTime""	DATETIME NOT NULL,
	""EndTime""	DATETIME NOT NULL,
	""Quality""	INTEGER NOT NULL,
	""RepeatDays""	INTEGER NOT NULL,
	PRIMARY KEY(""Id"")
);", 
			connection);
			
			createTables.ExecuteNonQuery();
		}
	}
}