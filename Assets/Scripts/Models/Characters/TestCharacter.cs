using System;
using System.Collections.Generic;
using Utils;
using Random = System.Random;

namespace Models
{
    public class TestCharacter : ICharacter
    {
        public List<ICard> InHandCards { get; set; } = new(Constants.MaxCardsInHandExclusive - 1);
        public List<ICard> BoardCards { get; set; } = new(Constants.MaxCardsOnBoard);

        public event Action<List<ICard>> OnCardsDealt;
        public event Action<ICard> OnCardDestroy;

        public void DealCards(List<ICard> dealtCards)
        {
            var cardsToAdd = new List<ICard>(dealtCards);
            if (InHandCards.Count + dealtCards.Count > Constants.MaxCardsInHandExclusive - 1)
            {
                int extraCards = InHandCards.Count + cardsToAdd.Count - (Constants.MaxCardsInHandExclusive - 1);

                cardsToAdd.RemoveRange(cardsToAdd.Count - extraCards, extraCards);
            }

            InHandCards.AddRange(cardsToAdd);

            OnCardsDealt?.Invoke(cardsToAdd);
        }

        public void UseAbility()
        {
            ModifyHandRandomly();
            ModifyHandRandomly();
        }

        private void ModifyHandRandomly()
        {
            foreach (var card in InHandCards)
            {
                ModifyRandomly(card);
            }

            ClearDeadCards();
        }

        private void ModifyRandomly(ICard card)
        {
            var rnd = new Random();
            var statToModify = (Stats)rnd.Next(Enum.GetValues(typeof(Stats)).Length);
            var newRndDecorator =
                new RandomIntDecorator(Constants.RandomModifierMin, Constants.RandomModifierMaxExclusive);

            switch (statToModify)
            {
                case Stats.Health:
                    card.ApplyModifier(Stats.Health, newRndDecorator);
                    break;
                case Stats.Attack:
                    card.ApplyModifier(Stats.Attack, newRndDecorator);
                    break;
                case Stats.Mana:
                    card.ApplyModifier(Stats.Mana, newRndDecorator);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ClearDeadCards()
        {
            for (int i = 0; i < InHandCards.Count; i++)
            {
                if (InHandCards[i].Health.Value == 0)
                {
                    InHandCards[i].KillCard();
                    InHandCards.RemoveAt(i);
                    i--;
                }   
            }
        }
    }

    public enum Stats
    {
        Health,
        Attack,
        Mana
    }
}