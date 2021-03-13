using Xunit;
using System.Collections.Generic;
using AdaptiveHuffman.Core.Tree;
using AdaptiveHuffman.Core.Tree.Interfaces;
using AdaptiveHuffman.UnitTests.Misc;
using System.Linq;
using AdaptiveHuffman.UnitTests.DebugVisualizerTools;

namespace AdaptiveHuffman.UnitTests
{
  public class TreeFixSiblingProperyTest
  {
    [Fact]
    public void FixingInvalidSiblingProperty_ShouldFixinvalidSiblingProperty()
    {
      // Arrange
      var tree = new Tree();

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

      var expectedFixedSiblingPropertyBypass = new List<ITreeNode>
      {
        new NYTNode(),
        new LeafNode(1, 1),
        new InnerNode(1),
        new LeafNode(0, 2),
        new LeafNode(2, 2),
        new InnerNode(3),
        new InnerNode(5)
      };

      // Act
      tree.FixSiblingPropery();

      // Assert
      var actualBypass = tree.SiblingPropertyBypass().Select(tuple => tuple.Item1);
      Assert.Equal(expectedFixedSiblingPropertyBypass, actualBypass, new TreeNodeEqualityComparer());
    }

    [Fact]
    public void FixingValidSiblingProperty_ShouldNotModifyTree()
    {
      // Arrange
      var tree = new Tree();

      var node100 = new NYTNode();
      var node101 = new LeafNode(1, 1);
      var node10 = new InnerNode(1)
      {
        Left = node100,
        Right = node101
      };
      var node11 = new LeafNode(0, 2);
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

      var expectedFixedSiblingPropertyBypass = new List<ITreeNode>
      {
        new NYTNode(),
        new LeafNode(1, 1),
        new InnerNode(1),
        new LeafNode(0, 2),
        new LeafNode(2, 2),
        new InnerNode(3),
        new InnerNode(5)
      };

      // Act
      tree.FixSiblingPropery();

      // Assert
      var actualBypass = tree.SiblingPropertyBypass().Select(tuple => tuple.Item1);
      Assert.Equal(expectedFixedSiblingPropertyBypass, actualBypass, new TreeNodeEqualityComparer());
    }
  }
}