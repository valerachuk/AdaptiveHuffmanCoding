using AdaptiveHuffman.Core.Tree.Interfaces;

namespace AdaptiveHuffman.Core.Tree
{
  public class LeafNode : INotNYTTreeNode
  {
    public int Weight { get; set; } = 1;
    public byte Payload { get; set; }

    public LeafNode(byte payload)
    {
      Payload = payload;
    }
  }
}