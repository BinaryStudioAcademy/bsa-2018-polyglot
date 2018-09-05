using System.Collections.Generic;
using System.Threading.Tasks;
using Polyglot.BusinessLogic.Interfaces.SignalR;

namespace Polyglot.BusinessLogic.TranslationServices
{
    public class TranslationTimerService
    {
        private readonly List<int> _keysInProgress;

        public TranslationTimerService()
        {
            _keysInProgress = new List<int>();
        }

        public void TrackTranslatingTime(int keyId)
        {
            _keysInProgress.Add(keyId);

            //await Task.Delay(5000);

            //_keysInProgress.Remove(keyId);
        }

        public void UntrackTranslatingTime(int keyId)
        {
            _keysInProgress.Remove(keyId);
        }

        public bool IsTranslationFinished(int keyId)
        {
            return !_keysInProgress.Contains(keyId);
        }
    }
}
