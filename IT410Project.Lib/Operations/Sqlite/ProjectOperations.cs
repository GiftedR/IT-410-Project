using IT410Project.Interfaces;
using IT410Project.Models;
using IT410Project.Providers;
using Microsoft.Data.Sqlite;

namespace IT410Project.Operations.Sqlite;

public class ProjectOperations : IDataAccess<Project>
{
	public int Create(Project newItem)
	{
		throw new NotImplementedException();
	}

	public int DeleteItem(int id)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<Project> GetAll()
	{
		List<Project> returnProjects = [];
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
						returnProjects.Add(new Project
						{
							Id = sleepReader.GetInt32(0),
							Name = sleepReader.GetString(1),
							StartTime = sleepReader.GetDateTime(2),
							EndTime = sleepReader.GetDateTime(3),
							Desc = sleepReader.GetString(4),
							IsRepeating = sleepReader.GetBoolean(5)
						});
					}
				}
			}
		}

		return returnProjects;
	}

	public IEnumerable<Project> GetByDateRange(DateTime start, DateTime end)
	{
		throw new NotImplementedException();
	}

	public Project? GetById(int id)
	{
		Project? returnProject = null;
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
						returnProject = new Project
						{
							Id = sleepReader.GetInt32(0),
							Name = sleepReader.GetString(1),
							StartTime = sleepReader.GetDateTime(2),
							EndTime = sleepReader.GetDateTime(3),
							Desc = sleepReader.GetString(4),
							IsRepeating = sleepReader.GetBoolean(5)
						};
					}
				}
			}
		}

		return returnProject;
	}

	public int UpdateItem(int id, Project updatedItem)
	{
		throw new NotImplementedException();
	}
}