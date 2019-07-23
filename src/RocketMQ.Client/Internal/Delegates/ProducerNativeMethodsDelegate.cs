using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("Producer.Sample")]

namespace RocketMQ.Client.Internal.Delegates
{
    internal static class ProducerNativeMethodsDelegate
    {
        // for producer
        internal delegate IntPtr CreateProducerDelegate(string groupId);

        internal delegate int StartProducer(HandleRef producer);

        internal delegate int ShutdownProducer(HandleRef producer);

        internal delegate int DestroyProducer(HandleRef producer);

        // for producer options
        internal delegate int SetProducerNameServerAddress(HandleRef producer, string nameServer);

        internal delegate int SetProducerNameServerDomain(HandleRef producer, string domain);

        internal delegate int SetProducerGroupName(HandleRef producer, string groupName);

        internal delegate int SetProducerInstanceName(HandleRef producer, string instanceName);

        internal delegate int SetProducerSessionCredentials(HandleRef producer, string accessKey, string secretKey, string channel);

        internal delegate int SetProducerLogPath(HandleRef producer, string logPath);

        internal delegate int SetProducerLogFileNumAndSize(HandleRef producer, int fileNum, long fileSize);

        internal delegate int SetProducerLogLevel(HandleRef producer, LogLevel level);

        internal delegate int SetProducerSendMsgTimeout(HandleRef producer, int timeout);

        internal delegate int SetProducerCompressLevel(HandleRef producer, int level);

        internal delegate int SetProducerMaxMessageSize(HandleRef producer, int size);

        // for producer send
        internal delegate int SendMessageSync(
            HandleRef producer,
            HandleRef message,
            [Out]
            out CSendResult result
        );

        internal delegate int SendMessageAsync(
            HandleRef producer,
            HandleRef message,
            [MarshalAs(UnmanagedType.FunctionPtr)]
            CSendSuccessCallback cSendSuccessCallback,
            [MarshalAs(UnmanagedType.FunctionPtr)]
            CSendExceptionCallback cSendExceptionCallback
        );

        internal delegate int SendMessageOneway(HandleRef producer, HandleRef message);

        internal delegate int SendMessageOrderly(
            HandleRef producer,
            HandleRef message,
            [MarshalAs(UnmanagedType.FunctionPtr)]
            CQueueSelectorCallback callback,
            IntPtr arg,
            int autoRetryTimes,
            [Out]
            out CSendResult result
        );

        internal delegate void CSendSuccessCallback(CSendResult result);

        internal delegate void CSendExceptionCallback(CMQException e);

        internal delegate int CQueueSelectorCallback(int size, IntPtr message, IntPtr args);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct CMQException
    {
        public int error;

        public int line;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string file;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string msg;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string type;
    }
}
