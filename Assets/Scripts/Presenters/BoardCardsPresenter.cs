using System;
using System.Collections.Generic;
using Models;
using UnityEngine;
using Views;

namespace Presenters
{
    public class BoardCardsPresenter : IDropArea
    {
        private readonly ICharacter character;
        private readonly BoardCardsView view;
        private readonly InGameCardsService inGameCardsService;
        private readonly CardDragger cardDragger;

        private Dictionary<ICard, CardView> cardViews = new(Constants.MaxCardsOnBoard);

        public BoardCardsPresenter(ICharacter character, BoardCardsView view, InGameCardsService inGameCardsService, CardDragger cardDragger)
        {
            this.character = character;
            this.view = view;
            this.inGameCardsService = inGameCardsService;
            this.cardDragger = cardDragger;

            cardDragger.AddDropArea(this);
            character.OnBoardUpdate += UpdateCards;
        }
        
        private void UpdateCards(List<ICard> cards)
        {
            view.ClearCardIndexes();
            
            foreach (var key in cardViews.Keys)
            {
                if (!cards.Contains(key))
                {
                    cardViews[key].AnimateDeath();
                }
            }

            for (int i = 0; i < cards.Count; i++)
            {
                if (inGameCardsService.TryGetCardView(cards[i], out var cardView))
                {
                    view.SetCardAtInd(cardView, i);
                }
                else
                {
                    throw new Exception("CardView not found by model");
                }
            }
        }

        public bool TryDrop(Transform drop)
        {
            CardView cardView = drop.gameObject.GetComponent<CardView>();
            if (cardView != null && inGameCardsService.TryGetModel(cardView, out var card))
            {
                bool isInArea = view.InArea(drop);
                if (!isInArea)
                    return false;
                
                var ind = view.GetCardIndexByPos(cardView.transform);
                bool isSetted = character.TrySetCardOnTable(card, ind); 

                if(isSetted)
                    view.SetCardAtInd(cardView, ind);
                
                return isSetted;
            }

            
            return false;
        }
    }

    public class InGameCardsService
    {
        private Dictionary<ICard, CardView> CardToViews = new();
        private Dictionary<CardView, ICard> ViewsToCards = new();

        public void UnregisterCard(ICard card)
        {
            if (CardToViews.ContainsKey(card))
            {
                var view = CardToViews[card];

                CardToViews.Remove(card);
                ViewsToCards.Remove(view);
            }
        }
        
        public void RegisterCard(ICard card, CardView cardView)
        {
            CardToViews[card] = cardView;
            ViewsToCards[cardView] = card;
        }

        public bool TryGetModel(CardView cardView, out ICard card)
        {
            if (ViewsToCards.ContainsKey(cardView))
            {
                card = ViewsToCards[cardView];
                return true;
            }
            else
            {
                card = null;
                return false;
            }
        }
        
        public bool TryGetCardView(ICard card, out CardView cardView)
        {
            if (CardToViews.ContainsKey(card))
            {
                cardView = CardToViews[card];
                return true;
            }
            else
            {
                cardView = null;
                return false;
            }
        }
    }
}