using System;

namespace TrainRoutes.Graph
{
    public class Node<T>:Node
    {
        public T Value {get;set;}

        public Node(T value, string name) : base(name)
        {
            Value = value;
        }
    }

    public class Node
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public Node(string name)
        {
            ID = Guid.NewGuid();
            Name = name;
        }
    }
}