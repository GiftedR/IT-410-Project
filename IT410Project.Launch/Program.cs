using System.Runtime.InteropServices;
using System.Text.Json;
using IT410Project.EfCore;
using IT410Project.Lib.EfCore.Context;
using efModels = IT410Project.Lib.EfCore.Entities;
using IT410Project.Models;
using IT410Project.Operations.Sqlite;
using IT410Project.Providers;
using Microsoft.EntityFrameworkCore;

namespace IT410Project.Launch;

internal class Program
{
	public static readonly string ConnectionString = $"Data Source={Directory.GetCurrentDirectory()}/Data/EFDatabase.db";

	public static async Task Main(string[] args)
	{
		bool useSqlite = true;

		{ // Moving Connection string to separate file
			string json_file = File.ReadAllText("Connection-String.json");
			Dictionary<string, string>? connections = JsonSerializer.Deserialize<Dictionary<string, string>>(json_file);

			if (connections != null)
			{
				if (m_HasArg("sqlserver", ref args))
				{
					if (!string.IsNullOrEmpty(connections["SqlServer"]))
					{
						SqlServerDBProvider.ConnectionString = connections["SqlServer"];
					}
					else
					{
						throw new Exception("No String Specified for SqlServer");
					}
				}

				if (!string.IsNullOrEmpty(connections["Sqlite"]))
				{
					SqliteDBProvider.ConnectionString = connections["Sqlite"];
				}
				else
				{
					throw new Exception("No String Specified for Sqlite");
				}
			}

		}

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
		// Console.WriteLine(Directory.GetCurrentDirectory());

		// ConsoleProvider.WriteLine($"Sql Server: {m_HasArg("sqlserver", ref args)}");
		// ConsoleProvider.WriteLine($"Use Sqlite {useSqlite}");
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

		ProjectDbContext pdb = new()
		{
			ConnectionString = ConnectionString
		};
		await pdb.Database.MigrateAsync();
		await pdb.DisposeAsync();
		
		ADOSample(itemLimit);
		EFCoreSample(itemLimit);

		ShootDatabase();
	}

	public static void ADOSample(int itemLimit = 10)
	{
		const int udpateSleepId = 1;
		const int deleteSleepId = 100;
		const int transaSleepId = 69;

		ConsoleProvider.WriteLine("\nADO Net Sample");
		ConsoleProvider.WriteLine($"Reading Sleep Operations with a limit of {itemLimit}");

		SleepOperations so = new();
		IEnumerable<Sleep> sleepItems = so.GetAllWithLimit(itemLimit);
		foreach (Sleep item in sleepItems)
		{
			ConsoleProvider.WriteLine(item.ToString());
		}

		ConsoleProvider.WriteLine($"Testing Updating with a new Sleep");
		so.UpdateItem(udpateSleepId, new Sleep{
			Name = "New Shweep",
			StartTime = DateTime.Now,
			EndTime = DateTime.Now.AddDays(7),
			Quality = 3,
			RepeatDays = 0
		});
		
		Sleep? updatedSleep = so.GetById(udpateSleepId);
		ConsoleProvider.WriteLine($"New first sleep: {(updatedSleep == null ? "No Sleep Found..." : updatedSleep)}");
		ConsoleProvider.WriteLine($"Testing Deleting a Sleep at index {deleteSleepId}");
		so.DeleteItem(deleteSleepId);
		Sleep? deletedSleep = so.GetById(deleteSleepId);
		ConsoleProvider.WriteLine($"Deleted Sleep: {(deletedSleep == null ? "No Sleep Found..." : deletedSleep)}");
		ConsoleProvider.WriteLine($"Testing Transaction with id {transaSleepId}");
		
		ProjectOperations po = new();
		
		Sleep? originalTransSleep = so.GetById(transaSleepId);
		Project? originalTransProje = po.GetById(transaSleepId);
		ConsoleProvider.WriteLine($"Original Sleep: {(originalTransSleep == null ? "No Sleep Found..." : originalTransSleep)}");
		ConsoleProvider.WriteLine($"Original Project: {(originalTransProje == null ? "No Project Found..." : originalTransProje)}");

		ConsoleProvider.WriteLine($"Deleting a Sleep and a Project at index {transaSleepId}");
		so.DeleteIdFromSleepAndProjects(transaSleepId);
		Sleep? deletedTransSleep = so.GetById(transaSleepId);
		Project? deletedTransProje = po.GetById(transaSleepId);
		ConsoleProvider.WriteLine($"Deleted Sleep: {(deletedTransSleep == null ? "No Sleep Found..." : deletedTransSleep)}");
		ConsoleProvider.WriteLine($"Deleted Project: {(deletedTransProje == null ? "No Project Found..." : deletedTransProje)}");
	}

	public static void EFCoreSample(int itemLimit = 10)
	{
		const int udpateSleepId = 5;
		const int deleteSleepId = 120;
		const int longProjectId = 356;

		ConsoleProvider.WriteLine("\nEF Core Sample");
		ConsoleProvider.WriteLine($"Reading Sleep Operations with a limit of {itemLimit}");
		ProjectDbContext pdb = new()
		{
			ConnectionString = ConnectionString
		};

		SleepController sc = new(pdb);
		LongProjectController lpc = new(pdb);

		IEnumerable<efModels.Sleep> sleepItems = sc.GetAllWithLimit(itemLimit);
		foreach (efModels.Sleep item in sleepItems)
		{
			ConsoleProvider.WriteLine(item.ToString());
		}

		ConsoleProvider.WriteLine($"Testing Updating with a new Sleep");
		efModels.Sleep? sleepToUpdate = sc.GetById(udpateSleepId);
		ConsoleProvider.WriteLine($"Sleep To Update: {(sleepToUpdate == null ? "No Sleep Found..." : sleepToUpdate)}");

		if (sleepToUpdate != null)
		{
			sleepToUpdate.Name = "New Shweep";
			sleepToUpdate.StartTime = DateTime.Now.ToString();
			sleepToUpdate.EndTime = DateTime.Now.AddDays(7).ToString();
			sleepToUpdate.Quality = 3;
			sleepToUpdate.RepeatDays = 0;
			sc.UpdateItem(udpateSleepId, sleepToUpdate);
		}
		
		efModels.Sleep? updatedSleep = sc.GetById(udpateSleepId);
		ConsoleProvider.WriteLine($"New first sleep: {(updatedSleep == null ? "No Sleep Found..." : updatedSleep)}");
		ConsoleProvider.WriteLine($"Testing Deleting a Sleep at index {deleteSleepId}");
		sc.DeleteItem(deleteSleepId);
		efModels.Sleep? deletedSleep = sc.GetById(deleteSleepId);
		ConsoleProvider.WriteLine($"Deleted Sleep: {(deletedSleep == null ? "No Sleep Found..." : deletedSleep)}");
	
		LongProject? lp = lpc.GetById(longProjectId);
		ConsoleProvider.WriteLine($"Long Project {longProjectId}: {(lp == null ? "No Long Project Found..." : lp) }");
	}

	private static bool m_HasArg(string arg, ref string[] args) => args.Contains(arg);

	private static void ShootDatabase()
	{
		if (File.Exists("Data/Database.db"))
			File.Delete("Data/Database.db");

		if (File.Exists("Data/EFDatabase.db"))
			File.Delete("Data/EFDatabase.db");

		if (File.Exists("Data/EFDatabase.db-shm"))
			File.Delete("Data/EFDatabase.db-shm");
		
		if (File.Exists("Data/EFDatabase.db-wal"))
			File.Delete("Data/EFDatabase.db-wal");
	}
}
