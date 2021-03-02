using System.Collections.Generic;

namespace AdaptiveHuffman.Tests.DebugVisualizerTools
{

  public class GraphJSONData
  {
    public object Kind => new { Graph = true };

    public IList<NodeData> Nodes { get; set; } = new List<NodeData>();

    public IList<EdgeData> Edges { get; set; } = new List<EdgeData>();

    public class NodeData
    {
      public NodeData(string id)
      {
        Id = id;
      }

      public string Id { get; set; }
      public string Label { get; set; }
      public string Color { get; set; }
      public string Shape { get; set; }
    }

    public class EdgeData
    {
      public EdgeData(string from, string to)
      {
        From = from;
        To = to;
      }

      public string From { get; set; }
      public string To { get; set; }
      public string Label { get; set; }
      public string Id { get; set; }
      public string Color { get; set; }
      public bool? Dashes { get; set; }
    }
  }
}
