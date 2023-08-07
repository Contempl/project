using System.Diagnostics;

namespace Taski
{
    internal class Program
    {
        private static readonly DateTime Now = DateTime.Now;

        private static async Task Main(string[] args)
        {
            if (args.Length < 1 || args.Length > 2)
            {
                Console.WriteLine("Wrong input, debil");
                return;
            }
            var resource = args[0];
            var defaultName = $"{args[0].Replace("https://", string.Empty)}_request_{Now}.log";
            var fileName = args.Length == 2 ? args[1] : defaultName;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using var client = new HttpClient();
                var output = await client.GetStringAsync(resource);
                await File.WriteAllTextAsync(fileName, output);
                Console.WriteLine("Status code: 200");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Electric. We are electric... and your request failed {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"Request took {stopwatch.ElapsedMilliseconds} ms");
            }
        }
    }
}