using System;
using System.Linq;

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

        public int CalculateNumberOfStops(string start, string end, int minStops, int maxStops)
        {
            return _routeProvider.GetPaths(start, end, (route) => route.Visited.Count >= minStops && route.Visited.Count < maxStops).Count();
        }

        public double CalculateLengthOfShortestRoute(string start, string end)
        {
            var shortestRoute = _routeProvider.GetPaths(start, end).OrderBy(p => p.Distance).FirstOrDefault();
            if (shortestRoute == null) throw new InvalidOperationException("NO SUCH ROUTE");
            return shortestRoute.Distance;
        }

        public int CalculatePossibleRoutes(string start, string end, int maxDistance)
        {
            return _routeProvider.GetPaths(start, end, (route) => route.Distance < maxDistance).Count();
        }
    }
}
