using CardsCreator.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardsCreator.DomainModel
{
    public class TranslateDirection
    {
        ILanguageTypeConverter _languageTypeConverter;
        public TranslateDirection(ILanguageTypeConverter languageTypeConverter)
        {
            _languageTypeConverter = languageTypeConverter;
        }
        public LanguageType SourceLanguage { get; }
        public LanguageType TargetLanguage { get; }

        public override string ToString()
        {
            return $"{_languageTypeConverter.Convert(SourceLanguage)}-{_languageTypeConverter.Convert(TargetLanguage)}";
        }
    }
}
