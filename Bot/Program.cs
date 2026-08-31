namespace Bot
{
    internal class Program
    {
        // This is the entry point to the program.
        static void Main(string[] args) => EntryPoint.Launch(args).GetAwaiter().GetResult();
    }
}
