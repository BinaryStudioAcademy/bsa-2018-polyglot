namespace Polyglot.Core.SignalR.Responses
{
    public class ChatMessageResponce : IResponse
    {
        public int SenderId { get; set; }

        public string SenderFullName { get; set; }

        public int DialogId { get; set; }

        public int MessageId { get; set; }

        public string Text { get; set; }
    }
}
