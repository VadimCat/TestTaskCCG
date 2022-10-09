using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ScreenNavigator screenNavigator;
        [SerializeField] private CardsViewConfig cardsViewConfig;
        
        
        private RandomCardsProvider _cardsProvider;

        private Context _context;
        
        private async void Awake()
        {
            _cardsProvider = new RandomCardsProvider();
            var cardsViewProvider = new CardsViewDataProvider(new CardsArtDownloader());

            var screenTask = screenNavigator.PushLoadingScreen();
            var loadingTask = cardsViewProvider.GenerateData(_cardsProvider.CardsCount);

            await Task.WhenAll(screenTask, loadingTask);

            await screenNavigator.HideLoadingScreen();

            _context = new Context(new CardsViewFactory(cardsViewProvider, cardsViewConfig), screenNavigator);
            
            await LoadGameScene();

            var gameSessionController = new GameSessionController(_context);
            gameSessionController.StartSession();

        }

        private async Task LoadGameScene()
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            var loadOperation = SceneManager.LoadSceneAsync(Scenes.GameSessionScene, LoadSceneMode.Additive);

            loadOperation.completed += (op) => { taskCompletionSource.SetResult(op.isDone); };

            await taskCompletionSource.Task;
        }
    }
}