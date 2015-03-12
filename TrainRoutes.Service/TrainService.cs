using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TrainRoutes.Service
{
    public class TrainService:ITrainService
    {
        private IRouteProvider<string> _routeProvider;

        public TrainService(IRouteProvider<string> routeProvider)
        {
            _routeProvider = routeProvider;
        }

        public double CalculateDistance(string routeDefinition)
        {
            return _routeProvider.CalculateDistance(routeDefinition);
        }
        public async Task<double> CalculateDistanceASync(string routeDefinition, CancellationToken cancellationToken)
        {
            return await Task.Run(() => CalculateDistance(routeDefinition), cancellationToken);
        }

        public int CalculateNumberOfTrips(string start, string end, int minStops, int maxStops)
        {
            return _routeProvider.GetRoutes(start, end, (route) =>
                {
                    //deduct visited count by 1 to 
                    //count the number of 'hops'in a route
                    var trips = route.Visited.Count - 1;
                    return trips >= minStops && trips <= maxStops;
                }).Count();
        }
        public async Task<int> CalculateNumberOfStopsAsync(string start, string end, int minStops, int maxStops,CancellationToken cancellationToken)
        {
            return await Task.Run(() => CalculateNumberOfTrips(start, end, minStops, maxStops), cancellationToken);
        }

        public double CalculateLengthOfShortestRoute(string start, string end)
        {
            var shortestRoute = _routeProvider.GetRoutes(start, end).OrderBy(p => p.Distance).FirstOrDefault();
            if (shortestRoute == null) throw new InvalidOperationException("NO SUCH ROUTE");
            return shortestRoute.Distance;
        }
        public async Task<double> CalculateLengthOfShortestRouteAsync(string start, string end, CancellationToken cancellationToken)
        {
            return await Task.Run(() => CalculateLengthOfShortestRoute(start, end), cancellationToken);
        }

        public int CalculatePossibleRoutes(string start, string end, int maxDistance)
        {
            return _routeProvider.GetRoutes(start, end, (route) => route.Distance <= maxDistance).Count();
        }
        public async Task<int> CalculatePossibleRoutesAsync(string start, string end, int maxDistance, CancellationToken cancellationToken)
        {
            return await Task.Run(() => CalculatePossibleRoutes(start, end, maxDistance), cancellationToken);
        }
    }
}
