using System;
using System.Collections.Generic;
using System.Text;

namespace CardsCreator.DomainModel.Interfaces
{
    public interface ILanguageTypeConverter
    {
        LanguageType Convert(string langTypeText);
        string Convert(LanguageType type);
    }
}
