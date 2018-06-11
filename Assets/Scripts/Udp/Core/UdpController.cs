using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Audio
{
    public abstract class UdpController
    {
        protected UdpClient Client;
        public delegate void ReceiveEventDelegate(UdpDatagram datagram);
        public event ReceiveEventDelegate OnReceive = delegate {  };

        protected async Task<UdpDatagram> ReceiveBytes()
        {
            var result = await Client.ReceiveAsync();
            var command = (UdpCommand)result.Buffer[0];
            return new UdpDatagram(result.RemoteEndPoint, command, result.Buffer.Skip(1).ToArray());
        }

        protected void TriggerReceivedEvent(UdpDatagram udpDatagram)
        {
            OnReceive(udpDatagram);
        }
    }
}