using System;
using System.Runtime.InteropServices;
using RocketMQ.Client.Internal.Delegates;

namespace RocketMQ.Client.Consumer.Internal
{
    public class MessageReader
    {
        private readonly IFunctionLoader _functionLoader;
        private readonly IntPtr _messagePtr;

        internal MessageReader(IFunctionLoader functionLoader, IntPtr messagePtr)
        {
            this._functionLoader = functionLoader ?? throw new ArgumentNullException(nameof(functionLoader));

            if (messagePtr == IntPtr.Zero) throw new ArgumentOutOfRangeException(nameof(messagePtr));

            this._messagePtr = messagePtr;
        }

        public string GetMessageTopic()
        {
            var ptr = this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageTopic>(nameof(GetMessageTopic))(this._messagePtr);

            return PtrToString(ptr);
        }

        public string GetMessageTags()
        {
            var ptr = this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageTags>(nameof(GetMessageTags))(this._messagePtr);

            return PtrToString(ptr);
        }

        public string GetMessageKeys()
        {
            var ptr = this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageKeys>(nameof(GetMessageKeys))(this._messagePtr);

            return PtrToString(ptr);
        }

        public string GetMessageBody()
        {
            var ptr = this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageBody>(nameof(GetMessageBody))(this._messagePtr);

            return PtrToString(ptr);
        }

        public string GetMessageProperty(string key)
        {
            var ptr = this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageProperty>(nameof(GetMessageProperty))(this._messagePtr, key);

            return PtrToString(ptr);
        }

        public string GetMessageId()
        {
            var ptr = this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageId>(nameof(GetMessageId))(this._messagePtr);

            return PtrToString(ptr);
        }

        public int GetMessageDelayTimeLevel()
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageDelayTimeLevel>(nameof(GetMessageDelayTimeLevel))(this._messagePtr);
        }

        public int GetMessageQueueId()
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageQueueId>(nameof(GetMessageQueueId))(this._messagePtr);
        }

        public int GetMessageReconsumeTimes()
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageReconsumeTimes>(nameof(GetMessageReconsumeTimes))(this._messagePtr);
        }

        public int GetMessageStoreSize()
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageStoreSize>(nameof(GetMessageStoreSize))(this._messagePtr);
        }

        public long GetMessageStoreTimestamp()
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageStoreTimestamp>(nameof(GetMessageStoreTimestamp))(this._messagePtr);
        }

        public long GetMessageBornTimestamp()
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageBornTimestamp>(nameof(GetMessageBornTimestamp))(this._messagePtr);
        }

        public long GetMessageQueueOffset()
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageQueueOffset>(nameof(GetMessageQueueOffset))(this._messagePtr);
        }

        public long GetMessageCommitLogOffset()
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessageCommitLogOffset>(nameof(GetMessageCommitLogOffset))(this._messagePtr);
        }

        public long GetMessagePreparedTransactionOffset()
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.GetMessagePreparedTransactionOffset>(nameof(GetMessagePreparedTransactionOffset))(this._messagePtr);
        }

        private static string PtrToString(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero) return string.Empty;

            var str = Marshal.PtrToStringAnsi(ptr);

            return str;
        }
    }
}
