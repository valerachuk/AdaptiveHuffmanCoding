using AdaptiveHuffman.Core.Tree.Interfaces;
namespace AdaptiveHuffman.Core.Tree
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