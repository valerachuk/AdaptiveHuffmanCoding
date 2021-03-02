namespace AdaptiveHuffman.Core.Tree
{
  public class InnerNode : ITreeNode
  {
    public int Weight { get; set; }
    public ITreeNode Left { get; set; }
    public ITreeNode Right { get; set; }
  }
}