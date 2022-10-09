using UnityEngine;

namespace Client
{
    public class GameScreen : BaseScreen
    {
        [SerializeField] private PlayerView playerView;

        public PlayerView PlayerView
        {
            get { return this.playerView; }
        }
    }
}