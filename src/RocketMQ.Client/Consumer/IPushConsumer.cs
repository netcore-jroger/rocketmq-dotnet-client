using System;
using System.Runtime.InteropServices;
using RocketMQ.Client.Consumer.Internal;
using RocketMQ.Client.Internal.Delegates;

namespace RocketMQ.Client.Consumer
{
    public interface IPushConsumer
    {
        void CreatePushConsumer(string groupId);

        int DestroyPushConsumer();

        int StartPushConsumer();

        int ShutdownPushConsumer();

        int SetPushConsumerGroupID(string groupId);

        string GetPushConsumerGroupID();

        int SetPushConsumerNameServerAddress(string namesrv);

        int SetPushConsumerNameServerDomain(string domain);

        int Subscribe(string topic, string expression);

        int RegisterMessageCallback(MessageCallBack callback);

        int UnregisterMessageCallback();

        int RegisterMessageCallbackOrderly(MessageCallBack callback);

        int UnregisterMessageCallbackOrderly();

        int SetPushConsumerThreadCount(int threadCount);

        int SetPushConsumerMessageBatchMaxSize(int batchSize);

        int SetPushConsumerInstanceName(string instanceName);

        int SetPushConsumerSessionCredentials(string accessKey, string secretKey, string channel);

        int SetPushConsumerLogPath(string logPath);

        int SetPushConsumerLogFileNumAndSize(int fileNum, long fileSize);

        int SetPushConsumerLogLevel(LogLevel level);

        int SetPushConsumerMessageModel(MessageModel messageModel);
    }

    internal class DefaultPushConsumer : IPushConsumer
    {
        private readonly IFunctionLoader _functionLoader;

        private HandleRef _pushConsumerHandle;

        public DefaultPushConsumer(IFunctionLoader functionLoader)
        {
            this._functionLoader = functionLoader ?? throw new ArgumentNullException(nameof(functionLoader));
        }

        public void CreatePushConsumer(string groupId)
        {
            var ptr = this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.CreatePushConsumer>(nameof(CreatePushConsumer))(groupId);
            if (ptr == IntPtr.Zero)
            {
                throw new ArgumentException($"Failed to create push consumer. groupId:{groupId}");
            }

            this._pushConsumerHandle = new HandleRef(this, ptr);
        }

        public int DestroyPushConsumer()
        {
            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.DestroyPushConsumer>(nameof(DestroyPushConsumer))(this._pushConsumerHandle);
        }

        public int StartPushConsumer()
        {
            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.StartPushConsumer>(nameof(StartPushConsumer))(this._pushConsumerHandle);
        }

        public int ShutdownPushConsumer()
        {
            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.ShutdownPushConsumer>(nameof(ShutdownPushConsumer))(this._pushConsumerHandle);
        }

        public int SetPushConsumerGroupID(string groupId)
        {
            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.SetPushConsumerGroupID>(nameof(SetPushConsumerGroupID))(this._pushConsumerHandle, groupId);
        }

        public string GetPushConsumerGroupID()
        {
            var ptr = this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.GetPushConsumerGroupID>(nameof(GetPushConsumerGroupID))(this._pushConsumerHandle);

            if (ptr == IntPtr.Zero) return string.Empty;

            return Marshal.PtrToStringAnsi(ptr);
        }

        public int SetPushConsumerNameServerAddress(string namesrv)
        {
            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.SetPushConsumerNameServerAddress>(nameof(SetPushConsumerNameServerAddress))(this._pushConsumerHandle, namesrv);
        }

        public int SetPushConsumerNameServerDomain(string domain)
        {
            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.SetPushConsumerNameServerDomain>(nameof(SetPushConsumerNameServerDomain))(this._pushConsumerHandle, domain);
        }

        public int Subscribe(string topic, string expression)
        {
            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.Subscribe>(nameof(Subscribe))(this._pushConsumerHandle, topic, expression);
        }

        public int RegisterMessageCallback(MessageCallBack callback)
        {
            var pCallback = new ConsumerNativeMethodsDelegate.PtrMessageCallBack((consumer, message) => {
                var code = callback(new MessageReader(this._functionLoader, message));
                // TODO: release message ptr.
                return code;
            });
            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.RegisterMessageCallback>(nameof(RegisterMessageCallback))(this._pushConsumerHandle, pCallback);
        }

        public int UnregisterMessageCallback()
        {
            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.UnregisterMessageCallback>(nameof(UnregisterMessageCallback))(this._pushConsumerHandle);
        }

        public int RegisterMessageCallbackOrderly(MessageCallBack callback)
        {
            var pCallback = new ConsumerNativeMethodsDelegate.PtrMessageCallBack((consumer, message) => {
                var code = callback(new MessageReader(this._functionLoader, message));
                // TODO: release message ptr.
                return code;
            });
            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.RegisterMessageCallbackOrderly>(nameof(RegisterMessageCallbackOrderly))(this._pushConsumerHandle, pCallback);
        }

        public int UnregisterMessageCallbackOrderly()
        {
            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.UnregisterMessageCallbackOrderly>(nameof(UnregisterMessageCallbackOrderly))(this._pushConsumerHandle);
        }

        public int SetPushConsumerThreadCount(int threadCount)
        {
            if (threadCount <= 0) throw new ArgumentOutOfRangeException(nameof(threadCount));

            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.SetPushConsumerThreadCount>(nameof(SetPushConsumerThreadCount))(this._pushConsumerHandle, threadCount);
        }

        public int SetPushConsumerMessageBatchMaxSize(int batchSize)
        {
            if (batchSize <= 0) throw new ArgumentOutOfRangeException(nameof(batchSize));

            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.SetPushConsumerMessageBatchMaxSize>(nameof(SetPushConsumerMessageBatchMaxSize))(this._pushConsumerHandle, batchSize);
        }

        public int SetPushConsumerInstanceName(string instanceName)
        {
            if (string.IsNullOrWhiteSpace(instanceName)) throw new ArgumentNullException(nameof(instanceName));

            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.SetPushConsumerInstanceName>(nameof(SetPushConsumerInstanceName))(this._pushConsumerHandle, instanceName);
        }

        public int SetPushConsumerSessionCredentials(string accessKey, string secretKey, string channel)
        {
            if (string.IsNullOrWhiteSpace(accessKey)) throw new ArgumentNullException(nameof(accessKey));
            if (string.IsNullOrWhiteSpace(secretKey)) throw new ArgumentNullException(nameof(secretKey));
            if (string.IsNullOrWhiteSpace(channel)) throw new ArgumentNullException(nameof(channel));

            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.SetPushConsumerSessionCredentials>(nameof(SetPushConsumerSessionCredentials))(this._pushConsumerHandle, accessKey, secretKey, channel);
        }

        public int SetPushConsumerLogPath(string logPath)
        {
            if (string.IsNullOrWhiteSpace(logPath)) throw new ArgumentNullException(nameof(logPath));

            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.SetPushConsumerLogPath>(nameof(SetPushConsumerLogPath))(this._pushConsumerHandle, logPath);
        }

        public int SetPushConsumerLogFileNumAndSize(int fileNum, long fileSize)
        {
            if (fileNum <= 0) throw new ArgumentOutOfRangeException(nameof(fileNum));
            if (fileSize <= 0) throw new ArgumentOutOfRangeException(nameof(fileSize));

            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.SetPushConsumerLogFileNumAndSize>(nameof(SetPushConsumerLogFileNumAndSize))(this._pushConsumerHandle, fileNum, fileSize);
        }

        public int SetPushConsumerLogLevel(LogLevel logLevel)
        {
            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.SetPushConsumerLogLevel>(nameof(SetPushConsumerLogLevel))(this._pushConsumerHandle, logLevel);
        }

        public int SetPushConsumerMessageModel(MessageModel messageModel)
        {
            return this._functionLoader.LoadFunction<ConsumerNativeMethodsDelegate.SetPushConsumerMessageModel>(nameof(SetPushConsumerMessageModel))(this._pushConsumerHandle, messageModel);
        }
    }

    public delegate int MessageCallBack(MessageReader messageReader);
}
