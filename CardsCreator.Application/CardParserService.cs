using CardsCreator.DomainModel;
using CardsCreator.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CardsCreator.Application
{

    public class CardParserService : ICardParserService
    {

        public IEnumerable<Card> Parse(LanguageType sideOneLanguage, LanguageType sideTwoLanguage, string cardsText, string separator = "-")
        {
            var cardRegex = new Regex(
                CardPatternBuilder.Build(sideOneLanguage, sideTwoLanguage, separator), 
                RegexOptions.Multiline);

            foreach (Match match in cardRegex.Matches(cardsText))
            {
                yield return new Card(sideOneLanguage, sideTwoLanguage, match.Groups[1].Value, match.Groups[3].Value);
            }
        }


        static class CardPatternBuilder
        {
            public static List<(LanguageType, string)> Parts
            {
                get
                {
                    return new List<(LanguageType, string)>
                    {
                        (LanguageType.En, $"[A-Za-z{OtherSymbolsPart}]+"),
                        (LanguageType.Ru, $"[А-Яа-я{OtherSymbolsPart}]+"),
                        (LanguageType.Undefined, $"[A-Za-zА-Яа-я{OtherSymbolsPart}]+")
                    };
                }
            }
            public static string OtherSymbolsPart => @"0-9\s!\/@#$%&*',`""()";

            public static string Build(LanguageType languageOne, LanguageType languageTwo, string separator = "-")
            {
                return $"^({Parts.First(x => x.Item1 == languageOne).Item2})({separator})({Parts.First(x => x.Item1 == languageTwo).Item2})$";
            }
        }
    }
}
