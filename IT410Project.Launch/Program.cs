using IT410Project.Models;
using IT410Project.Operations.Sqlite;
using IT410Project.Providers;

namespace IT410Project.Launch;

internal class Program
{
	public static void Main(string[] args)
	{
		bool useSqlite = true;

		if (m_HasArg("sqlserver", ref args))
		{
			useSqlite = false;
			try
			{
				ConsoleProvider.WriteLine("Attempting to use SqlServer, setting Sqlite as a fallback");
				SqlServerDBProvider.EnsureDatabaseRequirements();
				if (m_HasArg("seed", ref args))
				{
					ConsoleProvider.WriteLine("Using Seed Data");
					SqlServerDBProvider.CreateSampleSeedData();
				}
			}
			catch (Exception e)
			{
				ConsoleProvider.WriteLine($"Error Occured: {e}\n\tFalling back to Sqlite...");
				useSqlite = true;
			}
		}
		else
		{
			useSqlite = true;
		}
		Console.WriteLine(Directory.GetCurrentDirectory());

		ConsoleProvider.WriteLine($"Sql Server: {m_HasArg("sqlserver", ref args)}");
		ConsoleProvider.WriteLine($"Use Sqlite {useSqlite}");
		// ConsoleProvider.WriteLine($"Has Seed: {m_HasArg("seed", ref args)}");

		if (useSqlite)
		{
			ConsoleProvider.WriteLine("Launching Sqlite Provider");
			SqliteDBProvider.EnsureDatabaseRequirements();
			if (m_HasArg("seed", ref args))
			{
				ConsoleProvider.WriteLine("Using Seed Data");
				SqliteDBProvider.CreateSampleSeedData();
			}
		}

		const int itemLimit = 10;

		ConsoleProvider.WriteLine($"Reading Sleep Operations with a limit of {itemLimit}");

		SleepOperations so = new();
		IEnumerable<Sleep> sleepItems = so.GetAllWithLimit(itemLimit);
		foreach (Sleep item in sleepItems)
		{
			ConsoleProvider.WriteLine(item.ToString());
		}

		ConsoleProvider.WriteLine($"Testing Updating with a new Sleep");
		so.UpdateItem(1, new Sleep{
			Name = "New Shweep",
			StartTime = DateTime.Now,
			EndTime = DateTime.Now.AddDays(7),
			Quality = 3,
			RepeatDays = 0
		});
		
		Sleep? updatedSleep = so.GetById(1);
		ConsoleProvider.WriteLine($"New first sleep: {(updatedSleep == null ? "No Sleep Found..." : updatedSleep)}");
		ConsoleProvider.WriteLine($"Testing Deleting a Sleep at index 100");
		so.DeleteItem(100);
		Sleep? deletedSleep = so.GetById(100);
		ConsoleProvider.WriteLine($"Deleted Sleep: {(deletedSleep == null ? "No Sleep Found..." : deletedSleep)}");
		ConsoleProvider.WriteLine($"Testing Transaction with id 69");
		
		ProjectOperations po = new();
		
		Sleep? originalTransSleep = so.GetById(69);
		Project? originalTransProje = po.GetById(69);
		ConsoleProvider.WriteLine($"Original Sleep: {(originalTransSleep == null ? "No Sleep Found..." : originalTransSleep)}");
		ConsoleProvider.WriteLine($"Original Project: {(originalTransProje == null ? "No Project Found..." : originalTransProje)}");

		ConsoleProvider.WriteLine($"Deleting a Sleep and a Project at index 69");
		so.DeleteIdFromSleepAndProjects(69);
		Sleep? deletedTransSleep = so.GetById(69);
		Project? deletedTransProje = po.GetById(69);
		ConsoleProvider.WriteLine($"Deleted Sleep: {(deletedTransSleep == null ? "No Sleep Found..." : deletedTransSleep)}");
		ConsoleProvider.WriteLine($"Deleted Project: {(deletedTransProje == null ? "No Project Found..." : deletedTransProje)}");
		
		ShootDatabase();
	}

	private static bool m_HasArg(string arg, ref string[] args) => args.Contains(arg);

	private static void ShootDatabase()
	{
		if (File.Exists("Data/Database.db"))
		{
			File.Delete("Data/Database.db");
		}
	}
}
