namespace AdaptiveHuffman.Core.Tree.Interfaces
{
  public interface INotNYTTreeNode : ITreeNode
  {
    new int Weight { get; set; }
  }
}