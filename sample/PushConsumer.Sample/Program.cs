using System;
using System.Threading.Tasks;
using RocketMQ.Client.Consumer;
using RocketMQ.Client.Internal;

namespace PushConsumer.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "PushConsumer";

            Task.Run(async () =>
            {
                var functionLoader = new FunctionLoader();
                var consumer = new DefaultPushConsumer(functionLoader);

                consumer.CreatePushConsumer("xxx");

                var groupId = consumer.GetPushConsumerGroupID();
                var code1 = consumer.SetPushConsumerNameServerAddress("47.101.55.250:9876");
                var code2 = consumer.Subscribe("test", "*");

//                var code3 = consumer.RegisterMessageCallback(messageReader =>
//                {
//                    Console.WriteLine($@"RegisterMessageCallback:
//        topic: {messageReader.GetMessageTopic()}
//         tags: {messageReader.GetMessageTags()}
//         keys: {messageReader.GetMessageKeys()}
//         body: {messageReader.GetMessageBody()}
//property key1: {messageReader.GetMessageProperty("key1")}
//   message id: {messageReader.GetMessageId()}
//___________________________________________________________________________________________");
//                    return 0;
//                });

                var code4 = consumer.RegisterMessageCallbackOrderly(messageReader =>
                {
                    Console.WriteLine($@"RegisterMessageCallbackOrderly:
        topic: {messageReader.GetMessageTopic()}
         tags: {messageReader.GetMessageTags()}
         keys: {messageReader.GetMessageKeys()}
         body: {messageReader.GetMessageBody()}
property key1: {messageReader.GetMessageProperty("key1")}
   message id: {messageReader.GetMessageId()}
___________________________________________________________________________________________");
                    return 0;
                });

                var code15 = consumer.StartPushConsumer();

                while (true)
                {
                    await Task.Delay(1000);
                }
            });

            Console.ReadKey(true);
        }
    }
}
