using System.Runtime.InteropServices;
using System.Text.Json;
using IT410Project.EfCore;
using IT410Project.Lib.EfCore.Context;
using efModels = IT410Project.Lib.EfCore.Entities;
using IT410Project.Models;
using IT410Project.Operations.Sqlite;
using IT410Project.Providers;

namespace IT410Project.Launch;

internal class Program
{
	public static void Main(string[] args)
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
		
		ADOSample(itemLimit);
		EFCoreSample(itemLimit);

		// ShootDatabase();
	}

	public static void ADOSample(int itemLimit = 10)
	{
		ConsoleProvider.WriteLine("ADO Net Sample");
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
	}

	public static void EFCoreSample(int itemLimit = 10)
	{
		ConsoleProvider.WriteLine("EF Core Sample");
		ConsoleProvider.WriteLine($"Reading Sleep Operations with a limit of {itemLimit}");
		ProjectDbContext pdb = new();

		SleepController sc = new(pdb);
		IEnumerable<efModels.Sleep> sleepItems = sc.GetAllWithLimit(itemLimit);
		foreach (efModels.Sleep item in sleepItems)
		{
			ConsoleProvider.WriteLine(item.ToString());
		}

		ConsoleProvider.WriteLine($"Testing Updating with a new Sleep");
		sc.UpdateItem(1, new efModels.Sleep{
			Name = "New Shweep",
			StartTime = DateTime.Now.ToString(),
			EndTime = DateTime.Now.AddDays(7).ToString(),
			Quality = 3,
			RepeatDays = 0
		});
		
		efModels.Sleep? updatedSleep = sc.GetById(1);
		ConsoleProvider.WriteLine($"New first sleep: {(updatedSleep == null ? "No Sleep Found..." : updatedSleep)}");
		ConsoleProvider.WriteLine($"Testing Deleting a Sleep at index 100");
		sc.DeleteItem(100);
		efModels.Sleep? deletedSleep = sc.GetById(100);
		ConsoleProvider.WriteLine($"Deleted Sleep: {(deletedSleep == null ? "No Sleep Found..." : deletedSleep)}");
		ConsoleProvider.WriteLine($"Testing Transaction with id 69");
		
		ProjectController pc = new(pdb);
		
		efModels.Sleep? originalTransSleep = sc.GetById(69);
		efModels.Project? originalTransProje = pc.GetById(69);
		ConsoleProvider.WriteLine($"Original Sleep: {(originalTransSleep == null ? "No Sleep Found..." : originalTransSleep)}");
		ConsoleProvider.WriteLine($"Original Project: {(originalTransProje == null ? "No Project Found..." : originalTransProje)}");
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
