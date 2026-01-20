namespace IT410Project.Reporting;

public interface IReporter
{
	public abstract static void WriteLine(string message);
	public abstract static void Write(string message);
}