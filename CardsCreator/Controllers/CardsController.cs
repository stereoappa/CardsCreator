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
using CardsCreator.WebUI.Model;

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
        public async Task<List<Card>> Parse(ParseContext parseContext)
        {

            var cards = _cardParserService.Parse(parseContext.OneSideLanguage,
                                            parseContext.TwoSideLanguage,
                                            parseContext.Text, 
                                            parseContext.Separator).ToList();

           await _cardRestoreService.TryRestore(cards);
           return cards;
        }


        [HttpPost("table")]
        public async Task<FileStreamResult> GenerateTable([FromBody]List<Card> cards)
        {
            if (cards.Count == 0)
                return null;

            var restoreResults = await _cardRestoreService.TryRestore(cards);
            var file = await _cardsTableService.GenerateTable(cards);

            return File(new MemoryStream(file), "application/octet-stream", "CardsResult.docx");
        }

        [HttpGet("test")]
        public string TestMethod()
        {
            return "Test!";
        }
    }
}
