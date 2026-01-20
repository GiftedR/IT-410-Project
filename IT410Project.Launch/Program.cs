using IT410Project.Providers;

namespace IT410Project.Launch;

internal class Program
{
	public static void Main(string[] args)
	{
		bool useSqlite = false;

		if (m_HasArg("sqlserver", ref args))
		{
			try
			{
				Console.WriteLine("Attempting to use SqlServer, setting Sqlite as a fallback");
				SqlServerDBProvider.EnsureDatabaseRequirements();
				if (m_HasArg("seed", ref args))
				{
					Console.WriteLine("Using Seed Data");
					SqlServerDBProvider.CreateSampleSeedData();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error Occured: {e}\n\tFalling back to Sqlite...");
				useSqlite = true;
			}
		}
		else
			useSqlite = true;

		if (useSqlite)
		{
			SqliteDBProvider.EnsureDatabaseRequirements();
			if (m_HasArg("seed", ref args))
			{
				Console.WriteLine("Using Seed Data");
				SqliteDBProvider.CreateSampleSeedData();
			}
		}
	}

	private static bool m_HasArg(string arg, ref string[] args) => args.Contains(arg);
}
