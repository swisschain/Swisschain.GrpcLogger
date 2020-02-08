namespace Swisschain.GrpcLogger
{
    public class GrpcLoggerFactory : IGrpcLoggerFactory
    {
        private readonly GrpcPublisher _publisher;

        public GrpcLoggerFactory(GrpcPublisher publisher)
        {
            _publisher = publisher;
        }

        public IGrpcLogger GetLogger(object component)
        {
            return new GrpcLogger(_publisher, component is string str ? str : component?.GetType().Name);
        }
    }
}