using System;

namespace Swisschain.GrpcLogger
{
    public interface IGrpcLogger
    {
        void Info(string process, string message, Exception exception = null);
        void Warning(string process, string message, Exception exception = null);
        void Error(string process, Exception exception = null);
        void Error(string process, string message, Exception exception = null);
    }
}