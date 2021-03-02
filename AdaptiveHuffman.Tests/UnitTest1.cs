using Xunit;
using AdaptiveHuffman.Core.Tree;
using AdaptiveHuffman.Tests.DebugVisualizerTools; // need for "Debug Visualizer"

namespace AdaptiveHuffman.Tests
{
  public class UnitTest1
  {
    // [Fact]
    [Fact(Skip = "Visualizator test")]
    public void Test1()
    {
      var tree = new Tree();

      var inner1 = new InnerNode
      {
        Weight = 4
      };

      tree.Root = inner1;

      var inner2 = new InnerNode
      {
        Weight = 8,
      };

      inner1.Left = inner2;

      inner1.Right = new LeafNode
      {
        Weight = 1,
        Payload = 123
      };

      inner2.Right = new LeafNode
      {
        Weight = 2,
        Payload = 11
      };

      inner2.Left = new NYTNode();

    }
  }
}
