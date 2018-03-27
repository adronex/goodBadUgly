package atomedition.highnoon.whitetrash

import io.netty.buffer.ByteBuf
import io.netty.buffer.Unpooled
import io.netty.buffer.UnpooledDirectByteBuf
import io.netty.channel.ChannelHandlerContext
import io.netty.channel.socket.DatagramPacket
import io.netty.util.CharsetUtil
import java.nio.ByteBuffer

class GameRoomsPool {
    companion object {
        private val updateRate = 33
        val rooms = mutableListOf<GameRoom>()

        fun initGameRoom(room: GameRoom) {
            rooms.add(room)
            room.firstTeam.add(Hero(1,30, 6, 0))
            room.secondTeam.add(Hero(1,30, 6, 127))
//            room.context1.writeAndFlush(DatagramPacket(
//                    Unpooled.copiedBuffer("QOTM: " + "huinana", CharsetUtil.UTF_8), room.receiver1))
            room.context1.writeAndFlush(DatagramPacket(encode(room), room.receiver1))
        }

        fun encode(msg: GameRoom):ByteBuf {
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
            var offset = 4
            for (i in 0 until 4) {
                bytes[i + offset] = team1[i]
            }
            offset = 8
            for (i in 0 until 4) {
                bytes[i + offset] = team2[i]
            }
            return Unpooled.copiedBuffer(bytes)
        }
    }
}