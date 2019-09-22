using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CardsCreator.DomainModel;
using CardsCreator.DomainModel.Interfaces;

namespace CardsCreator.Application
{
    public interface ICardDocumentService
    {
        byte[] GetCardsSheetDocument(List<Card> cards);
    }
    public class CardDocumentService : ICardDocumentService
    {
        public CardDocumentService()
        {

        }
        public byte[] GetCardsSheetDocument(List<Card> cards)
        {
            return new CardsSheetDocx().GetBytes();
        }
    }
}
