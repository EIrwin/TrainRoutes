using System.Threading;
using System.Threading.Tasks;

namespace TrainRoutes.Service
{
    public interface ITrainService
    {
        double CalculateDistance(string routeDefinition);
        Task<double> CalculateDistanceASync(string routeDefinition, CancellationToken cancellationToken);

        int CalculateNumberOfTrips(string start, string end,int minStops,int maxStops);
        Task<int> CalculateNumberOfStopsAsync(string start, string end, int minStops, int maxStops,CancellationToken cancellationToken);

        double CalculateLengthOfShortestRoute(string start, string end);
        Task<double> CalculateLengthOfShortestRouteAsync(string start, string end, CancellationToken cancellationToken);

        int CalculatePossibleRoutes(string start, string end, int maxDistance);
        Task<int> CalculatePossibleRoutesAsync(string start, string end, int maxDistance, CancellationToken cancellationToken);
    }
}