namespace TrainRoutes.Graph
{
    public interface IGraph<T>
    {
        void Insert(Edge edge);

        bool EdgeExists(Node<T> source, Node<T> destination);

        Edge GetEdge(Node<T> source, Node<T> destination);
    }
}
