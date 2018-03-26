package atomedition.highnoon

import io.netty.bootstrap.Bootstrap
import io.netty.channel.ChannelOption
import io.netty.channel.nio.NioEventLoopGroup
import io.netty.channel.socket.nio.NioDatagramChannel

fun main(args: Array<String>) {
    QotmServer().run()
}

class QotmServer {

    private val PORT = Integer.parseInt(System.getProperty("port", "7686"))

    fun run() {
        val group = NioEventLoopGroup()
        try {
            val b = Bootstrap()
            b.group(group)
                    .channel(NioDatagramChannel::class.java)
                    .option(ChannelOption.SO_BROADCAST, true)
                    .handler(QotmServerHandler())

            b.bind(PORT).sync().channel().closeFuture().await()
        } finally {
            group.shutdownGracefully()
        }
    }
}