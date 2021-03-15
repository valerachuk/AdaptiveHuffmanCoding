using Xunit;
using System.Collections.Generic;
using AdaptiveHuffman.Core.TreeNodes;
using AdaptiveHuffman.Core.TreeNodes.Interfaces;
using AdaptiveHuffman.UnitTests.Misc;
using System.Linq;
using AdaptiveHuffman.Core.DebugVisualizerTools;

namespace AdaptiveHuffman.UnitTests.Tree
{
  public class TreeSwapNodesByPathAndRebuildWeights
  {
    [Theory]
    [MemberData(nameof(TestData))]
    public void SwappingNodesInTree_ShouldWorksFine(string pathX, string pathY, IEnumerable<ITreeNode> expectedBypass)
    {
      // Arrange
      var tree = new HuffmanTree();

      var node110 = new NYTNode();
      var node111 = new LeafNode(0, 2);
      var node10 = new LeafNode(1, 1);
      var node11 = new InnerNode(2)
      {
        Left = node110,
        Right = node111
      };
      var node0 = new LeafNode(2, 2);
      var node1 = new InnerNode(3)
      {
        Left = node10,
        Right = node11
      };
      tree.Root = new InnerNode(5)
      {
        Left = node0,
        Right = node1
      };

      // Act
      tree.SwapNodesByPathAndRebuildWeights(pathX, pathY);

      // Assert
      var actualBypass = tree.SiblingPropertyBypass().Select(tuple => tuple.Item1);
      Assert.Equal(expectedBypass, actualBypass, new TreeNodeEqualityComparer());
    }

    public static IEnumerable<object[]> TestData => new List<object[]>
    {
      new object[] {
        "10", "111",
        new List<ITreeNode>{
          new NYTNode(),
          new LeafNode(1, 1),
          new LeafNode(0, 2),
          new InnerNode(1),
          new LeafNode(2, 2),
          new InnerNode(3),
          new InnerNode(5)
        }
      },
      new object[] {
        "0", "1",
        new List<ITreeNode>{
          new NYTNode(),
          new LeafNode(0, 2),
          new LeafNode(1, 1),
          new InnerNode(2),
          new InnerNode(3),
          new LeafNode(2, 2),
          new InnerNode(5)
        }
      },
      new object[] {
        "111", "10",
        new List<ITreeNode>{
          new NYTNode(),
          new LeafNode(1, 1),
          new LeafNode(0, 2),
          new InnerNode(1),
          new LeafNode(2, 2),
          new InnerNode(3),
          new InnerNode(5)
        }
      },
      new object[] {
        "1", "0",
        new List<ITreeNode>{
          new NYTNode(),
          new LeafNode(0, 2),
          new LeafNode(1, 1),
          new InnerNode(2),
          new InnerNode(3),
          new LeafNode(2, 2),
          new InnerNode(5)
        }
      },
      new object[] {
        "10", "11",
        new List<ITreeNode>{
          new NYTNode(),
          new LeafNode(0, 2),
          new InnerNode(2),
          new LeafNode(1, 1),
          new LeafNode(2, 2),
          new InnerNode(3),
          new InnerNode(5)
        }
      },
      new object[] {
        "110", "111",
        new List<ITreeNode>{
          new LeafNode(0, 2),
          new NYTNode(),
          new LeafNode(1, 1),
          new InnerNode(2),
          new LeafNode(2, 2),
          new InnerNode(3),
          new InnerNode(5)
        }
      },
      new object[] {
        "0", "111",
        new List<ITreeNode>{
          new NYTNode(),
          new LeafNode(2, 2),
          new LeafNode(1, 1),
          new InnerNode(2),
          new LeafNode(0, 2),
          new InnerNode(3),
          new InnerNode(5)
        }
      },
    };
  }

}
