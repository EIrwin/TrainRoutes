using System;

namespace TrainRoutes.Graph
{
    public class Node<TValue>
    {
        #region [Public Properties]

        public Guid Id { get; private set; }
        public TValue Value { get; set;}
        public string Name { get; set; }
        public NodeCollection<TValue> Neighbors { get; set; }

        #endregion

        #region [Constructors]

        public Node()
        {
            Id = Guid.NewGuid();
            Neighbors = new NodeCollection<TValue>();
        }
        public Node(TValue value)
        {
            Id = Guid.NewGuid();
            Value = value;
            Neighbors = new NodeCollection<TValue>();
        }
        public Node(TValue value, string name)
        {
            Id = Guid.NewGuid();
            Value = value;
            Name = name;
        }
        public Node(TValue value, NodeCollection<TValue> neighbors)
        {
            Id = Guid.NewGuid();
            Value = value;
            Neighbors = neighbors;
        }
        public Node(TValue value,NodeCollection<TValue> neighbors,string name)
        {
            Id = Guid.NewGuid();
            Value = value;
            Name = name;
            Neighbors = neighbors;
        }

        #endregion
    }
}