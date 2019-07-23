using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("Producer.Sample")]
[assembly: InternalsVisibleTo("PushConsumer.Sample")]

namespace RocketMQ.Client.Internal.Delegates
{
    internal static class MessageNativeMethodsDelegate
    {
        // for create message
        internal delegate IntPtr CreateMessage(string topic);

        internal delegate int DestroyMessage(HandleRef message);

        internal delegate int SetMessageTopic(HandleRef message, string topic);

        internal delegate int SetMessageTags(HandleRef message, string tags);

        internal delegate int SetMessageKeys(HandleRef message, string keys);

        internal delegate int SetMessageBody(HandleRef message, string body);

        internal delegate int SetByteMessageBody(HandleRef message, string body, int len);

        internal delegate int SetMessageProperty(HandleRef message, string key, string value);

        internal delegate int SetDelayTimeLevel(HandleRef message, int level);

        // for get message
        internal delegate IntPtr GetMessageTopic(IntPtr message);

        internal delegate IntPtr GetMessageTags(IntPtr message);

        internal delegate IntPtr GetMessageKeys(IntPtr message);

        internal delegate IntPtr GetMessageBody(IntPtr message);

        internal delegate IntPtr GetMessageProperty(IntPtr message, string key);

        internal delegate IntPtr GetMessageId(IntPtr message);

        internal delegate int GetMessageDelayTimeLevel(IntPtr message);

        internal delegate int GetMessageQueueId(IntPtr message);

        internal delegate int GetMessageReconsumeTimes(IntPtr message);

        internal delegate int GetMessageStoreSize(IntPtr message);

        internal delegate long GetMessageBornTimestamp(IntPtr message);

        internal delegate long GetMessageStoreTimestamp(IntPtr message);

        internal delegate long GetMessageQueueOffset(IntPtr message);

        internal delegate long GetMessageCommitLogOffset(IntPtr message);

        internal delegate long GetMessagePreparedTransactionOffset(IntPtr message);
    }
}
