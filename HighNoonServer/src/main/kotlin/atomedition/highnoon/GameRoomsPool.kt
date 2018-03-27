package atomedition.highnoon

import atomedition.highnoon.whitetrash.GameRoom
import atomedition.highnoon.whitetrash.Hero
import io.netty.buffer.ByteBuf
import io.netty.buffer.Unpooled
import io.netty.channel.socket.DatagramPacket
import io.netty.util.HashedWheelTimer
import java.nio.ByteBuffer

class GameRoomsPool {
    companion object {
        private val updateRate = 33
        private val timer = HashedWheelTimer()
        val rooms = mutableListOf<GameRoom>()

        fun initGameRoom(room: GameRoom) {
            rooms.add(room)
            room.firstTeam.add(Hero(1, 30, 6, 0))
            room.secondTeam.add(Hero(2, 30, 6, 127))
            for (i in 0..5) {
                room.context1.writeAndFlush(DatagramPacket(encode(room), room.receiver1))
                room.currentMoment += updateRate

                Thread.sleep(updateRate.toLong())
            }
            room.context1.writeAndFlush(DatagramPacket(Unpooled.copiedBuffer(ByteArray(1, {2})), room.receiver1))
        }

        fun encode(msg: GameRoom):ByteBuf {
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
            var offset = 0
            val bytes = ByteArray(Integer.BYTES + msg.firstTeam.size * 4 + msg.secondTeam.size * 4 + 1)
            bytes[offset] = 1
            offset += 1
            for (i in 0 until Integer.BYTES) {
                bytes[i + offset] = currentMoment[i]
            }
            offset += 4
            for (i in 0 until 4) {
                bytes[i + offset] = team1[i]
            }
            offset += 4
            for (i in 0 until 4) {
                bytes[i + offset] = team2[i]
            }
            return Unpooled.copiedBuffer(bytes)
        }
    }
}