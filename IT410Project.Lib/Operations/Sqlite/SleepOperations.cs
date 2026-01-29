using System.Runtime.InteropServices;
using IT410Project.Interfaces;
using IT410Project.Models;
using IT410Project.Providers;
using Microsoft.Data.Sqlite;

namespace IT410Project.Operations.Sqlite;

public class SleepOperations : IDataAccess<Sleep>
{
	public int Create(Sleep newItem)
	{
		int result = -1;

		using (SqliteConnection connection = new(SqliteDBProvider.ConnectionString))
		{
			connection.Open();
			
			using (SqliteCommand createNewSleep = new(@"
				INSERT INTO ""Sleep""
				(""Name"", ""StartTime"", ""EndTime"", ""Quality"", ""RepeatDays"")
				VALUES
				(""@Name"", ""@StartTime"", ""@EndTime"", ""@Quality"", ""@RepeatDays"")
			", connection))
			{
				createNewSleep.Parameters.AddWithValue("@Name", newItem.Name);
				createNewSleep.Parameters.AddWithValue("@StartTime", newItem.StartTime);
				createNewSleep.Parameters.AddWithValue("@EndTime", newItem.EndTime);
				createNewSleep.Parameters.AddWithValue("@Quality", newItem.Quality);
				createNewSleep.Parameters.AddWithValue("@RepeatDays", newItem.RepeatDays);


				result = createNewSleep.ExecuteNonQuery();
			}
		}

		return result;
	}

	public int DeleteItem(int id)
	{
		int result = -1;

		using (SqliteConnection connection = new(SqliteDBProvider.ConnectionString))
		{
			connection.Open();
			using (SqliteCommand deleteSleep = new(@"
				DELETE FROM ""Sleep""
				WHERE @Id = Id
			", connection))
			{
				deleteSleep.Parameters.AddWithValue("@Id", id);

				result = deleteSleep.ExecuteNonQuery();
			}
		}

		return result;
	}

	public IEnumerable<Sleep> GetAll()
	{
		List<Sleep> returnSleeps = [];
		using (SqliteConnection connection = new(SqliteDBProvider.ConnectionString))
		{
			connection.Open();

			using (SqliteCommand getAllCommand = new(@"
SELECT Id, Name, StartTime, EndTime, Quality, RepeatDays FROM ""Sleep"";", connection))
			{
				using (SqliteDataReader sleepReader = getAllCommand.ExecuteReader())
				{
					while (sleepReader.Read())
					{
						returnSleeps.Add(new Sleep
						{
							Id = sleepReader.GetInt32(0),
							Name = sleepReader.GetString(1),
							StartTime = sleepReader.GetDateTime(2),
							EndTime = sleepReader.GetDateTime(3),
							Quality = sleepReader.GetInt32(4),
							RepeatDays = sleepReader.GetInt32(5)
						});
					}
				}
			}
		}

		return returnSleeps;
	}
	
	public IEnumerable<Sleep> GetAllWithLimit(int limit)
	{
		List<Sleep> returnSleeps = [];
		using (SqliteConnection connection = new(SqliteDBProvider.ConnectionString))
		{
			connection.Open();

			using (SqliteCommand getAllCommand = new(@"
SELECT Id, Name, StartTime, EndTime, Quality, RepeatDays FROM ""Sleep"" LIMIT @Limit;", connection))
			{
				getAllCommand.Parameters.AddWithValue("@Limit", limit);
				using (SqliteDataReader sleepReader = getAllCommand.ExecuteReader())
				{
					while (sleepReader.Read())
					{
						returnSleeps.Add(new Sleep
						{
							Id = sleepReader.GetInt32(0),
							Name = sleepReader.GetString(1),
							StartTime = sleepReader.GetDateTime(2),
							EndTime = sleepReader.GetDateTime(3),
							Quality = sleepReader.GetInt32(4),
							RepeatDays = sleepReader.GetInt32(5)
						});
					}
				}
			}
		}

		return returnSleeps;
	}

	public IEnumerable<Sleep> GetByDateRange(DateTime start, DateTime end)
	{
		throw new NotImplementedException();
	}

	public Sleep? GetById(int id)
	{
		Sleep? returnSleep = null;
		using (SqliteConnection connection = new(SqliteDBProvider.ConnectionString))
		{
			connection.Open();

			using (SqliteCommand getOneCommand = new(@"
SELECT Id, Name, StartTime, EndTime, Quality, RepeatDays FROM ""Sleep"" WHERE Id = @Id;", connection))
			{
				getOneCommand.Parameters.AddWithValue("@Id", id);

				using (SqliteDataReader sleepReader = getOneCommand.ExecuteReader())
				{
					while (sleepReader.Read())
					{
						returnSleep = new Sleep
						{
							Id = sleepReader.GetInt32(0),
							Name = sleepReader.GetString(1),
							StartTime = sleepReader.GetDateTime(2),
							EndTime = sleepReader.GetDateTime(3),
							Quality = sleepReader.GetInt32(4),
							RepeatDays = sleepReader.GetInt32(5)
						};
					}
				}
			}
		}

		return returnSleep;
	}

	public int UpdateItem(int id, Sleep updatedItem)
	{
		int result = -1;

		using (SqliteConnection connection = new(SqliteDBProvider.ConnectionString))
		{
			connection.Open();
			using (SqliteCommand updateSleep = new(@"
				UPDATE ""Sleep""
				SET ""Name"" = @Name,
					""StartTime"" = @StartTime,
					""EndTime"" = @EndTime,
					""Quality"" = @Quality,
					""RepeatDays"" = @RepeatDays
				WHERE ""Id"" = @Id
			", connection))
			{
				updateSleep.Parameters.AddWithValue("@Name", updatedItem.Name);
				updateSleep.Parameters.AddWithValue("@StartTime", updatedItem.StartTime);
				updateSleep.Parameters.AddWithValue("@EndTime", updatedItem.EndTime);
				updateSleep.Parameters.AddWithValue("@Quality", updatedItem.Quality);
				updateSleep.Parameters.AddWithValue("@RepeatDays", updatedItem.RepeatDays);
				updateSleep.Parameters.AddWithValue("@Id", id);

				result = updateSleep.ExecuteNonQuery();
			}
		}

		return result;
	}

	public int DeleteIdFromSleepAndProjects(int id)
	{
		int returnCode = 0;

		using (SqliteConnection connection = new(SqliteDBProvider.ConnectionString))
		{
			connection.Open();
			SqliteTransaction transAct = connection.BeginTransaction();

			try
			{
				SqliteCommand deleteSleep = new SqliteCommand(
					@"DELETE FROM ""Sleep""
					WHERE ""Id"" = @Id;", connection, transAct);
				
				deleteSleep.Parameters.AddWithValue("@Id", id);
				
				deleteSleep.ExecuteNonQuery();

				SqliteCommand deleteProject = new SqliteCommand(
					@"DELETE FROM ""Sleep""
					WHERE ""Id"" = @Id;", connection, transAct);

				deleteProject.Parameters.AddWithValue("@Id", id);

				deleteProject.ExecuteNonQuery();

				transAct.Commit();
			}
			catch
			{
				transAct.Rollback();
				throw;
			}
		}

		return returnCode;
	}
}