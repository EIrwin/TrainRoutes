using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TrainRoutes.Service
{
    public interface ITrainService
    {
        double CalculateDistance(string routeDefinition);
        Task<double> CalculateDistanceASync(string routeDefinition, CancellationToken cancellationToken);

        int CalculateNumberOfTrips(string start, string end,int minStops,int maxStops);
        Task<int> CalculateNumberOfTripsAsync(string start, string end, int minStops, int maxStops,CancellationToken cancellationToken);

        int CalculateNumberOfTrips(List<string> routeDefinitions, Func<double, bool> predicate);
        Task<int> CalculateNumberOfTripsAsync(List<string> routeDefinitions, Func<double, bool> predicate,CancellationToken cancellationToken);

        double CalculateLengthOfShortestRoute(string start, string end);
        Task<double> CalculateLengthOfShortestRouteAsync(string start, string end, CancellationToken cancellationToken);

        int CalculatePossibleRoutes(string start, string end, int maxDistance);
        Task<int> CalculatePossibleRoutesAsync(string start, string end, int maxDistance, CancellationToken cancellationToken);
    }
}