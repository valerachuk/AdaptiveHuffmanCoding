using Xunit;
using AdaptiveHuffman.Core.TreeNodes;
using AdaptiveHuffman.Core.DebugVisualizerTools;
using System;

namespace AdaptiveHuffman.UnitTests.Tree
{
  public class TreeFindLeafOrNYTByPayloadTest
  {
    [Theory]
    [InlineData(1, "1", true)]
    [InlineData(2, "01", true)]
    [InlineData(3, "001", true)]
    [InlineData(12, "000", false)]
    [InlineData(15, "000", false)]
    public void SearchingForExistingLeaf_ShouldReturnIt(byte payloadToFind, string expectedPath, bool expectedFoundResult)
    {
      // Arrange
      var tree = new HuffmanTree();

      // Act
      tree.AddItem(1, "");
      tree.AddItem(2, "0");
      tree.AddItem(3, "00");

      var (path, isFound) = tree.FindLeafOrNYTByPayload(payloadToFind);

      // Assert
      Assert.Equal(expectedPath, path);
      Assert.Equal(expectedFoundResult, isFound);
    }

  }
}
