using CliFx;
using CliFx.Attributes;
using System.Threading.Tasks;
using System.IO;
using AdaptiveHuffman.Core;

namespace AdaptiveHuffman.CLI.Commands
{
  [Command("decompress")]
  public class DecompressCommand : ICommand
  {
    [CommandParameter(0, Name = "inputFile")]
    public FileInfo InputFile { get; set; }

    [CommandParameter(1, Name = "outputFile")]
    public FileInfo OutputFile { get; set; }

    public ValueTask ExecuteAsync(IConsole console)
    {
      using var readFileStream = InputFile.OpenRead();
      using var writeFileStream = OutputFile.OpenWrite();

      Vitter.Decompress(readFileStream, writeFileStream);

      console.Output.WriteLine("Done!");

      return default;
    }
  }
}
