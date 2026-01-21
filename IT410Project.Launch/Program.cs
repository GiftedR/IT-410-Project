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
		ConsoleProvider.WriteLine($"Has Seed: {m_HasArg("seed", ref args)}");

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

		ConsoleProvider.WriteLine("Reading Sleep Operations");

		SleepOperations so = new();
		ConsoleProvider.WriteLine(so.GetAll().ToString());
	}

	private static bool m_HasArg(string arg, ref string[] args) => args.Contains(arg);
}
