using System.Net;

namespace Audio
{
    public class UdpDatagram
    {
        public UdpDatagram(IPEndPoint ipEndPoint, UdpCommand command, byte[] data)
        {
            IpEndPoint = ipEndPoint;
            Command = command;
            Data = data;
        }

        public IPEndPoint IpEndPoint { get; }

        public UdpCommand Command { get; }

        public byte[] Data { get; }
    }
}