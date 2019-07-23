using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using RocketMQ.Client.Internal.Delegates;

namespace RocketMQ.Client
{
    public interface IProducer
    {
        void CreateProducer(string groupId);

        int StartProducer();

        int ShutdownProducer();

        int DestroyProducer();

        int SetProducerNameServerAddress(string nameServer);

        int SetProducerNameServerDomain(string domain);

        int SetProducerGroupName(string groupName);

        int SetProducerInstanceName(string instanceName);

        int SetProducerSessionCredentials(string accessKey, string secretKey, string channel);

        int SetProducerLogPath(string logPath);

        int SetProducerLogFileNumAndSize(int fileNum, long fileSize);

        int SetProducerLogLevel(LogLevel logLevel);

        int SetProducerSendMsgTimeout(int timeout);

        int SetProducerCompressLevel(int level);

        int SetProducerMaxMessageSize(int size);

        SendResult SendMessageSync(MessageBuilder messageBuilder);

        Task<int> SendMessageAsync(MessageBuilder messageBuilder);

        int SendMessageOneway(MessageBuilder messageBuilder);

        SendResult SendMessageOrderly(
            MessageBuilder messageBuilder,
            QueueSelectorCallback callback,
            string arg,
            int autoRetryTimes
        );
    }

    public delegate int QueueSelectorCallback(int size, string message, string args);

    internal class DefaultProducer : IProducer
    {
        private readonly IFunctionLoader _functionLoader;

        ///// <summary>
        ///// This is the dynamic P/Invoke alternative
        ///// </summary>
        //private static readonly MessageBoxDelegate MessageBox;

        //static NativeMethods()
        //{
        //    MessageBox = (MessageBoxDelegate)FunctionLoader.LoadFunction<MessageBoxDelegate>(@"c:\windows\system32\user32.dll", "MessageBoxA");
        //}

        //private delegate int MessageBoxDelegate(IntPtr hwnd, string title, string message, int buttons);

        ///// <summary>
        ///// Example for a method that uses the "dynamic P/Invoke"
        ///// </summary>
        //public int ShowMessage()
        //{
        //    // 3 means "yes/no/cancel" buttons, just to show that it works...
        //    return MessageBox(IntPtr.Zero, "Hello world", "Loaded dynamically", 3);
        //}

        /// <summary>
        /// Get producer handle.
        /// </summary>
        private HandleRef _producerHandle;

        public DefaultProducer(IFunctionLoader functionLoader)
        {
            this._functionLoader = functionLoader ?? throw new ArgumentNullException(nameof(functionLoader));
        }

        public void CreateProducer(string groupId)
        {
            var ptr = this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.CreateProducerDelegate>(nameof(CreateProducer))(groupId);

            this._producerHandle = new HandleRef(this, ptr);
        }

        public int StartProducer()
        {
            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.StartProducer>(nameof(StartProducer))(this._producerHandle);
        }

        public int ShutdownProducer()
        {
            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.ShutdownProducer>(nameof(ShutdownProducer))(this._producerHandle);
        }

        public int DestroyProducer()
        {
            var code = this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.DestroyProducer>(nameof(DestroyProducer))(this._producerHandle);

            this._producerHandle = new HandleRef(null, IntPtr.Zero);

            return code;
        }

        public int SetProducerNameServerAddress(string nameServer)
        {
            if (string.IsNullOrWhiteSpace(nameServer))
            {
                throw new ArgumentException("{0} can not null or empty.", nameof(nameServer));
            }

            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SetProducerNameServerAddress>(nameof(SetProducerNameServerAddress))(this._producerHandle, nameServer);
        }

        public int SetProducerNameServerDomain(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                throw new ArgumentException("{0} can not null or empty.", nameof(domain));
            }

            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SetProducerNameServerDomain>(nameof(SetProducerNameServerDomain))(this._producerHandle, domain);
        }

        public int SetProducerGroupName(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new ArgumentException("{0} can not null or empty.", nameof(groupName));
            }

            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SetProducerGroupName>(nameof(SetProducerGroupName))(this._producerHandle, groupName);
        }

        public int SetProducerInstanceName(string instanceName)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                throw new ArgumentException("{0} can not null or empty.", nameof(instanceName));
            }

            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SetProducerInstanceName>(nameof(SetProducerInstanceName))(this._producerHandle, instanceName);
        }

        public int SetProducerSessionCredentials(string accessKey, string secretKey, string channel)
        {
            if (string.IsNullOrWhiteSpace(accessKey))
            {
                throw new ArgumentException("{0} can not null or empty.", nameof(accessKey));
            }

            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new ArgumentException("{0} can not null or empty.", nameof(secretKey));
            }

            if (string.IsNullOrWhiteSpace(channel))
            {
                throw new ArgumentException("{0} can not null or empty.", nameof(channel));
            }

            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SetProducerSessionCredentials>(nameof(SetProducerSessionCredentials))(this._producerHandle, accessKey, secretKey, channel);
        }

        public int SetProducerLogPath(string logPath)
        {
            if (string.IsNullOrWhiteSpace(logPath))
            {
                throw new ArgumentException("{0} can not null or empty.", nameof(logPath));
            }

            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SetProducerLogPath>(nameof(SetProducerLogPath))(this._producerHandle, logPath);
        }

        public int SetProducerLogFileNumAndSize(int fileNum, long fileSize)
        {
            if (fileNum <= 0) throw new ArgumentOutOfRangeException(nameof(fileNum));
            if (fileSize <= 0) throw new ArgumentOutOfRangeException(nameof(fileSize));

            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SetProducerLogFileNumAndSize>(nameof(SetProducerLogFileNumAndSize))(this._producerHandle, fileNum, fileSize);
        }

        public int SetProducerLogLevel(LogLevel logLevel)
        {
            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SetProducerLogLevel>(nameof(SetProducerLogLevel))(this._producerHandle, logLevel);
        }

        public int SetProducerSendMsgTimeout(int timeout)
        {
            if (timeout <= 0) throw new ArgumentOutOfRangeException(nameof(timeout));

            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SetProducerSendMsgTimeout>(nameof(SetProducerSendMsgTimeout))(this._producerHandle, timeout);
        }

        public int SetProducerCompressLevel(int level)
        {
            if (level < 0) throw new ArgumentOutOfRangeException(nameof(level));

            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SetProducerCompressLevel>(nameof(SetProducerCompressLevel))(this._producerHandle, level);
        }

        public int SetProducerMaxMessageSize(int size)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));

            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SetProducerMaxMessageSize>(nameof(SetProducerMaxMessageSize))(this._producerHandle, size);
        }

        public SendResult SendMessageSync(MessageBuilder messageBuilder)
        {
            var code = this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SendMessageSync>(nameof(SendMessageSync))(this._producerHandle, messageBuilder.Build(), out var result);

            return new SendResult {
                SendStatus = result.sendStatus,
                MsgId = result.msgId,
                Offset = result.offset,
                ReturnCode = code
            };
        }

        public Task<int> SendMessageAsync(MessageBuilder messageBuilder)
        {
            var taskCompletionSource = new TaskCompletionSource<int>();
            var successCallback = new ProducerNativeMethodsDelegate.CSendSuccessCallback(result => {
                taskCompletionSource.SetResult(0);
            });
            var exceptionCallback = new ProducerNativeMethodsDelegate.CSendExceptionCallback(exception => {
                taskCompletionSource.SetException(new Exception($"error:{exception.error}, type:{exception.type}, msg:{exception.msg}, line:{exception.line}, file:{exception.file}"));
            });

            this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SendMessageAsync>(nameof(SendMessageAsync))(this._producerHandle, messageBuilder.Build(), successCallback, exceptionCallback);

            return taskCompletionSource.Task;
        }

        public int SendMessageOneway(MessageBuilder messageBuilder)
        {
            return this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SendMessageOneway>(nameof(SendMessageOneway))(this._producerHandle, messageBuilder.Build());
        }

        public SendResult SendMessageOrderly(
            MessageBuilder messageBuilder,
            QueueSelectorCallback callback,
            string arg,
            int autoRetryTimes
        )
        {
            if (autoRetryTimes < 0) throw new ArgumentOutOfRangeException(nameof(autoRetryTimes));

            var pCallback = new ProducerNativeMethodsDelegate.CQueueSelectorCallback((size, message, args) => {
                return callback(
                    size,
                    message == IntPtr.Zero ? string.Empty : Marshal.PtrToStringAnsi(message),
                    args == IntPtr.Zero ? string.Empty : Marshal.PtrToStringAnsi(args)
                );
            });
            var code = this._functionLoader.LoadFunction<ProducerNativeMethodsDelegate.SendMessageOrderly>(nameof(SendMessageOrderly))(
                this._producerHandle,
                messageBuilder.Build(),
                pCallback,
                Marshal.StringToBSTR(arg??string.Empty),
                autoRetryTimes,
                out var result
            );

            return new SendResult {
                ReturnCode = code,
                SendStatus = result.sendStatus,
                MsgId = result.msgId,
                Offset = result.offset
            };
        }
    }
}
