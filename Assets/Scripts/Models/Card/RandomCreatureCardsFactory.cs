using System;
using Models;

namespace Client
{
    public class RandomCreatureCardsFactory : ICardFactory
    {
        private readonly Random _random = new();

        public ICard Create(int id)
        {
            return new CreatureCard(id, 
                health: _random.Next(Constants.MinHealthValue, Constants.MaxHealthValueExclusive),
                attack: _random.Next(Constants.MinAttackValue, Constants.MaxAttackValueExclusive),
                manaCost: _random.Next(Constants.MinManaValue, Constants.MaxManaValueExclusive));
        }
    }
}