using System.Threading.Tasks;
using Presenters;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ScreenNavigator screenNavigator;
        [SerializeField] private CardsViewConfig cardsViewConfig;
        [SerializeField] private Camera camera;
        [SerializeField] private MonoUpdater updater;

        private RandomCardsProvider _cardsProvider;

        private Context _context;

        private async void Awake()
        {
            _cardsProvider = new RandomCardsProvider();
            var cardsViewProvider = new CardsViewDataProvider(new CardsArtDownloader());
            var cardsService = new InGameCardsService();
            var carDragger = new CardDragger(camera, updater);
            var cardsViewFactory = new CardsViewFactory(cardsViewProvider, cardsViewConfig); 
            
            _context = new Context(cardsViewFactory, screenNavigator, cardsService, carDragger);

            var screenTask = screenNavigator.PushLoadingScreen();
            var loadingTask = cardsViewProvider.GenerateData(_cardsProvider.CardsCount);

            await Task.WhenAll(screenTask, loadingTask);

            await screenNavigator.HideLoadingScreen();

            // await LoadGameScene();

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