import io.netty.bootstrap.ServerBootstrap
import io.netty.buffer.ByteBuf
import io.netty.channel.*
import io.netty.channel.nio.NioEventLoopGroup
import io.netty.channel.socket.SocketChannel
import io.netty.channel.socket.nio.NioServerSocketChannel
import io.netty.handler.codec.MessageToByteEncoder
import io.netty.handler.codec.ReplayingDecoder
import java.nio.charset.Charset

fun main(args: Array<String>) {
    NettyServer(8888).run()
}

class NettyServer(private val port: Int = 7788) {
    fun run() {
        val bossGroup = NioEventLoopGroup()
        val workerGroup = NioEventLoopGroup()
        try {
            val bootstrap = ServerBootstrap()
            bootstrap.group(bossGroup, workerGroup)
                    .channel(NioServerSocketChannel::class.java)
                    .childHandler(object : ChannelInitializer<SocketChannel>() {
                        public override fun initChannel(ch: SocketChannel) {
                            ch.pipeline().addLast(
                                    RequestDecoder(),
                                    ResponseDataEncoder(),
                                    ProcessingHandler()
                            )
                        }
                    })
                    .option(ChannelOption.SO_BACKLOG, 128)
                    .childOption(ChannelOption.SO_KEEPALIVE, true)

            val f = bootstrap.bind(port).sync()
            f.channel().closeFuture().sync()
        } finally {
            workerGroup.shutdownGracefully()
            bossGroup.shutdownGracefully()
        }
    }
}
class RequestDecoder : ReplayingDecoder<RequestDto>() {

    private val charset = Charset.forName("UTF-8")

    override fun decode(ctx: ChannelHandlerContext,
                        bytes: ByteBuf,
                        out: MutableList<Any>) {

        val data = RequestDto()
        data.intValue = bytes.readInt()
        val strLen = bytes.readInt()
        data.stringValue = bytes.readCharSequence(strLen, charset).toString()
        out.add(data)
    }
}

class ProcessingHandler : ChannelInboundHandlerAdapter() {

    override fun channelRead(ctx: ChannelHandlerContext, msg: Any) {
        val requestDto = msg as RequestDto
        val responseDto = ResponseDto()
        responseDto.intValue = requestDto.intValue * 2
        val future = ctx.writeAndFlush(responseDto)
        future.addListener(ChannelFutureListener.CLOSE)
        System.out.println(requestDto)
    }
}

class ResponseDataEncoder : MessageToByteEncoder<ResponseDto>() {

    override fun encode(ctx: ChannelHandlerContext,
                        msg: ResponseDto,
                        out: ByteBuf) {
        out.writeInt(msg.intValue)
    }
}

data class RequestDto(var intValue: Int = 0,
                      var stringValue: String? = null)

data class ResponseDto(var intValue: Int = 0)