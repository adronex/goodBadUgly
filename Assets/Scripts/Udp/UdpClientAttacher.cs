using System;
using Audio;
using UnityEngine;

public class UdpClientAttacher : MonoBehaviour
{
    private UdpClientController _client;

    // Use this for initialization
    void Start()
    {
        _client = new UdpClientController("127.0.0.1", 9933);
        _client.OnReceive += Handle;
    }

    void Handle(UdpDatagram datagram)
    {
        switch (datagram.Command)
        {
            case UdpCommand.GameFound:
                // load game scene
                break;
            case UdpCommand.UpdateGameState:
                // get game state and set new values
                break;
            default:
                throw new ArgumentException("Client can't handle command: " + datagram.Command);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}