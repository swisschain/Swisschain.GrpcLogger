using System;

namespace Swisschain.GrpcLogger
{
    public interface IGrpcLogger
    {
        void Info(string process, string message, Exception exception = null);
        void Warning(string process, string message, Exception exception = null);
        void WriteError(string process, Exception exception = null);
        void WriteError(string process, string message, Exception exception = null);
    }
}