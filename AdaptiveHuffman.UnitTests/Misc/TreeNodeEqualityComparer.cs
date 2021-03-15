using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AdaptiveHuffman.Core.TreeNodes.Interfaces;
using AdaptiveHuffman.Core.TreeNodes;

namespace AdaptiveHuffman.UnitTests.Misc
{
  public class TreeNodeEqualityComparer : IEqualityComparer<ITreeNode>
  {
    public bool Equals(ITreeNode x, ITreeNode y)
    {
      switch ((x, y))
      {
        case (InnerNode innerX, InnerNode innerY):
          return innerX.Weight == innerY.Weight;
        case (LeafNode leafX, LeafNode leafY):
          return leafX.Payload == leafY.Payload && leafX.Weight == leafY.Weight;
        case (NYTNode _, NYTNode _):
          return true;
        default:
          return false;
      }
    }

    public int GetHashCode([DisallowNull] ITreeNode obj) => obj.GetHashCode();
  }
}