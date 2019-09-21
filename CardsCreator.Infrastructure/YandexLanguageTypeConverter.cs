
using CardsCreator.DomainModel;
using CardsCreator.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardsCreator.Infrastructure.Yandex
{
    public class YandexLanguageTypeConverter : ILanguageTypeConverter
    {
        public LanguageType Convert(string langTypeText)
        {
            switch (langTypeText)
            {
                case "en": return LanguageType.En;
                case "ru": return LanguageType.Ru;
                default: return LanguageType.Undefined;
            }
        }

        public string Convert(LanguageType type)
        {
            switch (type)
            {
                case LanguageType.En: return "en";
                case LanguageType.Ru: return "ru";

                case LanguageType.Undefined:
                default: return "";
            }
        }
    }
}
