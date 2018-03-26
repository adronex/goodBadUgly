package atomedition.highnoon.whitetrash

import io.netty.channel.ChannelHandlerContext
import io.netty.channel.ChannelInboundHandlerAdapter

class GameEventHandler: ChannelInboundHandlerAdapter() {

//    override fun channelActive(ctx: ChannelHandlerContext) {
//    }

    override fun channelRead(ctx: ChannelHandlerContext, msg: Any) {
        GameRoomsPool.initGameRoom(GameRoom(ctx))
    }
}