using CardsCreator.DomainModel.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardsCreator.Infrastructure.Yandex
{
    internal class LanguageResponse
    {
        public string code { get; set; }
        public string lang { get; set; }

    }

    internal class TranslateResponse
    {
        public string code { get; set; }

        [JsonProperty("lang")]
        public string DirectionsPair { get; set; }
        public List<string> text { get; set; }
    }
}
