package atomedition.highnoon

import io.netty.channel.ChannelHandlerContext
import io.netty.handler.codec.MessageToMessageEncoder
import java.nio.ByteBuffer

class GameRoomEncoder: MessageToMessageEncoder<GameRoom>() {

    override fun encode(ctx: ChannelHandlerContext, msg: GameRoom, out: MutableList<Any>) {
        val bytes = ByteArray(Integer.BYTES + msg.firstTeam.size * 4 + msg.secondTeam.size * 4)
        val currentMoment = ByteBuffer
                .allocate(Integer.BYTES)
                .putInt(msg.currentMoment)
                .array()
        val team1 = ByteBuffer.allocate(4)
                .put(msg.firstTeam[0].id)
                .put(msg.firstTeam[0].health)
                .put(msg.firstTeam[0].ammo)
                .put(msg.firstTeam[0].handAngle)
                .array()
        val team2 = ByteBuffer.allocate(4)
                .put(msg.secondTeam[0].id)
                .put(msg.secondTeam[0].health)
                .put(msg.secondTeam[0].ammo)
                .put(msg.secondTeam[0].handAngle)
                .array()
        for (i in 0 until Integer.BYTES) {
            bytes[i] = currentMoment[i]
        }
        for (i in 4 until 8) {
            bytes[i] = team1[i]
        }
        for (i in 9 until 12) {
            bytes[i] = team1[i]
        }
        out.add(bytes.toString())
    }
}