package atomedition.highnoon.whitetrash

fun main(args: Array<String>) {
    UdpServer().run()
}

class GameRoomsPool {
    companion object {
        private val updateRate = 33
        val rooms = mutableListOf<GameRoom>()

        fun initGameRoom(room: GameRoom) {
            rooms.add(room)
            room.receiver1.pipeline().writeAndFlush(room)
        }
    }
}