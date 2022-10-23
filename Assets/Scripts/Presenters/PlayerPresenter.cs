using System;
using Client;
using Models;

namespace Presenters
{
    public class PlayerPresenter : IDisposable
    {
        private readonly GameScreen _gameScreen;
        private readonly ICharacter _character;
        private readonly PlayerView _view;

        private readonly InHandCardsPresenter _inHandCardsPresenter;
        private readonly BoardCardsPresenter _boardCardsPresenter;

        public PlayerPresenter(ICharacter character, PlayerView view, Context context)
        {
            _character = character;
            _view = view;

            _inHandCardsPresenter = new InHandCardsPresenter(character, view.InHandCardsView, context.CardViewFactory,
                context.CardsService, context.CardDragger);
            _boardCardsPresenter = new BoardCardsPresenter(character, view.BoardCardsView, context.CardsService, context.CardDragger);
            _view.AbilityButton.onClick.AddListener(UseAbility);
        }

        private void UseAbility()
        {
            _character.UseAbility();
        }

        public void Dispose()
        {
            _inHandCardsPresenter?.Dispose();
            _view.AbilityButton.onClick.RemoveListener(UseAbility);
        }
    }
}