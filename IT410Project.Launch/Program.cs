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

		ConsoleProvider.WriteLine($"Testing Create with a new Sleep");
		so.Create(new Sleep
		{
			Name = "New Shweep",
			StartTime = DateTime.Now,
			EndTime = DateTime.Now.AddDays(7),
			Quality = 3,
			RepeatDays = 0
		});
	}

	private static bool m_HasArg(string arg, ref string[] args) => args.Contains(arg);
}
