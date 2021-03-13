using System.Collections.Generic;
using AdaptiveHuffman.Core.Tree.Interfaces;
using AdaptiveHuffman.Core.Tree;
using Xunit;

namespace AdaptiveHuffman.UnitTests
{
  public class TreeFindFirstSiblingPropertyMissmatchTest
  {

    [Theory]
    [MemberData(nameof(TestData))]
    public void GettingFirstSiblingPropertyMissmatch_ShouldWorkFine(IEnumerable<(ITreeNode, string)> nodeSequence, (string, string) expectedMissmath)
    {
      // Arrange

      // Act
      var actualMissmatch = Tree.FindFirstSiblingPropertyMissmatch(nodeSequence);

      // Assert
      Assert.Equal(expectedMissmath, actualMissmatch);
    }

    public static IEnumerable<object[]> TestData => new List<object[]>
    {
      new object[] {
        new List<(ITreeNode, string)>(),
        ((string)null, (string)null)
      },
      new object[] {
        new List<(ITreeNode, string)>{
          (new NYTNode(), "000"),
          (new LeafNode(0, 1), "001"),
          (new InnerNode(1), "00"),
          (new LeafNode(0, 1), "01"),
          (new InnerNode(2), "0"),
          (new LeafNode(0, 2), "1"),
          (new InnerNode(4), "")
        },
        ("00", "01")
      },
      new object[] {
        new List<(ITreeNode, string)>{
          (new NYTNode(), "110"),
          (new LeafNode(0, 2), "111"),
          (new LeafNode(0, 1), "10"),
          (new InnerNode(2), "11"),
          (new LeafNode(0, 2), "1"),
          (new InnerNode(3), "1"),
          (new InnerNode(5), ""),
        },
        ("111", "10")
      },
      new object[] {
        new List<(ITreeNode, string)>{
          (new NYTNode(), "110"),
          (new LeafNode(0, 1), "111"),
          (new LeafNode(0, 2), "10"),
          (new InnerNode(1), "11"),
          (new LeafNode(0, 2), "1"),
          (new InnerNode(3), "1"),
          (new InnerNode(5), ""),
        },
        ("10", "11")
      },
      new object[] {
        new List<(ITreeNode, string)>{
          (new NYTNode(), "100"),
          (new LeafNode(0, 1), "101"),
          (new InnerNode(1), "10"),
          (new LeafNode(0, 3), "11"),
          (new LeafNode(0, 2), "0"),
          (new InnerNode(4), "1"),
          (new InnerNode(1), ""),
        },
        ("11", "0")
      },
      new object[] {
        new List<(ITreeNode, string)>{
          (new NYTNode(), "100"),
          (new LeafNode(0, 1), "101"),
          (new InnerNode(1), "10"),
          (new LeafNode(0, 2), "11"),
          (new LeafNode(0, 3), "0"),
          (new InnerNode(4), "1"),
          (new InnerNode(5), ""),
        },
        ((string)null, (string)null)
      }
    };

  }
}