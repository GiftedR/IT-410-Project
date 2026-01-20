using IT410Project.Interfaces;
using Microsoft.Data.Sqlite;

namespace IT410Project.Providers;

public class SqliteDBProvider : IDBRequirements
{
	public static readonly string ConnectionString = "Data Source=Data/Database.db";

	public static void CreateSampleSeedData()
	{
		using (SqliteConnection connection = new(ConnectionString))
		{
			SqliteCommand insertSeedData = new(
@"

;"
,connection);
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
	""StartTime""	DATE NOT NULL,
	""EndTime""	DATE NOT NULL,
	""Desc""	TEXT NOT NULL,
	""IsRepeating""	BIT NOT NULL,
	PRIMARY KEY(""Id"")
);

CREATE TABLE IF NOT EXISTS ""Sleep"" (
	""Id""	INTEGER,
	""Name""	TEXT NOT NULL,
	""StartTime""	DATE NOT NULL,
	""EndTime""	DATE NOT NULL,
	""Quality""	INTEGER NOT NULL,
	""RepeatDays""	INTEGER NOT NULL,
	PRIMARY KEY(""Id"")
);", 
			connection);
			
			createTables.ExecuteNonQuery();
		}
	}
}