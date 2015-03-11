using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TrainRoutes.Graph
{
    public class NodeCollection<T>:Collection<Node<T>>
    {
        public NodeCollection()
        {
            
        }

        public NodeCollection(IList<Node<T>> nodes) : base(nodes)
        {
            
        }

        public Node<T> FindByValue(T value)
        {
            return Items.FirstOrDefault(node => node.Value.Equals(value));
        }
    }
}