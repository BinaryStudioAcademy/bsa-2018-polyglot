using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Interfaces
{
    interface ITranslatorProvider
    {
         Task<string> Translate(string text, string source, string target);
    }
}
