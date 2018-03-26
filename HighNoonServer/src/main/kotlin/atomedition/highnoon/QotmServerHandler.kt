package atomedition.highnoon

import atomedition.highnoon.whitetrash.GameRoom
import atomedition.highnoon.whitetrash.GameRoomsPool
import io.netty.channel.SimpleChannelInboundHandler
import io.netty.channel.socket.DatagramPacket
import io.netty.channel.ChannelHandlerContext
import io.netty.util.CharsetUtil
import io.netty.buffer.Unpooled
import java.util.Random

class QotmServerHandler : SimpleChannelInboundHandler<DatagramPacket>() {
    private val random = Random()

    // Quotes from Mohandas K. Gandhi:
    private val quotes = arrayOf(
            "Where there is love there is life.",
            "First they ignore you, then they laugh at you, then they fight you, then you win.",
            "Be the change you want to see in the world.",
            "The weak can never forgive. Forgiveness is the attribute of the strong."
    )

    private fun nextQuote(): String {
        var quoteId: Int = 0
        synchronized(random) {
            quoteId = random.nextInt(quotes.size)
        }
        return quotes[quoteId]
    }

    public override fun channelRead0(ctx: ChannelHandlerContext, packet: DatagramPacket) {
        System.err.println(packet)
        if ("QOTM?" == packet.content().toString(CharsetUtil.UTF_8)) {
            ctx.write(DatagramPacket(
                    Unpooled.copiedBuffer("QOTM: " + nextQuote(), CharsetUtil.UTF_8), packet.sender()))
        } else {
            GameRoomsPool.initGameRoom(GameRoom(packet.sender(), ctx))
        }
    }

    override fun channelReadComplete(ctx: ChannelHandlerContext) {
        ctx.flush()
    }

    override fun exceptionCaught(ctx: ChannelHandlerContext, cause: Throwable) {
        cause.printStackTrace()
        // We don't close the channel because we can keep serving requests.
    }
}