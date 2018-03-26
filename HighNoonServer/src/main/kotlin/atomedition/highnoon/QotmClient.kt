package atomedition.highnoon

import io.netty.bootstrap.Bootstrap
import io.netty.buffer.Unpooled
import io.netty.channel.ChannelOption
import io.netty.channel.nio.NioEventLoopGroup
import io.netty.channel.socket.DatagramPacket
import io.netty.channel.socket.nio.NioDatagramChannel
import io.netty.util.CharsetUtil
import io.netty.util.internal.SocketUtils

fun main(args: Array<String>) {
    QotmClient().run()
}

class QotmClient {

    val PORT = Integer.parseInt(System.getProperty("port", "7686"))

    fun run() {

        val group = NioEventLoopGroup()
        try {
            val b = Bootstrap()
            b.group(group)
                    .channel(NioDatagramChannel::class.java)
                    .option(ChannelOption.SO_BROADCAST, true)
                    .handler(QuoteOfTheMomentClientHandler())

            val ch = b.bind(0).sync().channel()

            // Broadcast the QOTM request to port 8080.
            ch.writeAndFlush(DatagramPacket(
                    Unpooled.copiedBuffer("QOTM?", CharsetUtil.UTF_8),
                    SocketUtils.socketAddress("255.255.255.255", PORT))).sync()

            // QuoteOfTheMomentClientHandler will close the DatagramChannel when a
            // response is received.  If the channel is not closed within 5 seconds,
            // print an error message and quit.
            if (!ch.closeFuture().await(5000)) {
                System.err.println("QOTM request timed out.")
            }
        } finally {
            group.shutdownGracefully()
        }
    }
}