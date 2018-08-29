namespace Polyglot.Core.SignalR.Responses
{
    public interface IResponse
    {
        int SenderId { get; set; }

        string SenderFullName { get; set; }
    }
}
