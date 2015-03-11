using System;

namespace TrainRoutes.Graph
{
    public class Node<TValue>
    {
        public Guid Id { get; set; }
        public TValue Value { get; set;}
        public string Name { get; set; }
        public NodeCollection<TValue> Neighbors { get; set; }


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
            Value = value;
            Name = name;
        }
        public Node(TValue value, NodeCollection<TValue> neighbors)
        {
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
    }
}