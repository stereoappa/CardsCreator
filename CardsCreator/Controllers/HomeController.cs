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

namespace CardsCreator.WebUI.Controllers
{
    public class HomeController : Controller
    {
        ICardRecoveryService _cardRecoveryService;
        public HomeController(ICardRecoveryService cardRecoveryService)
        {
            _cardRecoveryService = cardRecoveryService;
        }

        public async Task<IActionResult> Index()
        {
            var fullCards = await _cardRecoveryService.TryComplete(
                new List<Card>
                {
                    new Card(LanguageType.En, LanguageType.Ru, "", ""),
                    new Card(LanguageType.En, LanguageType.Ru, "", "дом"),
                    new Card(LanguageType.En, LanguageType.Ru, "работа", "work"),
                    new Card(LanguageType.En, LanguageType.Ru, "telephone", "телефон"),
                }) ;
            //Дай WORD документ, НА СЛОВА Cards[]
            //Проверка карточка данных
            //Восстановление незаполненных карточек МЕНЕДЖЕР ПО ВОССТАНОВЛЕНИЮ КАРТОЧЕК (внутри будет использоваться API Yandex)
            //СОЗДАЕМ ДОКУМЕНТ-МЕНЕДЖЕР Который умеет заполнять word document
            //получаем от него word документ 
            //возвращаем пользователю документ

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
