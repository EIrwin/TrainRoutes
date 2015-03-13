using System;
using System.Collections.Generic;
using NUnit.Framework;
using Simple.Mocking;
using TrainRoutes.Graph;
using TrainRoutes.Service;

namespace TrainRoutes.Test
{
    [TestFixture]
    public class TrainServiceTests
    {
        //concrete implementations
        private ITrainService _trainService;

        //mock implementations
        private IRouteProvider<string> _mockRouteProvider;
            
        [SetUp]
        public void Setup()
        {
            //create mock IRouteProvider<string> implementation
            _mockRouteProvider = Mock.Interface<IRouteProvider<string>>();

            //initialize ITrainService and inject mock provider
            _trainService = new TrainService(_mockRouteProvider);

        }

        /// <summary>
        /// The purpose of this test is to assert that
        /// if an unknown node is passed into start argument
        /// for CalculateNumberOfTrips(start,end,min,max) that it
        /// will handle it correctly and return a result of 0
        /// </summary>
        [Test]
        public void CalculateNumberOfTrips_UnknownStartNode()
        {
            //sample start and end with invalid start node
            string start = "R";
            string end = "A";

            //Mock call to IRouteProvider.GetRoute(string,string,predicate)
            Expect.MethodCall(
                () =>
                _mockRouteProvider.GetRoutes(Any<string>.Value, Any<string>.Value, Any<Func<Route<string>, bool>>.Value)).Returns(new List<Route<string>>());

            int trips = _trainService.CalculateNumberOfTrips(start, end, 0, 4);

            Assert.IsTrue(trips == 0);
        }

        /// <summary>
        /// The purpose of this test is to assert that if a 
        /// minimum value greater than the maximum value
        /// for CalculateNumberOfTrips(start,end,min,max)
        /// that an ArgumentException is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateNumberOfTrips_MaxLessThanMin()
        {
            string start = "A"; //any value works
            string end = "B";   //any value works
            int min = 4;    //must be greater than max
            int max = 2;    //must be less than min

            //the following call should result in an ArgumentException
            //being thrown so we do not need to assert any result
            _trainService.CalculateNumberOfTrips(start, end, min, max);
        }

        /// <summary>
        /// The purpose of this test is to assert that
        /// an ArgumentNullException is thrown if a null 
        /// predicate is passed into CalculateNumberOfTrips(...)
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateNumberOfTrips_NullPredicate()
        {
            //the following call should result in an ArgumentNullException
            //being thrown so we do not need to assert any result
            _trainService.CalculateNumberOfTrips(new List<string>(), null);
        }

        /// <summary>
        /// The purpose of this test is to assert that
        /// an ArgumentNullException is thrown if a null 
        /// route definition list is passed into CalculateNumberOfTrips(...)
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateNumberOfTrips_NullRouteDefinitionsList()
        {
            //the following call should result in an ArgumentNullException
            //being thrown so we do not need to assert any result
            _trainService.CalculateNumberOfTrips(null,(route)=> true);
        }

        /// <summary>
        /// The purpose of this test is to assert that
        /// if a collection of rempty route definitions is passed in
        /// to CalculateNumberOfTrips(..), that it will handle it correctly
        /// and return a result of 0.
        /// </summary>
        [Test]
        public void CalculateNumberOfTrips_EmptyList()
        {
            List<string> testDefinitions = new List<string>();
            int trips = _trainService.CalculateNumberOfTrips(testDefinitions, (route) => true);
            Assert.IsTrue(trips == 0);
        }


        /// <summary>
        /// The purpose of this test is to assert that
        /// if an empty route definition is passed into to 
        /// CalculateDistance(...) that it is handle correctly
        /// and an ArgumentException is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateDistance_EmptyRouteDefinition()
        {
            string routeDefinition = string.Empty;  //must be empty

            //The following call should result in an ArgumentException
            //being thrown so we donot need to assert any result
            _trainService.CalculateDistance(routeDefinition);
        }


        /// <summary>
        /// The purpose of this test is to assert that
        /// if an null route definition is passed into to 
        /// CalculateDistance(...) that it is handle correctly
        /// and an ArgumentException is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateDistance_NullRouteDefinition()
        {
            string routeDefinition = null;  //must be null

            //The following call should result in an ArgumentException
            //being thrown so we donot need to assert any result
            _trainService.CalculateDistance(routeDefinition);
        }
    }
}