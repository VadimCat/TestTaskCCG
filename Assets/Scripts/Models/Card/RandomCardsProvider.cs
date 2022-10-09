using System;
using System.Collections.Generic;
using Models;

namespace Client
{
    public class RandomCardsProvider
    {
        private Random _random = new();
        
        public ICardFactory CardFactory = new RandomCreatureCardsFactory();

        private Dictionary<int, ICard> uniqueCards = new Dictionary<int, ICard>();

        public int CardsCount => uniqueCards.Count;
        
        public RandomCardsProvider()
        {
            for (int i = 0; i < Constants.UniqueCardsToGenerate; i++)
            {
                uniqueCards[i] = CardFactory.Create(i);
            }
        }

        public List<ICard> GetRandomCards(int count)
        {
            var cards = new List<ICard>(count);

            for (int i = 0; i < count; i++)
            {
                cards.Add((ICard)uniqueCards[_random.Next(uniqueCards.Count)].Clone());
            }

            return cards;
        }
    }
}