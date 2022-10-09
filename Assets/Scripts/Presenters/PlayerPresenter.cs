using System;
using System.Collections.Generic;
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
    
        public PlayerPresenter(ICharacter character, PlayerView view, Context context)
        {
            _character = character;
            _view = view;

            _inHandCardsPresenter = new InHandCardsPresenter(character, view.InHandCardsView, context.CardViewFactory);
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