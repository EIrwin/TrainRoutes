using System.Collections.Generic;

namespace TrainRoutes.Graph
{
    public class GraphNode<TValue> : Node<TValue>
    {
        //private fields
        private List<double> _costs;

        //public properties
        public List<double> Costs
        {
            get { return _costs ?? (_costs = new List<double>()); }
        }
        public new NodeCollection<TValue> Neighbors
        {
            get
            {
                if (base.Neighbors == null)
                    base.Neighbors = new NodeCollection<TValue>();

                return base.Neighbors;
            }
        }

        //constructors
        public GraphNode() : base() { }
        public GraphNode(TValue value) : base(value) { }
        public GraphNode(TValue value, string name) : base(value, name)
        {
            
        }
        public GraphNode(TValue value, NodeCollection<TValue> neighbors) : base(value, neighbors) { }
        public GraphNode(TValue value,NodeCollection<TValue> neighbors,string name):base(value,neighbors,name){}
    }
}