using System;
using RocketMQ.Client;
using RocketMQ.Client.Internal;

namespace Producer.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Producer";

            var functionLoader = new FunctionLoader();
            var producer = new DefaultProducer(functionLoader);
            using (var messageBuilder = new MessageBuilder(functionLoader))
            {

                producer.CreateProducer("xxx");

                var code1 = producer.SetProducerNameServerAddress("47.101.55.250:9876");
                var code2 = producer.SetProducerLogLevel(LogLevel.Debug);
                var code3 = producer.StartProducer();

                Console.WriteLine("press any key to send message.");
                Console.ReadKey(true);
                messageBuilder.CreateMessage("test");
                var code4 = messageBuilder.SetMessageBody("hello" + Guid.NewGuid().ToString("N"));
                //var code5 = messageBuilder.SetMessageTags("xxxxfff中国");
                //var code6 = messageBuilder.SetMessageKeys($"123:{Guid.NewGuid():N}");
                //var code7 = messageBuilder.SetMessageProperty("key1", "value1");
                //var code8 = messageBuilder.SetDelayTimeLevel(1);

                var result1 = producer.SendMessageSync(messageBuilder);

                //在 cpp sdk 未找到此方法
                //var result2 = producer.SendMessageAsync(messageBuilder).GetAwaiter().GetResult();

                //var result3 = producer.SendMessageOneway(messageBuilder);

                //var result4 = producer.SendMessageOrderly(messageBuilder, (size, message, arg) => { return 0; }, string.Empty, 1);
            }

            var code20 = producer.ShutdownProducer();
            var code21 = producer.DestroyProducer();

        }
    }
}
