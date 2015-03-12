using System;
using System.Collections.Generic;

namespace TrainRoutes.Graph
{
    public class GraphNode<TValue> : Node<TValue>
    {
        #region [Private Fields]

        private IDictionary<Guid, double> _costs;

        #endregion

        #region [Public Properties]

        public IDictionary<Guid, double> Costs
        {
            get { return _costs ?? (_costs = new Dictionary<Guid,double>()); }
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

        #endregion

        #region [Constructors]

        public GraphNode(){ }
        public GraphNode(TValue value) : base(value) { }
        public GraphNode(TValue value, string name) : base(value, name)
        {
            
        }
        public GraphNode(TValue value, NodeCollection<TValue> neighbors) : base(value, neighbors) { }
        public GraphNode(TValue value, NodeCollection<TValue> neighbors, string name) : base(value, neighbors, name) { }

        #endregion
    }
}