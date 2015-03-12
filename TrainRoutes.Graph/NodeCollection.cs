using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TrainRoutes.Graph
{
    public class NodeCollection<T>:Collection<GraphNode<T>>
    {
        #region [Constructors]

        public NodeCollection()
        {
            
        }

        public NodeCollection(IList<GraphNode<T>> nodes) : base(nodes)
        {
            
        }

        #endregion

        #region [Public Methods]

        public GraphNode<T> FindByValue(T value)
        {
            return Items.FirstOrDefault(node => node.Value.Equals(value));
        }

        #endregion
    }
}