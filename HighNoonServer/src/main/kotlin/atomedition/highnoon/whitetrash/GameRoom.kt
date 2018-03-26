package atomedition.highnoon.whitetrash

import io.netty.channel.ChannelHandlerContext
import java.net.InetSocketAddress

data class Hero(
        val id: Byte,
        val health: Byte,
        val ammo: Byte,
        val handAngle: Byte
)

enum class EventType{
    SHOOT, HIT
}

data class EventLog(
        val eventType: EventType,
        val timeStamp: Int
)

class GameRoom(val receiver1: InetSocketAddress,
               val context1: ChannelHandlerContext) {
    val firstTeam = mutableListOf<Hero>()
    val secondTeam = mutableListOf<Hero>()
    val lastActionsLog = Array<EventType?>(2,  { null })
    val currentMoment: Int = 0
}