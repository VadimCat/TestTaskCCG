using Presenters;

namespace Client
{
    public class Context
    {
        private readonly ICardViewFactory _cardViewFactory;
        private readonly ScreenNavigator _screenNavigator;
        private readonly InGameCardsService cardsService;
        private readonly CardDragger cardCardDragger;

        public Context(ICardViewFactory cardViewFactory,
            ScreenNavigator screenNavigator,
            InGameCardsService cardsService,
            CardDragger cardCardDragger)
        {
            _cardViewFactory = cardViewFactory;
            _screenNavigator = screenNavigator;
            this.cardsService = cardsService;
            this.cardCardDragger = cardCardDragger;
        }

        public ScreenNavigator Navigator => _screenNavigator;

        public ICardViewFactory CardViewFactory => _cardViewFactory;

        public InGameCardsService CardsService => cardsService;

        public CardDragger CardDragger => cardCardDragger;
    }
}