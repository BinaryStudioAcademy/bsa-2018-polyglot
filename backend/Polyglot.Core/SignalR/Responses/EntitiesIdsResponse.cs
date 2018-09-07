namespace Polyglot.Core.SignalR.Responses
{
    public class EntitiesIdsResponce : IResponse
    {
        public int SenderId { get; set; }

        public string SenderFullName { get; set; }

        public int[] Ids { get; set; }
    }
}
