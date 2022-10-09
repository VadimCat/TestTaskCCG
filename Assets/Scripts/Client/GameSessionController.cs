using Models;
using Presenters;

namespace Client
{
    public class GameSessionController
    {
        private readonly Context _context;

        private IGameSession _currentGameSession;

        public GameSessionController(Context context)
        {
            _context = context;
        }

        public async void StartSession()
        {
            var gameScreen = await _context.Navigator.PushGameScreen();

            _currentGameSession = new GameSession(new RandomCardsProvider());
            _currentGameSession.StartGame();

            PlayerPresenter presenter = new PlayerPresenter(_currentGameSession.Player1, gameScreen.PlayerView, _context);
        }
    }
}