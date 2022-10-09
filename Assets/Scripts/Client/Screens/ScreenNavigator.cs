using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    //Hack class
    public class ScreenNavigator : MonoBehaviour
    {
        [SerializeField] private GameScreen gameScreenPref;
        [SerializeField] private LoadingScreen loadingScreenPref;

        private GameScreen _gameScreen;
        private LoadingScreen _loadingScreen;

        public async Task<GameScreen> PushGameScreen()
        {
            if (_gameScreen == null)
            {
                _gameScreen = Instantiate(gameScreenPref, transform);
            }

            await _gameScreen.Show();

            return _gameScreen;
        }

        public async void HideGameScreen()
        {
            if (_gameScreen == null)
                return;

            await _gameScreen.Hide();
        }
        
        public async Task<LoadingScreen> PushLoadingScreen()
        {
            if (_loadingScreen == null)
            {
                _loadingScreen = Instantiate(loadingScreenPref, transform);
            }

            await _loadingScreen.Show();

            return _loadingScreen;
        }

        public async Task HideLoadingScreen()
        {
            if (_loadingScreen == null)
                return;

            await _loadingScreen.Hide();
        }
    }
}