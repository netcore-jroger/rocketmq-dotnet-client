using System;
using System.Runtime.InteropServices;

namespace RocketMQ.Client.Internal.Delegates
{
    internal static class ConsumerNativeMethodsDelegate
    {
        internal delegate IntPtr CreatePushConsumer(string groupId);

        internal delegate int DestroyPushConsumer(HandleRef consumer);

        internal delegate int StartPushConsumer(HandleRef consumer);

        internal delegate int ShutdownPushConsumer(HandleRef consumer);

        internal delegate int SetPushConsumerGroupID(HandleRef consumer, string groupId);

        internal delegate IntPtr GetPushConsumerGroupID(HandleRef consumer);

        internal delegate int SetPushConsumerNameServerAddress(HandleRef consumer, string namesrv);

        internal delegate int SetPushConsumerNameServerDomain(HandleRef consumer, string domain);

        internal delegate int Subscribe(HandleRef consumer, string topic, string expression);

        internal delegate int RegisterMessageCallbackOrderly(
            HandleRef consumer,
            [MarshalAs(UnmanagedType.FunctionPtr)]
            PtrMessageCallBack pCallback
        );

        internal delegate int RegisterMessageCallback(
            HandleRef consumer,
            [MarshalAs(UnmanagedType.FunctionPtr)]
            PtrMessageCallBack pCallback
        );

        internal delegate int PtrMessageCallBack(IntPtr consumer, IntPtr messageIntPtr);

        internal delegate int UnregisterMessageCallbackOrderly(HandleRef consumer);

        internal delegate int UnregisterMessageCallback(HandleRef consumer);

        internal delegate int SetPushConsumerThreadCount(HandleRef consumer, int threadCount);

        internal delegate int SetPushConsumerMessageBatchMaxSize(HandleRef consumer, int batchSize);

        internal delegate int SetPushConsumerInstanceName(HandleRef consumer, string instanceName);

        internal delegate int SetPushConsumerSessionCredentials(HandleRef consumer, string accessKey, string secretKey, string channel);

        internal delegate int SetPushConsumerLogPath(HandleRef consumer, string logPath);

        internal delegate int SetPushConsumerLogFileNumAndSize(HandleRef consumer, int fileNum, long fileSize);

        internal delegate int SetPushConsumerLogLevel(HandleRef consumer, LogLevel level);

        internal delegate int SetPushConsumerMessageModel(HandleRef consumer, MessageModel messageModel);
    }
}
