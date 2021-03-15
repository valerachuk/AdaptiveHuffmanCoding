namespace AdaptiveHuffman.Core.TreeNodes.Interfaces
{
  public interface INotNYTTreeNode : ITreeNode
  {
    new int Weight { get; set; }
  }
}