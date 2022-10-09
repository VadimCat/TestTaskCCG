using UnityEngine;

namespace Client
{
    public class CardsViewFactory : ICardViewFactory
    {
        private CardsViewDataProvider _cardsViewProvider;
        private readonly CardsViewConfig _config;

        public CardView cardPrefab { get; private set; }

        public CardsViewFactory(CardsViewDataProvider cardsViewProvider, CardsViewConfig config)
        {
            _cardsViewProvider = cardsViewProvider;
            _config = config;
        }

        public CardView Create(int cardId, Transform transform)
        {
            var card = Object.Instantiate(_config.CardViewPrefab, transform);
            card.SetViewData(_cardsViewProvider.GetViewData(cardId));

            return card;
        }
    }
}