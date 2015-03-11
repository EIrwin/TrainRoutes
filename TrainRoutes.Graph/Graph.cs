using System;
using System.Collections;
using System.Collections.Generic;

namespace TrainRoutes.Graph
{
    public class Graph<TValue> : IEnumerable<TValue>
    {
        private readonly NodeCollection<TValue> _nodeCollection;

        public Graph() : this(null) { }
        public Graph(NodeCollection<TValue> nodeCollection)
        {
            if (nodeCollection == null)
                this._nodeCollection = new NodeCollection<TValue>();
            else
                this._nodeCollection = nodeCollection;
        }

        public void AddNode(GraphNode<TValue> node)
        {
            // adds a node to the graph
            _nodeCollection.Add(node);
        }

        public void AddNode(TValue value)
        {
            // adds a node to the graph
            _nodeCollection.Add(new GraphNode<TValue>(value));
        }

        public void AddEdge(GraphNode<TValue>  from, GraphNode<TValue> to, double cost, bool isDirected)
        {
            if (isDirected)
                AddDirectedEdge(from, to, cost);
            else
                AddUndirectedEdge(from, to, cost);
        }

        public void AddDirectedEdge(GraphNode<TValue> from, GraphNode<TValue> to,double cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);
        }

        public void AddUndirectedEdge(GraphNode<TValue> from, GraphNode<TValue> to,double cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);

            to.Neighbors.Add(from);
            to.Costs.Add(cost);
        }

        public bool Contains(TValue value)
        {
            return _nodeCollection.FindByValue(value) != null;
        }

        public bool Remove(TValue value)
        {
            // first remove the node from the nodeset
            GraphNode<TValue> nodeToRemove = (GraphNode<TValue>)_nodeCollection.FindByValue(value);
            if (nodeToRemove == null)
                // node wasn't found
                return false;

            // otherwise, the node was found
            _nodeCollection.Remove(nodeToRemove);

            // enumerate through each node in the nodeCollection, removing edges to this node
            foreach (GraphNode<TValue> gnode in _nodeCollection)
            {
                int index = gnode.Neighbors.IndexOf(nodeToRemove);
                if (index != -1)
                {
                    // remove the reference to the node and associated cost
                    gnode.Neighbors.RemoveAt(index);
                    gnode.Costs.RemoveAt(index);
                }
            }

            return true;
        }

        public NodeCollection<TValue> Nodes
        {
            get
            {
                return _nodeCollection;
            }
        }

        public int Count
        {
            get { return _nodeCollection.Count; }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}