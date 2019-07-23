using System;
using System.Runtime.InteropServices;
using RocketMQ.Client.Internal.Delegates;

namespace RocketMQ.Client
{
    public class MessageBuilder : IDisposable
    {
        private readonly IFunctionLoader _functionLoader;

        private HandleRef _handle;

        public MessageBuilder(IFunctionLoader functionLoader)
        {
            this._functionLoader = functionLoader ?? throw new ArgumentNullException(nameof(functionLoader));
        }

        public void CreateMessage(string topic)
        {
            var messagePtr = this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.CreateMessage>(nameof(CreateMessage))(topic);

            this._handle = new HandleRef(this, messagePtr);
        }

        public int DestroyMessage()
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.DestroyMessage>(nameof(DestroyMessage))(this._handle);
        }

        public int SetMessageTopic(string topic)
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.SetMessageTopic>(nameof(SetMessageTopic))(this._handle, topic);
        }

        public int SetMessageTags(string tags)
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.SetMessageTags>(nameof(SetMessageTags))(this._handle, tags);
        }

        public int SetMessageKeys(string keys)
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.SetMessageKeys>(nameof(SetMessageKeys))(this._handle, keys);
        }

        public int SetMessageBody(string body)
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.SetMessageBody>(nameof(SetMessageBody))(this._handle, body);
        }

        public int SetByteMessageBody(string body, int len)
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.SetByteMessageBody>(nameof(SetByteMessageBody))(this._handle, body, len);
        }

        public int SetMessageProperty(string key, string value)
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.SetMessageProperty>(nameof(SetMessageProperty))(this._handle, key, value);
        }

        public int SetDelayTimeLevel(int level)
        {
            return this._functionLoader.LoadFunction<MessageNativeMethodsDelegate.SetDelayTimeLevel>(nameof(SetDelayTimeLevel))(this._handle, level);
        }

        public HandleRef Build() => this._handle;

        public void Dispose()
        {
            if (this._handle.Handle != IntPtr.Zero)
            {
                this.DestroyMessage();
                this._handle = new HandleRef(null, IntPtr.Zero);
            }
        }
    }
}
