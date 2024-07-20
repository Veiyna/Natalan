namespace WorldServer.Network.Connection;

public class GameConnection
{
    private WorldSession WorldSession { get; set; }
    private ChatSession ChatSession { get; set; }

    public void SetWorldSession(WorldSession worldSession)
    {
        this.WorldSession = worldSession;
        if (this.ChatSession is not null)
        {
            this.ChatSession.Player = this.WorldSession.Player;
            this.ChatSession.Player.ChatSession = this.ChatSession;
        }
    }

    public void SetChatSession(ChatSession chatSession)
    {
        this.ChatSession = chatSession;
        if (this.WorldSession is not null)
        {
            this.ChatSession.Player = this.WorldSession.Player;
            this.ChatSession.Player.ChatSession = this.ChatSession;
        }

    }
    
    
}