namespace AdaptiveHuffman.Core.Tree.Interfaces
{
  public interface ITree
  {
    void AddItemAndFixSiblingProperty(byte payload, string nytPath);
    void IncrementItemAndFixSiblingProperty(string path);
    (string, bool) FindLeafOrNYTByPayload(byte payload);
  }
}