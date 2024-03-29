namespace MyWebApiProject.Logging;

public class Logging : ILogging
{
    public void Log(string message, string type)
    {
        switch (type)
        {
            case "info":
                Console.WriteLine($"INFO: {message}");
                break;
            case "error":
                Console.WriteLine($"ERROR: {message}");
                break;
            default:
                Console.WriteLine($"UNKNOWN: {message}");
                break;
        }
    }
}