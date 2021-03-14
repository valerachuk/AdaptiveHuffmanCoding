using System.IO;
using AdaptiveHuffman.Core.Tree.Interfaces;

namespace AdaptiveHuffman.Core
{
  public static class Vitter
  {
    public static void Encode(Stream inputStream, Stream outputStream)
    {
      ITree tree = new Tree.Tree();
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
  }
}
