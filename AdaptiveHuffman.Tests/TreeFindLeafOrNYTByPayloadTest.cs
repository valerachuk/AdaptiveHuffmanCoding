using Xunit;
using AdaptiveHuffman.Core.Tree;
using AdaptiveHuffman.Tests.DebugVisualizerTools;
using System;

namespace AdaptiveHuffman.Tests
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
      var tree = new Tree();

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
