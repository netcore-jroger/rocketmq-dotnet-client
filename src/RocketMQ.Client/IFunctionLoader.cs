using System;

namespace RocketMQ.Client
{
    public interface IFunctionLoader
    {
        TDelegate LoadFunction<TDelegate>(string functionName) where TDelegate : Delegate;

        void Free();
    }
}
