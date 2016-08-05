namespace NServiceBus.Routing.MessageDrivenSubscriptions
{
    using System;

    /// <summary>
    /// Represents an entry in a publisher table.
    /// </summary>
    public class PublisherTableEntry
    {
        /// <summary>
        /// Type of event.
        /// </summary>
        public Type EventType { get; }
        /// <summary>
        /// Addres.
        /// </summary>
        public PublisherAddress Address { get; }
        /// <summary>
        /// Relative priority of this entry.
        /// </summary>
        public RoutePriority Priority { get; }

        /// <summary>
        /// Creates a new entry.
        /// </summary>
        public PublisherTableEntry(Type eventType, PublisherAddress address, RoutePriority priority)
        {
            EventType = eventType;
            Address = address;
            Priority = priority;
        }
    }
}