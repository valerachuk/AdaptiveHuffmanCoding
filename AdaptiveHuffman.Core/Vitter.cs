using System.IO;
using AdaptiveHuffman.Core.Interfaces;
using AdaptiveHuffman.Core.TreeNodes;
using AdaptiveHuffman.Core.DebugVisualizerTools;

namespace AdaptiveHuffman.Core
{
  public static class Vitter
  {
    public static void Compress(Stream inputStream, Stream outputStream)
    {
      IHuffmanTree tree = new HuffmanTree();
      var bitWriter = new BitWriter(outputStream);

      while (true)
      {
        var currentRead = inputStream.ReadByte();
        if (currentRead == -1)
          break;

        var currentByte = (byte)currentRead;

        var (path, isFound) = tree.FindLeafOrNYTByPayload(currentByte);
        bitWriter.WriteBitSequenseAsString(path);
        if (isFound)
        {
          tree.IncrementItemAndFixSiblingProperty(path);
        }
        else
        {
          bitWriter.WriteByte(currentByte);
          tree.AddItemAndFixSiblingProperty(currentByte, path);
        }
      }

      bitWriter.WriteTerminator();
    }

    public static void Decompress(Stream inputStream, Stream outputStream)
    {
      IHuffmanTree tree = new HuffmanTree();
      var bitReader = new BitReader(inputStream);

      while (!bitReader.IsEndOfStream)
      {
        var (path, payload) = tree.FindLeafOrNYTByGenerator(() => bitReader.ReadBit().ToString());
        if (payload == null)
        {
          var readByte = bitReader.ReadByte();
          tree.AddItemAndFixSiblingProperty(readByte, path);
          outputStream.WriteByte(readByte);
        }
        else
        {
          tree.IncrementItemAndFixSiblingProperty(path);
          outputStream.WriteByte((byte)payload);
        }
      }
    }
  }
}
