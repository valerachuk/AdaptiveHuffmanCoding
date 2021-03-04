using System.Collections.Generic;
using AdaptiveHuffman.Core.Tree.Interfaces;

namespace AdaptiveHuffman.Core.Tree
{
  public class Tree
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

    public void IncrementItem(string path)
    {
      var pathQueue = new Queue<char>(path.ToCharArray());

      var currentItem = Root as INotNYTTreeNode;

      while (pathQueue.Count > 0)
      {
        currentItem.Weight++;
        currentItem = (pathQueue.Dequeue() == '0' ? (currentItem as InnerNode).Left : (currentItem as InnerNode).Right) as INotNYTTreeNode;
      }
      currentItem.Weight++;
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

  }

}