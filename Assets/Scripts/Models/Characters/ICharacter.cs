using System;
using System.Collections.Generic;

namespace Models
{
    public interface ICharacter
    {
        public List<ICard> InHandCards { get; set; }
        public List<ICard> BoardCards { get; set; }

        public event Action<List<ICard>> OnBoardUpdate;
        event Action<ICard> OnCardDestroy; 

        public void RemoveCardFromHand(ICard card)
        {
            InHandCards.Remove(card);
        }

        public bool TrySetCardOnTable(ICard card, int index);
        public void DealCards(List<ICard> dealtCards);
        void UseAbility();
    }
}