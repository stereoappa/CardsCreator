using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardsCreator.DomainModel.Interfaces;
using CardsCreator.Application;
using CardsCreator.DomainModel;
using System.Net.Http.Headers;
using System.IO;
using System.Text.RegularExpressions;

namespace CardsCreator.WebUI.Controllers
{
    [ApiController]
    public class CardsController : ControllerBase
    {
        ICardRestoreService _cardRestoreService;
        ICardsTableService _cardsTableService;
        ICardParserService _cardParserService;
        public CardsController(ICardRestoreService cardRestoreService, ICardsTableService cardsTableService, ICardParserService cardParserService)
        {
            _cardsTableService = cardsTableService;
            _cardRestoreService = cardRestoreService;
            _cardParserService = cardParserService;
        }

        [HttpPost("parse")]
        public List<Card> Parse(string text, string separator = "-")
        {
            //_cardParserService.Parse(text)
            return new List<Card>
                {
                    new Card(LanguageType.En, LanguageType.Ru, "spent time", "проводить время"),
                    new Card(LanguageType.En, LanguageType.Ru, "pleasant", "приятный"),
                    new Card(LanguageType.En, LanguageType.Ru, "tired", "устал"),
                    new Card(LanguageType.En, LanguageType.Ru, "thick", "толстый, густой"),
                    new Card(LanguageType.En, LanguageType.Ru, "tall", "высокий"),
                    new Card(LanguageType.En, LanguageType.Ru, "servant", "слуга"),
                    new Card(LanguageType.En, LanguageType.Ru, "appear", "появиться"),
                    new Card(LanguageType.En, LanguageType.Ru, "Excited", "в восторге"),
                    new Card(LanguageType.En, LanguageType.Ru, "steam train", "паровоз"),
                    new Card(LanguageType.En, LanguageType.Ru, "advise", "совет"),
                    new Card(LanguageType.En, LanguageType.Ru, "Luckily", "к счастью"),
                    new Card(LanguageType.En, LanguageType.Ru, "means of travelling", "средства передвижения"),
                    new Card(LanguageType.En, LanguageType.Ru, "earned money", "заработанные деньги"),
                    new Card(LanguageType.En, LanguageType.Ru, "judge", "судья"),
                    new Card(LanguageType.En, LanguageType.Ru, "grown-up", "взрослый"),
                    new Card(LanguageType.En, LanguageType.Ru, "think/thought/thought", "думать/думал"),
                    new Card(LanguageType.En, LanguageType.Ru, "through", "через"),
                    new Card(LanguageType.En, LanguageType.Ru, "them", "их"),
                    new Card(LanguageType.En, LanguageType.Ru, "they", "они"),
                    new Card(LanguageType.En, LanguageType.Ru, "then", "затем"),
                    new Card(LanguageType.En, LanguageType.Ru, "is known as", "известен как"),
                };
        }


        [HttpPost("table")]
        public async Task<FileStreamResult> GenerateTable([FromBody]List<Card> cards)
        {
            var restoreResults = await _cardRestoreService.TryRestore(cards);
            var file = await _cardsTableService.GenerateTable(cards);

            return File(new MemoryStream(file), "application/octet-stream", "CardsResult.docx");
        }

    }
}
