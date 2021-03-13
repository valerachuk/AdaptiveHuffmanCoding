using System.Collections.Generic;
using AdaptiveHuffman.Core.Tree.Interfaces;
using AdaptiveHuffman.Core.Tree;

namespace AdaptiveHuffman.Core
{
  public class SiblingPropertyTreeNodeComparer : IComparer<ITreeNode>
  {
    public int Compare(ITreeNode nodeX, ITreeNode nodeY)
    {
      var compareWeights = nodeX.Weight.CompareTo(nodeY.Weight);

      if (compareWeights != 0)
      {
        return compareWeights;
      }

      if (nodeX is InnerNode && nodeY is not InnerNode)
      {
        return 1;
      }
      if (nodeX is not InnerNode && nodeY is InnerNode)
      {
        return -1;
      }

      return 0;
    }
  }
}