using System.Runtime.InteropServices;

namespace RocketMQ.Client.Internal
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct CSendResult
    {
        public int sendStatus;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string msgId;

        public long offset;
    }
}
