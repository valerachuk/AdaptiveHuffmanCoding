using AdaptiveHuffman.Core.TreeNodes;
using AdaptiveHuffman.Core.TreeNodes.Interfaces;
using System.Text.Json;

namespace AdaptiveHuffman.Core.DebugVisualizerTools
{
  public static class TreeVisualizer
  {
    public static string VisualizeTree(this AdaptiveHuffman.Core.TreeNodes.HuffmanTree tree)
    {
      var graphJson = new GraphJSONData();
      var id = 0;

      int AddNode(ITreeNode node)
      {
        var nodeId = id++;
        switch (node)
        {
          case NYTNode nyt:
            graphJson.Nodes.Add(new GraphJSONData.NodeData(nodeId.ToString())
            {
              Color = "green",
              Shape = "box",
              Label = $"{nyt.Weight}\nNYT"
            });
            break;
          case LeafNode leaf:
            graphJson.Nodes.Add(new GraphJSONData.NodeData(nodeId.ToString())
            {
              Color = "orange",
              Shape = "box",
              Label = $"{leaf.Weight}\n[{leaf.Payload}]"
            });
            break;
          case InnerNode inner:
            graphJson.Nodes.Add(new GraphJSONData.NodeData(nodeId.ToString())
            {
              Label = inner.Weight.ToString()
            });

            if (inner.Left != null)
            {
              var leftId = AddNode(inner.Left);
              graphJson.Edges.Add(new GraphJSONData.EdgeData(nodeId.ToString(), leftId.ToString())
              {
                Label = "0"
              });
            }

            if (inner.Right != null)
            {
              var rightId = AddNode(inner.Right);
              graphJson.Edges.Add(new GraphJSONData.EdgeData(nodeId.ToString(), rightId.ToString())
              {
                Label = "1"
              });
            }
            break;
        }

        return nodeId;
      }

      AddNode(tree.Root);

      var options = new JsonSerializerOptions
      {
        IgnoreNullValues = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
      };

      return JsonSerializer.Serialize(graphJson, options);
    }

  }
}