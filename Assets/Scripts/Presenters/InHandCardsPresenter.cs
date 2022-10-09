using System;
using System.Collections.Generic;
using Models;
using Views;

namespace Presenters
{
    public class InHandCardsPresenter : IDisposable
    {
        private readonly ICharacter _character;
        private readonly InHandCardsView _view;
        private readonly ICardViewFactory _cardViewFactory;

        private ModelAnimationProxy _animationProxy;

        public InHandCardsPresenter(ICharacter character, InHandCardsView view, ICardViewFactory cardViewFactory)
        {
            _character = character;
            _view = view;
            _cardViewFactory = cardViewFactory;

            _animationProxy = new ModelAnimationProxy();
            character.OnCardsDealt += FillCards;

            FillCards(character.InHandCards);
        }

        private void FillCards(List<ICard> cards)
        {
            foreach (var card in cards)
            {
                var cardView = _cardViewFactory.Create(card.Id, _view.CardsRoot);
                cardView.SetMana(card.ManaCost.Value, 0);
                cardView.SetAttack(card.Attack.Value, 0);
                cardView.SetHealth(card.Health.Value, 0);
            
                card.ManaCost.OnValueChanged += (value, prevValue) =>
                    _animationProxy.EnqueueAction(() =>
                        cardView.SetMana(value, prevValue));

                card.Health.OnValueChanged += (value, prevValue) =>
                {
                    _animationProxy.EnqueueAction(() => cardView.SetHealth(value, prevValue));

                    if (value <= 0)
                    {
                    
                        _animationProxy.EnqueueAction(() =>
                        {
                            _view.RemoveCard(cardView);
                            _animationProxy.RemoveAnimatableItem(cardView);
                            cardView.AnimateDeath();
                        });
                    }
                };

                card.Attack.OnValueChanged += (value, prevValue) =>
                {
                    _animationProxy.EnqueueAction(() =>
                        cardView.SetAttack(value, prevValue));
                };

                _view.AddCard(cardView);
                _animationProxy.AddAnimatableItem(cardView);
            }
        }

        public void Dispose()
        {
            _character.OnCardsDealt -= FillCards;
        }
    }
}