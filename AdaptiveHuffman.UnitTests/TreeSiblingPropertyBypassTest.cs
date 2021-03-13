using System.Linq;
using Xunit;
using AdaptiveHuffman.Core.Tree;
using AdaptiveHuffman.Core.Tree.Interfaces;
using AdaptiveHuffman.UnitTests.Misc;
using AdaptiveHuffman.Core.DebugVisualizerTools;

namespace AdaptiveHuffman.UnitTests
{
  public class TreeSiblingPropertyBypassTest
  {
    [Fact]
    public void SiblingPropertyBypass_ShouldBeCorrect_1()
    {
      // Arrange
      var tree = new Tree();
      var expectedBypassSequence = new (ITreeNode, string)[]
      {
        (new NYTNode(), "0000"),
        (new LeafNode(4, 1), "0001"),
        (new InnerNode(1), "000"),
        (new LeafNode(3, 1), "001"),
        (new InnerNode(2), "00"),
        (new LeafNode(2, 1), "01"),
        (new InnerNode(3), "0"),
        (new LeafNode(1, 1), "1"),
        (new InnerNode(4), "")
      };

      var expectedNodeSequence = expectedBypassSequence.Select(tuple => tuple.Item1);
      var expectedNodePathSequence = expectedBypassSequence.Select(tuple => tuple.Item2);

      tree.AddItem(1, "");
      tree.AddItem(2, "0");
      tree.AddItem(3, "00");
      tree.AddItem(4, "000");

      // Act
      var actualBypassSequence = tree.SiblingPropertyBypass();

      // Assert
      var actualNodeSequence = actualBypassSequence.Select(tuple => tuple.Item1);
      var actualNodePathSequence = actualBypassSequence.Select(tuple => tuple.Item2);

      Assert.Equal(expectedNodeSequence, actualNodeSequence, new TreeNodeEqualityComparer());
      Assert.Equal(expectedNodePathSequence, actualNodePathSequence);
    }

    [Fact]
    public void SiblingPropertyBypass_ShouldBeCorrect_2()
    {
      // Arrange
      var tree = new Tree();
      var expectedBypassSequence = new (ITreeNode, string)[]
      {
        (new LeafNode(4, 1), "1010"),
        (new LeafNode(6, 1), "1011"),
        (new LeafNode(3, 1), "010"),
        (new LeafNode(5, 1), "011"),
        (new LeafNode(1, 1), "100"),
        (new InnerNode(1), "101"),
        (new NYTNode(), "00"),
        (new InnerNode(1), "01"),
        (new InnerNode(2), "10"),
        (new LeafNode(2, 1), "11"),
        (new InnerNode(2), "0"),
        (new InnerNode(3), "1"),
        (new InnerNode(6), "")
      };

      var expectedNodeSequence = expectedBypassSequence.Select(tuple => tuple.Item1);
      var expectedNodePathSequence = expectedBypassSequence.Select(tuple => tuple.Item2);

      tree.AddItem(1, "");
      tree.AddItem(2, "1");
      tree.AddItem(3, "0");
      tree.AddItem(4, "10");
      tree.AddItem(5, "01");
      tree.AddItem(6, "101");

      // Act
      var actualBypassSequence = tree.SiblingPropertyBypass();

      // Assert
      var actualNodeSequence = actualBypassSequence.Select(tuple => tuple.Item1);
      var actualNodePathSequence = actualBypassSequence.Select(tuple => tuple.Item2);

      Assert.Equal(expectedNodeSequence, actualNodeSequence, new TreeNodeEqualityComparer());
      Assert.Equal(expectedNodePathSequence, actualNodePathSequence);
    }

  }
}
