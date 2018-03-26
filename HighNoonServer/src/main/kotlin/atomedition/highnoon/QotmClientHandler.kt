package atomedition.highnoon

import io.netty.channel.ChannelHandlerContext
import io.netty.util.CharsetUtil
import io.netty.channel.SimpleChannelInboundHandler
import io.netty.channel.socket.DatagramPacket


class QuoteOfTheMomentClientHandler : SimpleChannelInboundHandler<DatagramPacket>() {

    override fun channelRead0(ctx: ChannelHandlerContext, msg: DatagramPacket) {
        val response = msg.content().toString(CharsetUtil.UTF_8)
        if (response.startsWith("QOTM: ")) {
            println("Quote of the Moment: " + response.substring(6))
            ctx.close()
        }
    }

    override fun exceptionCaught(ctx: ChannelHandlerContext, cause: Throwable) {
        cause.printStackTrace()
        ctx.close()
    }
}