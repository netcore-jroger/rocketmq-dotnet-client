namespace RocketMQ.Client
{
    public class SendResult
    {
        public int SendStatus { get; internal set; }

        public string MsgId { get; internal set; }

        public long Offset { get; internal set; }

        public int ReturnCode { get; internal set; }
    }
}
