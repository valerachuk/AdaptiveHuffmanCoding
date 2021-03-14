using System.Collections.Generic;
using System.Linq;
using AdaptiveHuffman.Core.Tree.Interfaces;
using AdaptiveHuffman.Core.DebugVisualizerTools;

namespace AdaptiveHuffman.Core.Tree
{
  public class Tree : ITree
  {
    public ITreeNode Root { get; set; } = new NYTNode();

    public void AddItem(byte payload, string nytPath)
    {
      var pathQueue = new Queue<char>(nytPath.ToCharArray());

      var newLeaf = new LeafNode(payload);

      if (pathQueue.Count == 0)
      {
        var oldRoot = Root;
        Root = new InnerNode
        {
          Left = oldRoot,
          Right = newLeaf
        };

        return;
      }

      var currentNode = Root as InnerNode;
      while (pathQueue.Count > 1)
      {
        currentNode.Weight++;
        var pathCommand = pathQueue.Dequeue();
        currentNode = (pathCommand == '0' ? currentNode.Left : currentNode.Right) as InnerNode;
      }

      currentNode.Weight++;

      var lastPathCommand = pathQueue.Dequeue();
      if (lastPathCommand == '0')
      {
        var oldNyt = currentNode.Left;
        currentNode.Left = new InnerNode
        {
          Left = oldNyt,
          Right = newLeaf
        };
      }
      else
      {
        var oldNyt = currentNode.Right;
        currentNode.Right = new InnerNode
        {
          Left = oldNyt,
          Right = newLeaf
        };
      }

    }

    public void IncrementItem(string path, int value = 1)
    {
      var pathQueue = new Queue<char>(path.ToCharArray());

      var currentItem = Root as INotNYTTreeNode;

      while (pathQueue.Count > 0)
      {
        currentItem.Weight += value;
        currentItem = (pathQueue.Dequeue() == '0' ? (currentItem as InnerNode).Left : (currentItem as InnerNode).Right) as INotNYTTreeNode;
      }
      currentItem.Weight += value;
    }

    public (string, bool) FindLeafOrNYTByPayload(byte payload)
    {
      string leafPath = null;
      string nytPath = null;

      bool FindLeafOrNYT(string currentPath, ITreeNode currentNode)
      {
        switch (currentNode)
        {
          case NYTNode nyt:
            nytPath = currentPath;
            break;
          case LeafNode leaf when leaf.Payload == payload:
            leafPath = currentPath;
            return true;
          case InnerNode inner:
            var foundInRightChild = FindLeafOrNYT(currentPath + '1', inner.Right);
            return foundInRightChild ? foundInRightChild : FindLeafOrNYT(currentPath + '0', inner.Left);
        }
        return false;
      }

      FindLeafOrNYT("", Root);

      return leafPath != null ? (leafPath, true) : (nytPath, false);
    }

    public IEnumerable<(ITreeNode, string)> SiblingPropertyBypass()
    {
      var treeNodeLayers = new Dictionary<int, List<(ITreeNode, string)>>();

      void Bypass(ITreeNode node, string currentPath)
      {
        var inner = node as InnerNode;
        if (inner != null)
        {
          Bypass(inner.Left, currentPath + "0");
          Bypass(inner.Right, currentPath + "1");
        }

        var currentDepth = currentPath.Length;

        if (!treeNodeLayers.ContainsKey(currentDepth))
        {
          treeNodeLayers[currentDepth] = new();
        }

        var currentLayer = treeNodeLayers[currentDepth];
        currentLayer.Add((node, currentPath));
      }

      Bypass(Root, "");

      return treeNodeLayers
        .OrderByDescending(kvp => kvp.Key)
        .SelectMany(kvp => kvp.Value);
    }

    public static (string, string) FindFirstSiblingPropertyMissmatch(IEnumerable<(ITreeNode, string)> bypassedTreeNodes)
    {
      var bypassedTreeNodesArray = bypassedTreeNodes.ToArray();
      var sublingProperyComparer = new SiblingPropertyTreeNodeComparer();

      for (int i = 0; i < bypassedTreeNodesArray.Length - 1; i++)
      {
        var nodePathX = bypassedTreeNodesArray[i];
        var nodePathY = bypassedTreeNodesArray[i + 1];

        if (sublingProperyComparer.Compare(nodePathX.Item1, nodePathY.Item1) > 0)
        {
          return (nodePathX.Item2, nodePathY.Item2);
        }
      }

      return (null, null);
    }

    public void SwapNodesByPathAndRebuildWeights(string nodeXPath, string nodeYPath)
    {
      InnerNode FindByPath(string path)
      {
        InnerNode currentNode = Root as InnerNode;
        while (path != "")
        {
          var currentChildPath = path[0];
          path = path.Substring(1, path.Length - 1);
          currentNode = currentChildPath == '0' ? currentNode.Left as InnerNode : currentNode.Right as InnerNode;
        }

        return currentNode;
      }

      void SwapLeftAndRightClildAndRebuildWeights(string parentXPath, string parentYPath)
      {
        var parentX = FindByPath(parentXPath);
        var parentY = FindByPath(parentYPath);

        if (parentX != parentY)
        {
          var deltaWeightParentX = parentY.Right.Weight - parentX.Left.Weight;
          var deltaWeightParentY = parentX.Left.Weight - parentY.Right.Weight;

          IncrementItem(parentXPath, deltaWeightParentX);
          IncrementItem(parentYPath, deltaWeightParentY);
        }

        var tmp = parentX.Left;
        parentX.Left = parentY.Right;
        parentY.Right = tmp;

      }

      var parentXPath = nodeXPath.Substring(0, nodeXPath.Length - 1);
      var parentYPath = nodeYPath.Substring(0, nodeYPath.Length - 1);

      if (nodeXPath.Last() == '0')
      {
        SwapLeftAndRightClildAndRebuildWeights(parentXPath, parentYPath);
      }
      else
      {
        SwapLeftAndRightClildAndRebuildWeights(parentYPath, parentXPath);
      }

    }

    public void FixSiblingPropery()
    {
      while (true)
      {
        var bypass = SiblingPropertyBypass();
        var missmatch = FindFirstSiblingPropertyMissmatch(bypass);
        if (missmatch == (null, null))
          break;
        SwapNodesByPathAndRebuildWeights(missmatch.Item1, missmatch.Item2);
      }
    }

    public void AddItemAndFixSiblingProperty(byte payload, string nytPath)
    {
      AddItem(payload, nytPath);
      FixSiblingPropery();
    }

    public void IncrementItemAndFixSiblingProperty(string path)
    {
      IncrementItem(path);
      FixSiblingPropery();
    }
  }
}
