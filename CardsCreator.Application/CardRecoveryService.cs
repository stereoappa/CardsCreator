using CardsCreator.DomainModel;
using CardsCreator.DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardsCreator.Application
{
    public interface ICardRestoreService
    {
        Task<List<RestoreResult>> TryRestore(List<Card> cards);
        Task<RestoreResult> TryRestoreTranslate(Card card);
        Task<RestoreResult> TryRestoreLanguageType(Card card);
    }

    public class CardRestoreService : ICardRestoreService
    {
        private readonly ITranslator _translator;

        public CardRestoreService(ITranslator translator)
        {
            _translator = translator;
        }

        public async Task<List<RestoreResult>> TryRestore(List<Card> inputCards)
        {
            List<RestoreResult> results = new List<RestoreResult>();

            foreach (var card in inputCards)
            {
                if (card.IsCompleted)
                    continue;

                if (string.IsNullOrWhiteSpace(card.SideOne.Text) && string.IsNullOrWhiteSpace(card.SideTwo.Text))
                    continue;

                results.Add(await TryRestoreLanguageType(card));
                results.Add(await TryRestoreTranslate(card));
            }
            return results;
        }

        public async Task<RestoreResult> TryRestoreLanguageType(Card card)
        {
            var restoredParams = RestoredParams.Nothing;
            if (!card.SideOne.LanguageType.HasValue && card.SideOne.Text.Length > 0)
            {
                card.SideOne.LanguageType = await _translator.GetLanguage(card.SideOne.Text);
                restoredParams = RestoredParams.SideOneLanguageType;
            }
            if (!card.SideTwo.LanguageType.HasValue && card.SideTwo.Text.Length > 0)
            {
                card.SideTwo.LanguageType = await _translator.GetLanguage(card.SideTwo.Text);
                restoredParams |= RestoredParams.SideOneLanguageType;
            }

            return new RestoreResult(card, restoredParams);
        }

        public async Task<RestoreResult> TryRestoreTranslate(Card card)
        {
            var restoredParams = RestoredParams.Nothing;
            if (string.IsNullOrWhiteSpace(card.SideOne.Text))
            {
                card.SideOne.Text = await _translator.GetTranslate(card.SideTwo.Text, card.SideOne.LanguageType.Value);
                restoredParams = RestoredParams.SideOneTranslate;
            }
            if (string.IsNullOrWhiteSpace(card.SideTwo.Text))
            {
                card.SideTwo.Text = await _translator.GetTranslate(card.SideOne.Text, card.SideTwo.LanguageType.Value);
                restoredParams |= RestoredParams.SideTwoTranslate;
            }

            return new RestoreResult(card, restoredParams);
        }
    }

    public class RestoreResult
    {
        public RestoreResult(Card targetCard, RestoredParams restoredParams)
        {
            TargetCard = targetCard;
            RestoredParams = restoredParams;
        }

        public Card TargetCard { get; }
        public bool IsRestored => RestoredParams != RestoredParams.Nothing;
        public RestoredParams RestoredParams { get; } = RestoredParams.Nothing;
    }

    [Flags]
    public enum RestoredParams
    {
        Nothing = 1,
        SideOneTranslate = 2,
        SideOneLanguageType = 4,
        SideTwoTranslate = 8,
        SideTwoLanguageType = 16
    }
}
