using CardsCreator.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsCreator.WebUI.Model
{
    public class ParseContext
    {
        public LanguageType OneSideLanguage { get; set; } = LanguageType.Undefined;
        public LanguageType TwoSideLanguage { get; set; } = LanguageType.Undefined;
        public string Text { get; set; }
        public string Separator { get; set; }
    }
}
