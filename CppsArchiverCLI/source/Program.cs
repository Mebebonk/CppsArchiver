namespace CppsArchiverCLI.source
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                while (true)
                {
                    switch (Console.ReadLine())
                    {
                        default:
                            return;
                        case "why":
                            return;
                    }
                }
            }
        }
    }
}
