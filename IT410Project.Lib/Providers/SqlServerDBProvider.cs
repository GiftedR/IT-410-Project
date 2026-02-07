using IT410Project.Interfaces;
using IT410Project.Models;

namespace IT410Project.Providers;

public class SqlServerDBProvider : IDBRequirements
{
	public static string ConnectionString { get; set; } = default!;
	
	public static void CreateSampleSeedData()
	{
		throw new Exception("SqlServer has not been implemented");
	}

	public static void EnsureDatabaseRequirements()
	{
		throw new Exception("SqlServer has not been implemented");
	}
}