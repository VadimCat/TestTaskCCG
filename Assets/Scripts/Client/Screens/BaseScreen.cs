using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Client
{
    public abstract class BaseScreen : MonoBehaviour, IScreen
    {
        [SerializeField] private CanvasGroup parentCanvasGroup;

        private PlayerView _playerView;

        private Tween currentTween;

        public async Task Show()
        {
            Cancel();

            MakeInteractable(false);
            parentCanvasGroup.alpha = 0;
            currentTween = parentCanvasGroup.DOFade(1, 1);
            await currentTween.AsyncWaitForCompletion();
            MakeInteractable(true);
        }

        public async Task Hide()
        {
            Cancel();

            parentCanvasGroup.alpha = 1;
            currentTween = parentCanvasGroup.DOFade(0, 1);
            await currentTween.AsyncWaitForCompletion();
            MakeInteractable(false);
        }

        private void MakeInteractable(bool isInteractable)
        {
            parentCanvasGroup.interactable = isInteractable;
            parentCanvasGroup.blocksRaycasts = isInteractable;
        }
        
        private void Cancel()
        {
            if (currentTween != null)
                currentTween.Complete();
        }
    }
}