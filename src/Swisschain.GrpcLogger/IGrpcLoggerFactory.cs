namespace Swisschain.GrpcLogger
{
    /// <summary>
    /// Generate instance of 
    /// </summary>
    public interface IGrpcLoggerFactory
    {
        IGrpcLogger GetLogger(object component);
    }
}