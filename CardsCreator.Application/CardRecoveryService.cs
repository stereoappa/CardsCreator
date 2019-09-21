using CardsCreator.DomainModel;
using CardsCreator.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardsCreator.Application
{
    public interface ICardRecoveryService
    {
        Task<List<Card>> TryComplete(List<Card> cards);
    }

    public class CardRecoveryService : ICardRecoveryService
    {
        private readonly ITranslator _translator;

        public CardRecoveryService(ITranslator translator)
        {
            _translator = translator;
        }

        public async Task<List<Card>> TryComplete(List<Card> inputCards)
        {
            foreach (var card in inputCards)
            {
                if (card.IsCompleted)
                    continue;

                if (string.IsNullOrWhiteSpace(card.SideOne.Text))
                {
                    card.SideOne.Text = await _translator.GetTranslate(card.SideTwo.Text, card.SideOne.Type);
                }
                if (string.IsNullOrWhiteSpace(card.SideTwo.Text))
                {
                    card.SideTwo.Text = await _translator.GetTranslate(card.SideOne.Text, card.SideTwo.Type);
                }
            }

            return inputCards;
        }
    }
}
