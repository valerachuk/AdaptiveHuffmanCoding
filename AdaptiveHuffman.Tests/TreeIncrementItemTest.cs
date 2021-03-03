using Xunit;
using AdaptiveHuffman.Core.Tree;
using AdaptiveHuffman.Tests.DebugVisualizerTools;
using System;

namespace AdaptiveHuffman.Tests
{
  public class TreeIncrementItemTest
  {
    [Fact]
    public void IncrementItem_ShouldIncrementItemsWeightAndItsAncestors()
    {
      // Arrange
      var expectedInnerWeight1 = 9;
      var expectedInnerWeight2 = 6;
      var expectedInnerWeight3 = 5;
      var expectedInnerWeight4 = 1;

      var expectedLeafWeight1 = 3;
      var expectedLeafWeight2 = 1;
      var expectedLeafWeight3 = 4;
      var expectedLeafWeight4 = 1;

      var tree = new Tree();

      // Act
      tree.AddItem(1, "");
      tree.AddItem(2, "0");
      tree.AddItem(3, "00");
      tree.AddItem(4, "000");

      tree.IncrementItem("1");
      tree.IncrementItem("1");

      tree.IncrementItem("001");
      tree.IncrementItem("001");
      tree.IncrementItem("001");


      var currentNode = tree.Root;
      var actualWeight1 = currentNode.Weight;
      var actualLeafWeight1 = TestUtils.GetWeightOfRightLeaf(currentNode);

      currentNode = TestUtils.LeftStep(currentNode);
      var actualWeight2 = currentNode.Weight;
      var actualLeafWeight2 = TestUtils.GetWeightOfRightLeaf(currentNode);

      currentNode = TestUtils.LeftStep(currentNode);
      var actualWeight3 = currentNode.Weight;
      var actualLeafWeight3 = TestUtils.GetWeightOfRightLeaf(currentNode);

      currentNode = TestUtils.LeftStep(currentNode);
      var actualWeight4 = currentNode.Weight;
      var actualLeafWeight4 = TestUtils.GetWeightOfRightLeaf(currentNode);

      // Assert
      Assert.Equal(expectedInnerWeight1, actualWeight1);
      Assert.Equal(expectedInnerWeight2, actualWeight2);
      Assert.Equal(expectedInnerWeight3, actualWeight3);
      Assert.Equal(expectedInnerWeight4, actualWeight4);

      Assert.Equal(expectedLeafWeight1, actualLeafWeight1);
      Assert.Equal(expectedLeafWeight2, actualLeafWeight2);
      Assert.Equal(expectedLeafWeight3, actualLeafWeight3);
      Assert.Equal(expectedLeafWeight4, actualLeafWeight4);
    }

    [Fact]
    public void IncrementItemWithInvalidPath_ShouldFail_1()
    {
      // Arrange
      var tree = new Tree();

      // Act
      // Assert
      Assert.Throws<NullReferenceException>(() => tree.IncrementItem(""));
    }

    [Fact]
    public void IncrementItemWithInvalidPath_ShouldFail_2()
    {
      // Arrange
      var tree = new Tree();

      // Act
      tree.AddItem(1, "");
      tree.AddItem(1, "0");

      // Assert
      Assert.Throws<NullReferenceException>(() => tree.IncrementItem("10"));
    }
  }
}
