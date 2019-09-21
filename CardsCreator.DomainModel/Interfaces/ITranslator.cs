using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardsCreator.DomainModel.Interfaces
{
    public interface ITranslator
    {
        Task<LanguageType> GetLanguage(string text);

        Task<string> GetTranslate(string text, LanguageType targetLanguage);
    }
}
