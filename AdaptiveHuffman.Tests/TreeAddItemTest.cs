using Xunit;
using AdaptiveHuffman.Core.Tree;
using AdaptiveHuffman.Tests.DebugVisualizerTools;
using AdaptiveHuffman.Tests.Misc;
using System;

namespace AdaptiveHuffman.Tests
{
  public class TreeAddItemTest
  {
    [Fact]
    public void AddingItemsToTree_ShouldAddItemsToTree()
    {
      // Arrange
      byte expectedPayload1 = 1,
      expectedPayload2 = 2,
      expectedPayload3 = 3;

      var tree = new Tree();

      // Act
      tree.AddItem(expectedPayload1, "");
      tree.AddItem(expectedPayload2, "0");
      tree.AddItem(expectedPayload3, "00");


      var currentNode = tree.Root;

      var actualPayload1 = TestUtils.GetPayloadOfRightLeaf(currentNode);
      currentNode = TestUtils.LeftStep(currentNode);

      var actualPayload2 = TestUtils.GetPayloadOfRightLeaf(currentNode);
      currentNode = TestUtils.LeftStep(currentNode);

      var actualPayload3 = TestUtils.GetPayloadOfRightLeaf(currentNode);

      // Assert
      Assert.Equal(expectedPayload1, actualPayload1);
      Assert.Equal(expectedPayload2, actualPayload2);
      Assert.Equal(expectedPayload3, actualPayload3);
    }

    [Fact]
    public void AddingItemsToTree_ShouldAddItemsToTreeAndSetCorrespondingWeights()
    {
      // Arrange
      byte expectedPayload1 = 11,
      expectedPayload2 = 12,
      expectedPayload3 = 13,
      expectedPayload4 = 14;

      var expectedWeight1 = 4;
      var expectedWeight2 = 3;
      var expectedWeight3 = 2;
      var expectedWeight4 = 1;

      var tree = new Tree();

      // Act
      tree.AddItem(expectedPayload1, "");
      tree.AddItem(expectedPayload2, "0");
      tree.AddItem(expectedPayload3, "00");
      tree.AddItem(expectedPayload4, "000");


      var currentNode = tree.Root;

      var actualWeight1 = currentNode.Weight;
      var actualPayload1 = TestUtils.GetPayloadOfRightLeaf(currentNode);
      currentNode = TestUtils.LeftStep(currentNode);

      var actualWeight2 = currentNode.Weight;
      var actualPayload2 = TestUtils.GetPayloadOfRightLeaf(currentNode);
      currentNode = TestUtils.LeftStep(currentNode);

      var actualWeight3 = currentNode.Weight;
      var actualPayload3 = TestUtils.GetPayloadOfRightLeaf(currentNode);
      currentNode = TestUtils.LeftStep(currentNode);

      var actualWeight4 = currentNode.Weight;
      var actualPayload4 = TestUtils.GetPayloadOfRightLeaf(currentNode);

      // Assert
      Assert.Equal(expectedPayload1, actualPayload1);
      Assert.Equal(expectedPayload2, actualPayload2);
      Assert.Equal(expectedPayload3, actualPayload3);
      Assert.Equal(expectedPayload4, actualPayload4);

      Assert.Equal(expectedWeight1, actualWeight1);
      Assert.Equal(expectedWeight2, actualWeight2);
      Assert.Equal(expectedWeight3, actualWeight3);
      Assert.Equal(expectedWeight4, actualWeight4);
    }

    [Fact]
    public void CreatingTree_ShouldBeInitializedWithNYTNode()
    {
      // Arrange
      var tree = new Tree();

      // Act
      var defaultRoot = tree.Root;

      // Assert
      Assert.NotNull(defaultRoot);
      Assert.IsType<NYTNode>(defaultRoot);
    }

    [Fact]
    public void AddingItemsToTreeWithInvalidPath_ShouldFail_1()
    {
      // Arrange
      var tree = new Tree();

      // Act
      // Assert
      Assert.Throws<NullReferenceException>(() => tree.AddItem(1, "0"));
    }

    [Fact]
    public void AddingItemsToTreeWithInvalidPath_ShouldFail_2()
    {
      // Arrange
      var tree = new Tree();

      // Act
      // Assert
      Assert.Throws<NullReferenceException>(() => tree.AddItem(1, "1"));
    }

    [Fact]
    public void AddingItemsToTreeWithInvalidPath_ShouldFail_3()
    {
      // Arrange
      var tree = new Tree();

      // Act
      tree.AddItem(3, "");
      tree.AddItem(4, "0");

      // Assert
      Assert.Throws<NullReferenceException>(() => tree.AddItem(1, "000"));
    }

    [Fact]
    public void AddingItemsToTreeWithInvalidPath_ShouldFail_4()
    {
      // Arrange
      var tree = new Tree();

      // Act
      tree.AddItem(3, "");
      tree.AddItem(4, "0");
      tree.AddItem(45, "00");

      // Assert
      Assert.Throws<NullReferenceException>(() => tree.AddItem(1, "0001"));
    }

  }
}
