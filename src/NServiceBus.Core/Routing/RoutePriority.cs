namespace NServiceBus.Routing
{
    /// <summary>
    /// Represents the priority of a route. Routes with bigger priority (which means smaller numeric value) override these with smaller priority (larger numeric value).
    /// </summary>
    public struct RoutePriority
    {
        int priorityValue;

        /// <summary>
        /// Crates a new instance of priority.
        /// </summary>
        /// <param name="priorityValue">Numeric value of priority. The lower the number the higher the priority. Must be non-negative.</param>
        public RoutePriority(int priorityValue)
        {
            Guard.AgainstNegative(nameof(priorityValue), priorityValue);
            this.priorityValue = priorityValue;
        }

        /// <summary>
        /// Route priority for a specific type route (highest).
        /// </summary>
        public static RoutePriority SpecificType = new RoutePriority(0);

        /// <summary>
        /// Route priority for a specific namespace within an assembly.
        /// </summary>
        public static RoutePriority SpecificNamespace = new RoutePriority(100);

        /// <summary>
        /// Route priority for an assembly-level route.
        /// </summary>
        public static RoutePriority SpecificAssembly = new RoutePriority(1000);

        /// <summary>
        /// Checks weather this priority is higher than the other.
        /// </summary>
        public bool IsHigherThan(RoutePriority other)
        {
            return priorityValue < other.priorityValue;
        }
    }
}