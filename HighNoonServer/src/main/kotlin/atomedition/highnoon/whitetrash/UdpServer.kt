package atomedition.highnoon.whitetrash

import io.netty.bootstrap.Bootstrap
import io.netty.channel.ChannelInitializer
import io.netty.channel.nio.NioEventLoopGroup
import io.netty.channel.socket.nio.NioDatagramChannel

class UdpServer(private val port: Int = 7798) {
    fun run() {
        val workerGroup = NioEventLoopGroup()
        try {
            val bootstrap = Bootstrap()
                    .group(workerGroup)
                    .channel(NioDatagramChannel::class.java)
                    .handler(object : ChannelInitializer<NioDatagramChannel>() {
                        override fun initChannel(ch: NioDatagramChannel) {
                            ch.pipeline().addLast(
                                    GameRoomEncoder(),
                                    GameEventHandler()
                            )
                        }
                    })
            val f = bootstrap.bind(port).sync()
            f.channel().closeFuture().sync()
        } finally {
            workerGroup.shutdownGracefully()
        }
    }
}