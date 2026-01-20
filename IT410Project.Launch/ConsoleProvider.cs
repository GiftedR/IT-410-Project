using IT410Project.Reporting;

internal class ConsoleProvider : IReporter
{
	public static void Write(string message)
	{
		Console.Write(message);
	}

	public static void WriteLine(string message)
	{
		Console.WriteLine(message);
	}
}