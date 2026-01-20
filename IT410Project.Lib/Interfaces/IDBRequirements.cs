namespace IT410Project.Interfaces;

/// <summary>
/// Provides a platform to make sure that the database provider has all of its requirements.
/// </summary>
public interface IDBRequirements
{
	public abstract static void EnsureDatabaseRequirements();
	public abstract static void CreateSampleSeedData();
}