using System;
using System.Collections.Generic;
using TrainRoutes.Graph;

namespace TrainRoutes.Service
{
    public interface IRouteProvider<TValue>
    {
        /// <summary>
        /// Calculate the distance given a string
        /// representing the route to calculate distance for
        /// </summary>
        /// <param name="route"></param>
        /// <returns>The calculated distance of the provided route</returns>
        double CalculateDistance(string route);

        /// <summary>
        /// Calculate the distance given an array
        /// </summary>
        /// <param name="route">An array of nodes reflecting the route</param>
        /// <returns>The calculated distance of the provided route</returns>
        double CalculateDistance(TValue[] route);

        /// <summary>
        /// Calculate the distance given an array of nodes
        /// representing the route to calculate distance for
        /// </summary>
        /// <param name="route">An array of graph nodes reflecting the route</param>
        /// <returns>The calculated distance of the route provided</returns>
        double CalculateDistance(GraphNode<TValue>[] route);

        /// <summary>
        /// Get all paths between the provided start and ending value
        /// </summary>
        /// <param name="start">Value of starting node</param>
        /// <param name="end">Value of ending node</param>
        /// <returns>Collection of possible paths</returns>
        IEnumerable<Route<TValue>> GetPaths(TValue start, TValue end);

        /// <summary>
        /// Get all paths between the provided start and ending value
        /// that also satisfy the provided predicate criteria
        /// </summary>
        /// <param name="start">Starting value</param>
        /// <param name="end">Ending value</param>
        /// <returns>Collection of possible paths</returns>
        IEnumerable<Route<TValue>> GetPaths(TValue start, TValue end, Func<Route<TValue>, bool> predicate);

        /// <summary>
        /// Get all paths between the provided start and
        /// ending nodes that are provided
        /// </summary>
        /// <param name="start">Starting node</param>
        /// <param name="end">Ending node</param>
        /// <returns>Collection of possible paths</returns>
        IEnumerable<Route<TValue>> GetPaths(GraphNode<TValue> startNode, GraphNode<TValue> endNode);

        /// <summary>
        /// Get all paths between the provided start and 
        /// ending nodes that are provided which
        /// also satisfy the provided predicate criteria
        /// </summary>
        /// <param name="start">Starting node</param>
        /// <param name="end">Ending node</param>
        /// <returns>Collection of possible paths</returns>
        IEnumerable<Route<TValue>> GetPaths(GraphNode<TValue> startNode, GraphNode<TValue> endNode, Func<Route<TValue>, bool> predicate);

    }
}