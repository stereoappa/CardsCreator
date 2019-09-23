using CardsCreator.DomainModel;
using CardsCreator.DomainModel.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CardsCreator.Infrastructure.Yandex
{
    public class YandexTranslate : ITranslator
    {
        IHttpClientFactory _clientFactory;
        ILanguageTypeConverter _languageTypeConverter;
        readonly string _apiKey;

        public YandexTranslate(IConfiguration configuration, IHttpClientFactory clientFactory, ILanguageTypeConverter languageTypeConverter)
        {
            _apiKey = configuration["AppSettings:YandexApiKey"].ToString();
            _clientFactory = clientFactory;
            _languageTypeConverter = languageTypeConverter;

        }

        public async Task<LanguageType> GetLanguage(string text)
        {
            var client = _clientFactory.CreateClient("yandexApiClient");

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("text", text)
            });

            var response = await client.PostAsync($"detect?hint=en,ru&key={_apiKey}", formContent);
            var lang = await (response.Content.ReadAsStringAsync());

            var langResponse = JsonConvert.DeserializeObject<LanguageResponse>(lang);
            return _languageTypeConverter.Convert(langResponse.lang);
        }

        public async Task<string> GetTranslate(string text, LanguageType targetLang)
        {
            if (targetLang == LanguageType.Undefined)
                throw new ArgumentException("Target Language isn't be Undefined");

            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var client = _clientFactory.CreateClient("yandexApiClient");
        
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("text", text)
            });
        
            var response = await client.PostAsync(
                $"translate?lang={_languageTypeConverter.Convert(targetLang)}" +
                $"&key={_apiKey}", formContent);

            var lang = await (response.Content.ReadAsStringAsync());
        
            var transResponse = JsonConvert.DeserializeObject<TranslateResponse>(lang);

            return transResponse.text.FirstOrDefault();
        }
    }
}
