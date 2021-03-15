using AdaptiveHuffman.Core.TreeNodes.Interfaces;

namespace AdaptiveHuffman.Core.TreeNodes
{
  public class InnerNode : INotNYTTreeNode
  {
    public int Weight { get; set; } = 1;
    public ITreeNode Left { get; set; }
    public ITreeNode Right { get; set; }

    public InnerNode()
    {

    }

    public InnerNode(int weight)
    {
      Weight = weight;
    }
  }
}