using Autofac;


namespace Swisschain.GrpcLogger
{
    public static class AutofacExtension
    {
        public static ContainerBuilder RegisterGrpcLogger(this ContainerBuilder builder)
        {
            builder
                .RegisterInstance(GrpcLoggerFactoryStatic.Factory)
                .As<IGrpcLoggerFactory>()
                .SingleInstance();

            return builder;
        }
    }
}