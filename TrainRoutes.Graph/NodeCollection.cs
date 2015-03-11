using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TrainRoutes.Graph
{
    public class NodeCollection<T>:Collection<GraphNode<T>>
    {
        public NodeCollection()
        {
            
        }

        public NodeCollection(IList<GraphNode<T>> nodes) : base(nodes)
        {
            
        }

        public GraphNode<T> FindByValue(T value)
        {
            return Items.FirstOrDefault(node => node.Value.Equals(value));
        }
    }
}