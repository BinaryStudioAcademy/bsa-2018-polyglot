namespace Polyglot.Common.Helpers.SignalR
{
    public enum ChatAction
    {
        messageReceived,
        messageRead,
        typing,
        personConnected,
        personDisconnected,
        dialogsChanged,
        userStateUpdated
    }
}
