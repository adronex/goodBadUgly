using System;
using Audio;
using UnityEngine;

public class UdpServerAttacher : MonoBehaviour
{
    private UdpServerController _server;

    // Use this for initialization
    void Start()
    {
        _server = new UdpServerController(9933);
        _server.OnReceive += Handle;
    }

    void Handle(UdpDatagram datagram)
    {
        switch (datagram.Command)
        {
            case UdpCommand.FindGame:
                break;
            case UdpCommand.GetGameState:
                break;
            case UdpCommand.SetArmAngle:
                break;
            case UdpCommand.Shoot:
                break;
            default:
                throw new ArgumentException("Server can't handle command: " + datagram.Command);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}