package atomedition.highnoon

import io.netty.bootstrap.Bootstrap
import io.netty.buffer.ByteBuf
import io.netty.channel.ChannelHandlerContext
import io.netty.channel.ChannelInboundHandlerAdapter
import io.netty.channel.ChannelInitializer
import io.netty.channel.ChannelOption
import io.netty.channel.nio.NioEventLoopGroup
import io.netty.channel.socket.SocketChannel
import io.netty.channel.socket.nio.NioDatagramChannel
import io.netty.channel.socket.nio.NioSocketChannel
import io.netty.handler.codec.MessageToByteEncoder
import io.netty.handler.codec.ReplayingDecoder
import java.nio.charset.Charset

//fun main(args: Array<String>) {
//    NettyUdpClient().run()
//}
//
//class NettyUdpClient(private val host: String = "localhost",
//                     private val port: Int = 7788) {
//    fun run() {
//        val workerGroup = NioEventLoopGroup()
//        try {
//            val b = Bootstrap()
//                    .group(workerGroup)
//                    .channel(NioDatagramChannel::class.java)
//                    .handler(object : ChannelInitializer<NioDatagramChannel>() {
//
//                        override fun initChannel(ch: NioDatagramChannel) {
//                            ch.pipeline().addLast(
//                                    RequestDataEncoder(),
//                                    ResponseDataDecoder(),
//                                    ClientHandler())
//                        }
//                    })
//
//            val f = b.connect(host, port).sync()
//
//            f.channel().closeFuture().sync()
//        } finally {
//            workerGroup.shutdownGracefully()
//        }
//    }
//}
//
//class NettyTcpClient {
//    val host = "localhost"
//    val port = 7788
//    val workerGroup = NioEventLoopGroup()
//
//    fun run() {
//        try {
//            val b = Bootstrap()
//            b.group(workerGroup)
//            b.channel(NioSocketChannel::class.java)
//            b.option(ChannelOption.SO_KEEPALIVE, true)
//            b.handler(object : ChannelInitializer<SocketChannel>() {
//
//                public override fun initChannel(ch: SocketChannel) {
//                    ch.pipeline().addLast(
//                            RequestDataEncoder(),
//                            ResponseDataDecoder(),
//                            ClientHandler())
//                }
//            })
//
//            val f = b.connect(host, port).sync()
//
//            f.channel().closeFuture().sync()
//        } finally {
//            workerGroup.shutdownGracefully()
//        }
//    }
//}
//
//class RequestDataEncoder : MessageToByteEncoder<RequestDto>() {
//
//    private val charset = Charset.forName("UTF-8")
//
//    override fun encode(ctx: ChannelHandlerContext,
//                        msg: RequestDto,
//                        out: ByteBuf) {
//
//        out.writeInt(msg.intValue)
//        out.writeInt(msg.stringValue!!.length)
//        out.writeCharSequence(msg.stringValue, charset)
//    }
//}
//
//class ResponseDataDecoder : ReplayingDecoder<ResponseDto>() {
//
//    override fun decode(ctx: ChannelHandlerContext,
//                        bytes: ByteBuf, out: MutableList<Any>) {
//
//        val data = ResponseDto()
//        data.intValue = bytes.readInt()
//        out.add(data)
//    }
//}
//
//class ClientHandler : ChannelInboundHandlerAdapter() {
//
//    override fun channelActive(ctx: ChannelHandlerContext) {
//
//        val msg = RequestDto()
//        msg.intValue = 123
//        msg.stringValue = "all work and no play makes jack a dull boy"
//        ctx.writeAndFlush(msg)
//    }
//
//    override fun channelRead(ctx: ChannelHandlerContext, msg: Any) {
//        System.out.println("Client got response: ${msg as ResponseDto}")
//        ctx.close()
//    }
//}