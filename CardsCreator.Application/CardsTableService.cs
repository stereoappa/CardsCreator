using CardsCreator.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardsCreator.Application
{
    public interface ICardsTableService
    {
        Task<byte[]> GenerateTable(List<Card> cards);
    }

    public class CardsTableService : ICardsTableService
    {
        ICardsTemplatesService _cardsTemplatesService;

        public CardsTableService(ICardsTemplatesService cardsTemplatesService)
        {
            _cardsTemplatesService = cardsTemplatesService;
        }

        public async Task<byte[]> GenerateTable(List<Card> cards)
        {
            return await Task.Factory.StartNew(() => _cardsTemplatesService.GetCardsTable(cards));
        }
    }
}
