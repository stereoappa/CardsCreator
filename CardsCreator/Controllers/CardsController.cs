using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CardsCreator.WebUI.Models;
using CardsCreator.DomainModel.Interfaces;
using CardsCreator.Application;
using CardsCreator.DomainModel;
using System.Net.Http.Headers;
using System.IO;

namespace CardsCreator.WebUI.Controllers
{
    public class CardsController : Controller
    {
        ICardTableService _cardsTableBuilder;
        public CardsController(ICardTableService cardsTableBuilder)
        {
            _cardsTableBuilder = cardsTableBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<FileStreamResult> CreateTable(List<Card> cards)
        {
            cards = new List<Card>
                {
                    new Card(LanguageType.En, LanguageType.Ru, "", "дом"),
                    new Card(LanguageType.En, LanguageType.Ru, "работа", "work"),
                    new Card(LanguageType.En, LanguageType.Ru, "telephone", "телефон"),
                    new Card(LanguageType.En, LanguageType.Ru, "investigate", ""),
                };

            var file = await _cardsTableBuilder.BuildTable(cards);

            return File(new MemoryStream(file), "application/octet-stream", "CardsResult.docx");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
