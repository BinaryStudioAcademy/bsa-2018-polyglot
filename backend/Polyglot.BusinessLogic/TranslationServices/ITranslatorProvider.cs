using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.TranslationServices
{
    public interface ITranslatorProvider
    {
         Task<string> Translate(TextForTranslation item);
    }
}
