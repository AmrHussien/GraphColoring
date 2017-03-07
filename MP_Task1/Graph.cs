using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace MP_Task1
{
    class Graph<T> where T : IEquatable<T>
    {

        private readonly IList<Nodes<T>> _nodeSet;




        // Initialize a new Graph 

        public Graph() : this(null) { }


        //    initial set of nodes in the graph 
        public Graph(IList<Nodes<T>> nodeSet)
        {
            if (nodeSet == null)
                _nodeSet = new Collection<Nodes<T>>();
            else
                _nodeSet = nodeSet;
        }

        // Return  set of nodes in the graph.

        public IList<Nodes<T>> Nodes
        {
            get { return _nodeSet; }
        }


        // Returns the number of vertices in the graph.

        public int Count
        {
            get { return _nodeSet.Count; }
        }

        // Add a new node
        public void AddNode(Nodes<T> node)
        {
            // adds a node to the graph
            _nodeSet.Add(node);
        }


        // Add a new value 
        public void AddNode(T value)
        {
            _nodeSet.Add(new Nodes<T>(value));
        }



        // Add an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to).

        public void AddUndirectedEdge(T from, T to)
        {
            var fromNode = _nodeSet.FindByValue(from);
            var toNode = _nodeSet.FindByValue(to);

            //if we did not find the nodes we cannot add them.
            if (fromNode == null || toNode == null) return;

            if (fromNode.Neighbors.Contains(toNode) || toNode.Neighbors.Contains(fromNode)) return;

            fromNode.Neighbors.Add(toNode);

            toNode.Neighbors.Add(fromNode);
        }


        // Adds an undirected edge from one GraphNode to another.

        public void AddUndirectedEdge(Nodes<T> fromNode, Nodes<T> toNode)
        {
            if (fromNode == null || toNode == null) return;

            if (fromNode.Neighbors.Contains(toNode) || toNode.Neighbors.Contains(fromNode)) return;

            fromNode.Neighbors.Add(toNode);

            toNode.Neighbors.Add(fromNode);
        }





        public bool Contains(T value)
        {
            return _nodeSet.FindByValue(value) != null;
        }
        // Remove Node 
        public bool Remove(T value)
        {
            // first remove the node from the nodeset
            Nodes<T> nodeToRemove = _nodeSet.FindByValue(value);
            if (nodeToRemove == null)
                // node wasn't found
                return false;

            // otherwise, the node was found
            _nodeSet.Remove(nodeToRemove);

            // enumerate through each node in the nodeSet, removing edges to this node
            foreach (var node in _nodeSet)
            {
                // remove the reference to the node.
                node.Neighbors.Remove(nodeToRemove);
            }

            return true;
        }


        public bool Remove(Nodes<T> node)
        {
            return Remove(node.Data);
        }
        public void Clear()
        {
            _nodeSet.Clear();
        }
    }
}
