using CliFx;
using System.Threading.Tasks;

namespace AdaptiveHuffman.CLI
{
  public static class Program
  {
    public static async Task<int> Main() =>
        await new CliApplicationBuilder()
            .AddCommandsFromThisAssembly()
            .Build()
            .RunAsync();
  }
}
