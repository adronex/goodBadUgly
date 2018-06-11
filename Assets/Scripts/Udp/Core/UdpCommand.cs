namespace Audio
{
    public enum UdpCommand : byte
    {
        // client commands
        FindGame = 1,
        GetGameState = 2,
        SetArmAngle = 3,
        Shoot = 4,
        
        // server commands
        GameFound = 5,
        UpdateGameState = 6
    }
}