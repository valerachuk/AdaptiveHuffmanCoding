using Xunit;
using AdaptiveHuffman.Core.Tree.Interfaces;
using AdaptiveHuffman.Core.Tree;
using System.Collections.Generic;
using AdaptiveHuffman.Core;

namespace AdaptiveHuffman.UnitTests
{

  public class SiblingPropertyTreeNodeComparerTest
  {
    [Theory]
    [MemberData(nameof(TestData))]
    public void ComparingTreeNodesWithSiblingPropertyComparer_ShouldWorkFine(ITreeNode nodeX, ITreeNode nodeY, int expectedComparisonResult)
    {
      // Arrange
      var siblingPropertyTreeNodeComparer = new SiblingPropertyTreeNodeComparer();

      // Act
      var actualComparsionResult = siblingPropertyTreeNodeComparer.Compare(nodeX, nodeY);

      // Assert
      Assert.Equal(expectedComparisonResult, actualComparsionResult);
    }

    public static IEnumerable<object[]> TestData => new List<object[]>
    {
      new object[] { new NYTNode(), new InnerNode(1), -1 },
      new object[] { new LeafNode(55, 4), new NYTNode(), 1 },
      new object[] { new LeafNode(55, 4), new InnerNode(10), -1 },
      new object[] { new LeafNode(55, 4), new InnerNode(4), 1 },
      new object[] { new InnerNode(49), new InnerNode(4), 1 },
      new object[] { new InnerNode(7), new LeafNode(12, 7), -1 },
      new object[] { new InnerNode(7), new InnerNode(7), 0 },
      new object[] { new LeafNode(12, 6), new LeafNode(33, 6), 0 },
      new object[] { new NYTNode(), new NYTNode(), 0 }
    };

  }

}