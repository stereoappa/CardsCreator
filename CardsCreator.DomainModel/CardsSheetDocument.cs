using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TemplateEngine.Docx;

namespace CardsCreator.DomainModel
{
    public class CardsSheetTemplate : DocxTemplateBase
    {
        private readonly List<Card> _cards;
        public CardsSheetTemplate(string path, List<Card> cards) : base(path)
        {
            _cards = cards;
        }

        protected override Content GetContent()
        {
            var cardsTable = new TableContent("CardsTable");
            foreach (var card in _cards)
            {
                cardsTable.AddRow(new FieldContent("SideOne:Text", card.SideOne.Text), new FieldContent("SideTwo:Text", card.SideTwo.Text));
            }

            return new Content(cardsTable);
        }
    }
}
