namespace TrainRoutes.Service
{
    public interface ITrainService
    {
        double CalculateDistance(string routeDefinition);
        int CalculateNumberOfStops(string start, string end,int minStops,int maxStops);
        double CalculateLengthOfShortestRoute(string start, string end);
        int CalculatePossibleRoutes(string start, string end, int maxDistance);
    }
}