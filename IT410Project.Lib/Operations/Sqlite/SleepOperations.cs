using IT410Project.Interfaces;
using IT410Project.Models;
using IT410Project.Providers;
using Microsoft.Data.Sqlite;

namespace IT410Project.Operations.Sqlite;

public class SleepOperations : IDataAccess<Sleep>
{
	public int Create(Sleep newItem)
	{
		throw new NotImplementedException();
	}

	public int DeleteItem(int id)
	{
		throw new NotImplementedException();
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
		throw new NotImplementedException();
	}
}