using System;
using System.Collections.Generic;
using System.Text;

namespace CardsCreator.DomainModel.Interfaces
{
    public interface ICardParserService
    {
        IEnumerable<Card> Parse(LanguageType sideOneLanguage,
                                LanguageType sideTwoLanguage, 
                                string cardsText, 
                                string separator);
    }
}
