using AdaptiveHuffman.Core.Tree.Interfaces;

namespace AdaptiveHuffman.Core.Tree
{
  public class LeafNode : INotNYTTreeNode
  {
    public int Weight { get; set; } = 1;
    public byte Payload { get; set; }

    public LeafNode()
    {

    }

    public LeafNode(byte payload)
    {
      Payload = payload;
    }

    public LeafNode(byte payload, int weight) : this(payload)
    {
      Weight = weight;
    }

  }
}