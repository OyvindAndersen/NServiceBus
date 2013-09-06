namespace NServiceBus.Transports.ActiveMQ
{
    using System;

    public class SubscriptionEventArgs : EventArgs
    {
        public SubscriptionEventArgs(string topic)
        {
            Topic = topic;
        }

        public string Topic { get; private set; }
    }
}