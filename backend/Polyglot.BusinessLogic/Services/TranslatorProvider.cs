using System.Threading.Tasks;
using Polyglot.BusinessLogic.Interfaces;

namespace Polyglot.BusinessLogic.Services
{
    class TranslatorProvider : ITranslatorProvider
    {
        private readonly string _key; 

        public TranslatorProvider(string providerKey)
        {
            _key = providerKey;
        }

        public async Task<string> Translate(string text, string source, string target)
        {

            return "";
        }
    }
}
