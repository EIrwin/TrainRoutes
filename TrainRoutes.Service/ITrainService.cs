using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TrainRoutes.Service
{
    public interface ITrainService
    {
        /// <summary>
        /// Calculate the distance using the
        /// route definition string provided
        /// </summary>
        /// <param name="routeDefinition">Represents the route definition</param>
        /// <returns>Distance of the route provided</returns>
        double CalculateDistance(string routeDefinition);

        /// <summary>
        /// Caclculate the distane using the
        /// route definition string provided
        /// </summary>
        /// <param name="routeDefinition">Represents the route definition</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Distance of the route provided</returns>
        Task<double> CalculateDistanceASync(string routeDefinition, CancellationToken cancellationToken);

        /// <summary>
        /// Calculate the number of trips given
        /// the start and end node that satisfies
        /// the minimum and maximum amount of stops
        /// </summary>
        /// <param name="start">Repesents start node</param>
        /// <param name="end">Represents end node</param>
        /// <param name="minStops">Minimum stop count</param>
        /// <param name="maxStops">Maximum stop count</param>
        /// <returns>Number of trips</returns>
        int CalculateNumberOfTrips(string start, string end,int minStops,int maxStops);

        /// <summary>
        /// Calculate the number of trips given
        /// the start and end node that satisfies
        /// the minimum and maximum amount of stops
        /// </summary>
        /// <param name="start">Represents start node</param>
        /// <param name="end">Represents end node</param>
        /// <param name="minStops">Minimum stop count</param>
        /// <param name="maxStops">Maximum stop count</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Number of trips</returns>
        Task<int> CalculateNumberOfTripsAsync(string start, string end, int minStops, int maxStops,CancellationToken cancellationToken);


        /// <summary>
        /// Calculate the number of trips given
        /// a list of route definitions that also
        /// satisfies the predicate provided
        /// </summary>
        /// <param name="routeDefinitions">Represents the possible route definitions</param>
        /// <param name="predicate">Predicate to run against the data</param>
        /// <returns>Number of trips</returns>
        int CalculateNumberOfTrips(List<string> routeDefinitions);


        /// <summary>
        /// Calculate the number of trips given
        /// a list of route definitions that also
        /// satisfies the predicate provided
        /// </summary>
        /// <param name="routeDefinitions">Represents the possible route definitions</param>
        /// <param name="predicate">Predicate to run against the data</param>
        /// <returns>Number of trips</returns>
        int CalculateNumberOfTrips(List<string> routeDefinitions, Func<double, bool> predicate);


        /// <summary>
        /// Calculate the number of trips given
        /// a list of route definitions that also
        /// satisfies the predicate provided
        /// </summary>
        /// <param name="routeDefinitions">Represents the possible route definitions</param>
        /// <param name="predicate">Predicate to run against the data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Number of trips</returns>
        Task<int> CalculateNumberOfTripsAsync(List<string> routeDefinitions, Func<double, bool> predicate,CancellationToken cancellationToken);

        /// <summary>
        /// Calculate the length (in distance) of the shortest route
        /// </summary>
        /// <param name="start">Represents start node</param>
        /// <param name="end">Represents end node</param>
        /// <returns>Length in (distance) of the shortest route</returns>
        double CalculateLengthOfShortestRoute(string start, string end);

        /// <summary>
        /// Calculate the length (in distance) of the shortest route
        /// </summary>
        /// <param name="start">Represents start node</param>
        /// <param name="end">Represents end node</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Length in (distance) of the shortest route</returns>
        Task<double> CalculateLengthOfShortestRouteAsync(string start, string end, CancellationToken cancellationToken);

        /// <summary>
        /// Calculates number of possible routes that
        /// also satisfies the maximum distance criteria
        /// </summary>
        /// <param name="start">Represents start node</param>
        /// <param name="end">Represents end node</param>
        /// <param name="maxDistance">Represents the maximum distance to calculate routes for</param>
        /// <returns>Number of possible routes that meets max distance criteria</returns>
        int CalculatePossibleRoutes(string start, string end, int maxDistance);

        /// <summary>
        /// Calculates nuber of possible routes that
        /// also satisfies the maximum distance criteria
        /// </summary>
        /// <param name="start">Represents start node</param>
        /// <param name="end">Represents end node</param>
        /// <param name="maxDistance">Represents the maximum distance to calculate routes for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Number of possible routes that meets max distance criteria</returns>
        Task<int> CalculatePossibleRoutesAsync(string start, string end, int maxDistance, CancellationToken cancellationToken);
    }
}