using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CardsCreator.DomainModel;
using CardsCreator.DomainModel.Interfaces;

namespace CardsCreator.Application
{
    public interface ICardsTemplatesService
    {
        byte[] GetCardsTable(List<Card> cards);
    }
    public class CardsTemplateService : ICardsTemplatesService
    {
        private readonly string _templatePath;
        public CardsTemplateService(string templatePath)
        {
            _templatePath = templatePath;
        }
        public byte[] GetCardsTable(List<Card> cards)
        {
            CardsSheetTemplate cardsSheetTemplate = new CardsSheetTemplate(_templatePath, cards);
            return cardsSheetTemplate.GetCompleteDocument();
        }
    }
}
