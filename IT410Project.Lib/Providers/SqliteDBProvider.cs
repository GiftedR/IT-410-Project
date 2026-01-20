using IT410Project.Interfaces;

namespace IT410Project.Providers;

public class SqliteDBProvider : IDBRequirements
{
	public static void CreateSampleSeedData()
	{
		
	}

	public static void EnsureDatabaseRequirements()
	{
		if (!Directory.Exists("Data"))
			Directory.CreateDirectory("Data");
	}
}