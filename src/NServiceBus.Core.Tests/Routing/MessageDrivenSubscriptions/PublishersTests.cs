namespace NServiceBus.Core.Tests.Routing.MessageDrivenSubscriptions
{
    using System.Collections.Generic;
    using NServiceBus.Routing;
    using NServiceBus.Routing.MessageDrivenSubscriptions;
    using NUnit.Framework;

    [TestFixture]
    public class PublishersTests
    {
        [Test]
        public void When_group_does_not_exist_routes_are_added()
        {
            var publisherTable = new Publishers();
            var publisher = PublisherAddress.CreateFromEndpointName("Endpoint1");
            publisherTable.AddOrReplacePublishers("key", new List<PublisherTableEntry>
            {
                new PublisherTableEntry(typeof(MyEvent), publisher, RoutePriority.SpecificType),
            });

            var retrievedPublisher = publisherTable.GetPublisherFor(typeof(MyEvent));
            Assert.AreSame(publisher, retrievedPublisher);
        }

        [Test]
        public void When_group_exists_routes_are_replaced()
        {
            var publisherTable = new Publishers();
            var oldPublisher = PublisherAddress.CreateFromEndpointName("Endpoint1");
            var newPublisher = PublisherAddress.CreateFromEndpointName("Endpoint2");
            publisherTable.AddOrReplacePublishers("key", new List<PublisherTableEntry>
            {
                new PublisherTableEntry(typeof(MyEvent), oldPublisher, RoutePriority.SpecificType),
            });

            publisherTable.AddOrReplacePublishers("key", new List<PublisherTableEntry>
            {
                new PublisherTableEntry(typeof(MyEvent), newPublisher, RoutePriority.SpecificType),
            });

            var retrievedPublisher = publisherTable.GetPublisherFor(typeof(MyEvent));
            Assert.AreSame(newPublisher, retrievedPublisher);
        }

        [Test]
        public void Routes_with_higher_priority_take_precedence()
        {
            var publisherTable = new Publishers();
            var lowPriorityPublisher = PublisherAddress.CreateFromEndpointName("Endpoint1");
            var highPriorityPublisher = PublisherAddress.CreateFromEndpointName("Endpoint1");

            publisherTable.AddOrReplacePublishers("key2", new List<PublisherTableEntry>
            {
                new PublisherTableEntry(typeof(MyEvent), highPriorityPublisher, RoutePriority.SpecificType),
            });

            publisherTable.AddOrReplacePublishers("key1", new List<PublisherTableEntry>
            {
                new PublisherTableEntry(typeof(MyEvent), lowPriorityPublisher, RoutePriority.SpecificAssembly),
            });

            var retrievedPublisher = publisherTable.GetPublisherFor(typeof(MyEvent));
            Assert.AreSame(highPriorityPublisher, retrievedPublisher);
        }

        class MyEvent
        {
        }
    }
}

namespace MessageNameSpace
{
    interface IMessageInterface
    {
    }

    class BaseMessage : IMessageInterface
    {
    }
}

namespace OtherMesagenameSpace
{
    using MessageNameSpace;

    class SubMessage : BaseMessage
    {
    }
}

class EventWithoutNamespace
{
}