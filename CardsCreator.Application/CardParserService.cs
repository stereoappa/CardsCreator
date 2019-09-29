using CardsCreator.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardsCreator.Application
{
    public interface ICardParserService
    {
        List<Card> Parse(string text, LanguageType sideOneLanguage, LanguageType sideTwoLanguage, string separator = "-");
    }
    public class CardParserService : ICardParserService
    {
        public List<Card> Parse(string text, LanguageType sideOneLanguage, LanguageType sideTwoLanguage, string separator = "-")
        {
            return new List<Card>
            {
                new Card(LanguageType.En, LanguageType.Ru, "Good", "Job")
            };
        }
    }
}
