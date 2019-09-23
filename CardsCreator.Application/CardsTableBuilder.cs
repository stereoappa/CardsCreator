using CardsCreator.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardsCreator.Application
{
    public interface ICardTableService
    {
        Task<byte[]> BuildTable(List<Card> cards);
    }

    public class CardTableService : ICardTableService
    {
        ICardRecoveryService _cardRecoveryService;
        ICardsTemplatesService _cardsTemplatesService;

        public CardTableService(ICardRecoveryService cardRecoveryService, ICardsTemplatesService cardsTemplatesService)
        {
            _cardRecoveryService = cardRecoveryService;
            _cardsTemplatesService = cardsTemplatesService;
        }

        public async Task<byte[]> BuildTable(List<Card> cards)
        {
            var completedCards = await _cardRecoveryService.TryComplete(cards);

            return await Task.Factory.StartNew(() => _cardsTemplatesService.GetCardsTable(completedCards));
        }
    }
}
