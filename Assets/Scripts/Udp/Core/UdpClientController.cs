using System.Net.Sockets;
using System.Threading.Tasks;

namespace Audio
{
    public class UdpClientController : UdpController
    {
        public UdpClientController(string address, int port)
        {
            Client = new UdpClient(address, port);
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var received = await ReceiveBytes();
                    TriggerReceivedEvent(received);
                }
            });
        }

        public void SendCommand(UdpCommand command, byte[] data)
        {
            var bytes = new byte[data.Length + 1];
            bytes[0] = (byte)command;
            data.CopyTo(bytes, 1);
            Client.Send(bytes, bytes.Length);
        }
    }
}