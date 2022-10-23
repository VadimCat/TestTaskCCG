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
        private readonly InGameCardsService inGameCardsService;
        private readonly CardDragger cardDragger;

        private ModelAnimationProxy _animationProxy;

        public InHandCardsPresenter(ICharacter character, InHandCardsView view, ICardViewFactory cardViewFactory,
            InGameCardsService inGameCardsService, CardDragger cardDragger)
        {
            _character = character;
            _view = view;
            _cardViewFactory = cardViewFactory;
            this.inGameCardsService = inGameCardsService;
            this.cardDragger = cardDragger;

            cardDragger.OnCardDropFail += view.UpdatePoses;
            cardDragger.OnCardDrop += view.RemoveCard;
            
            _animationProxy = new ModelAnimationProxy();
            _animationProxy.OnAnimationStepStart += cardDragger.Disable;
            _animationProxy.OnAnimationStepComplete += cardDragger.Enable;
            character.OnBoardUpdate += FillCards;

            FillCards(character.InHandCards);
        }

        private void FillCards(List<ICard> cards)
        {
            foreach (var card in cards)
            {
                var cardView = _cardViewFactory.Create(card.Id, _view.CardsRoot);
                cardDragger.RegisterCard(cardView);
                inGameCardsService.RegisterCard(card, cardView);
                cardView.SetMana(card.ManaCost.Value, 0);
                cardView.SetAttack(card.Attack.Value, 0);
                cardView.SetHealth(card.Health.Value, 0);

                //TODO: Logic for CardPresenter created from CharacterPresenter
                card.ManaCost.OnValueChanged += (value, prevValue) =>
                    _animationProxy.EnqueueAction(() =>
                        cardView.SetMana(value, prevValue));

                card.Health.OnValueChanged += (value, prevValue) =>
                {
                    _animationProxy.EnqueueAction(() => cardView.SetHealth(value, prevValue));

                    if (value <= 0)
                    {
                        inGameCardsService.UnregisterCard(card);
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
            _character.OnBoardUpdate -= FillCards;
        }
    }
}