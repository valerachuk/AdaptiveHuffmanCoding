using System;

namespace AdaptiveHuffman.Core.Interfaces
{
  public interface IHuffmanTree
  {
    void AddItemAndFixSiblingProperty(byte payload, string nytPath);
    void IncrementItemAndFixSiblingProperty(string path);
    (string, bool) FindLeafOrNYTByPayload(byte payload);
    (string, byte?) FindLeafOrNYTByGenerator(Func<string> getNext);
  }
}