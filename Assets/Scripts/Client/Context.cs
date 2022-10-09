namespace Client
{
    public class Context
    {
        private readonly ICardViewFactory _cardViewFactory;
        private readonly ScreenNavigator _screenNavigator;

        public Context(ICardViewFactory cardViewFactory,
            ScreenNavigator screenNavigator)
        {
            _cardViewFactory = cardViewFactory;
            _screenNavigator = screenNavigator;
        }

        public ScreenNavigator Navigator => _screenNavigator;

        public ICardViewFactory CardViewFactory => _cardViewFactory;
    }
}