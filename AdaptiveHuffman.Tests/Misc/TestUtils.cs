using AdaptiveHuffman.Core.Tree;
using AdaptiveHuffman.Core.Tree.Interfaces;

namespace AdaptiveHuffman.Tests.Misc
{
  public static class TestUtils
  {

    public static byte GetPayloadOfRightLeaf(ITreeNode node)
    {
      return ((node as InnerNode).Right as LeafNode).Payload;
    }

    public static int GetWeightOfRightLeaf(ITreeNode node)
    {
      return (node as InnerNode).Right.Weight;
    }

    public static ITreeNode LeftStep(ITreeNode node)
    {
      return (node as InnerNode).Left;
    }

  }
}
