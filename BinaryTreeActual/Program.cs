using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeActual { //Integer Binary Tree
  class Program {
    static void Main(string[] args) {
      Tree tree = new Tree(new IntNode(8, null));
      tree.InsertInt(7);
      tree.InsertInt(9);
      tree.InsertInt(10);
      Console.WriteLine(tree.getDepth(10));
      Console.WriteLine(tree.root.childRight.childRight.value);
      //Console.ReadLine();
      while (true) {
        Console.Clear();
        var results = tree.printFuckingEverything();
        var groupedResults = results.GroupBy(t => t.Item1, t => t.Item2).Select(g => Tuple.Create(g.Key, string.Join(" ", g))); //Groups tuples by first item
        foreach (Tuple<int, string> meme in groupedResults) {
          Console.WriteLine("Layer " + meme.Item1 + ": " + meme.Item2);
        }
        tree.InsertInt(int.Parse(Console.ReadLine()));
      }
    }
  }

  public class Tree {
    public IntNode root;
    public Tree(IntNode root) {
      this.root = root;
    }

    public void InsertRawNode(IntNode nodeToAdd, IntNode currentNode = null) { //Deprecated, parent must be given but cannot properly be identified with tree assigning a parent
      if (currentNode == null) {
        currentNode = root;
      }

      if (currentNode.value >= nodeToAdd.value) {
        if (currentNode.childLeft == null) {
          currentNode.childLeft = nodeToAdd;
        }
        else {
          InsertRawNode(nodeToAdd, currentNode.childLeft);
        }
      }
      else {  //(currentNode.value < nodeToAdd.value)
        if (currentNode.childRight == null) {
          currentNode.childRight = nodeToAdd;
        }
        else {
         InsertRawNode(nodeToAdd, currentNode.childRight);
        }
      }
    }

    public void InsertInt(int number, IntNode currentNode = null) {
      if (currentNode == null) {
        currentNode = root;
      }

      if (currentNode.value > number) {
        if (currentNode.childLeft == null) {
          currentNode.childLeft = new IntNode(number, currentNode);
        }
        else {
          InsertInt(number, currentNode.childLeft);
        }
      }
      else if (currentNode.value != number){  //(currentNode.value < nodeToAdd.value)
        if (currentNode.childRight == null) {
          currentNode.childRight = new IntNode(number, currentNode);
        }
        else {
          InsertInt(number, currentNode.childRight);
        }
      }else {
        Console.WriteLine("NO DUPLICATES YOU KNOB");
      }
    }

    public int getDepth(int number, IntNode currentNode = null, int depth = 0) {
      if (currentNode == null) {
        currentNode = root;
      }

      if (currentNode == null) {
        return -1;
      }
      else if (currentNode.value > number) {
        return getDepth(number, currentNode.childLeft, depth + 1);
      }
      else if (currentNode.value < number) {
        return getDepth(number, currentNode.childRight, depth + 1);
      }
      else {  // (currentNode.value == number) 
        return depth;
      }
    }

    public List<Tuple<int,string>> printFuckingEverything(List<Tuple<int,string>> result = null, IntNode currentNode = null) {
      if (currentNode == null) {
        currentNode = root;
      }
      if (result == null) {
        result = new List<Tuple<int,string>>();
      }
      string toCheck = getDepth(currentNode.value).ToString() + ".";
      //I KNOW THIS IS UGLY AND SLOW BUT IndexOf doesnt work and neither does Contains for some bloody reason
      string direction;
      if (currentNode != root) {
        if (currentNode.parent.childLeft == currentNode) {
          direction = "L";
        }
        else {
          direction = "R";
        }
        result.Add(new Tuple<int, string>(getDepth(currentNode.value), currentNode.value.ToString() + direction + currentNode.parent.value.ToString()));
      }else {
        result.Add(new Tuple<int, string>(0, currentNode.value.ToString()));
      }

      if (currentNode.childLeft != null) {
        printFuckingEverything(result, currentNode.childLeft);
      }
      if (currentNode.childRight != null) {
        printFuckingEverything(result, currentNode.childRight);
      }
      return result;
      
    }

  }

  public class IntNode {
    public int value;
    public IntNode childLeft, childRight, parent;
    public IntNode(int value, IntNode parent) {
      this.value = value;
      this.parent = parent;
      childLeft = null;
      childRight = null;
    }
    public void setLeftC(IntNode node) {
      childLeft = node;
    }
    public void setRightC(IntNode node) {
      childRight = node;
    }
  }
}
