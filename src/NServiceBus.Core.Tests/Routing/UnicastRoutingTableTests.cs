namespace NServiceBus.Core.Tests.Routing
{
    using System.Collections.Generic;
    using NServiceBus.Routing;
    using NUnit.Framework;

    [TestFixture]
    class UnicastRoutingTableTests
    {
        [Test]
        public void When_group_does_not_exist_routes_are_added()
        {
            var routingTable = new UnicastRoutingTable();
            var route = UnicastRoute.CreateFromEndpointName("Endpoint1");
            routingTable.AddOrReplaceRoutes("key", new List<RouteTableEntry>()
            {
                new RouteTableEntry(typeof(Command), route, RoutePriority.SpecificType),
            });

            var retrievedRoute = routingTable.GetRouteFor(typeof(Command));
            Assert.AreSame(route, retrievedRoute);
        }

        [Test]
        public void When_group_exists_routes_are_replaced()
        {
            var routingTable = new UnicastRoutingTable();
            var oldRoute = UnicastRoute.CreateFromEndpointName("Endpoint1");
            var newRoute = UnicastRoute.CreateFromEndpointName("Endpoint2");
            routingTable.AddOrReplaceRoutes("key", new List<RouteTableEntry>()
            {
                new RouteTableEntry(typeof(Command), oldRoute, RoutePriority.SpecificType),
            });

            routingTable.AddOrReplaceRoutes("key", new List<RouteTableEntry>()
            {
                new RouteTableEntry(typeof(Command), newRoute, RoutePriority.SpecificType),
            });

            var retrievedRoute = routingTable.GetRouteFor(typeof(Command));
            Assert.AreSame(newRoute, retrievedRoute);
        }

        [Test]
        public void Routes_with_higher_priority_take_precedence()
        {
            var routingTable = new UnicastRoutingTable();
            var lowPriorityRoute = UnicastRoute.CreateFromEndpointName("Endpoint1");
            var highPriorityRoute = UnicastRoute.CreateFromEndpointName("Endpoint2");

            routingTable.AddOrReplaceRoutes("key2", new List<RouteTableEntry>()
            {
                new RouteTableEntry(typeof(Command), highPriorityRoute, RoutePriority.SpecificType),
            });

            routingTable.AddOrReplaceRoutes("key1", new List<RouteTableEntry>()
            {
                new RouteTableEntry(typeof(Command), lowPriorityRoute, RoutePriority.SpecificAssembly),
            });

            var retrievedRoute = routingTable.GetRouteFor(typeof(Command));
            Assert.AreSame(highPriorityRoute, retrievedRoute);
        }

        class Command
        {
        }
    }
}