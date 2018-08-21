using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.TranslationServices
{
    interface ITranslatorProvider
    {
         Task<string> Translate(TextForTranslation item);
    }
}
