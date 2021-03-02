namespace AdaptiveHuffman.Core.Tree
{
  public class LeafNode : ITreeNode
  {
    public int Weight { get; set; }
    public byte Payload { get; set; }
  }
}