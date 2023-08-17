namespace Task;

public class CommandLineArgs
{
    public string FilePath { get; private set; }
    public string OutputMode { get; private set; }

    public CommandLineArgs(string[] args)
    {
        if (args.Length > 2)
            throw new ArgumentException();

        if (args.Length == 1)
        {
            throw new ArgumentException();
        }
        
        FilePath = args[0];
        OutputMode = args[1];
    }
}