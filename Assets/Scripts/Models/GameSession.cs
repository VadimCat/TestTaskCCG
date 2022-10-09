using System;
using Client;

namespace Models
{
    public class GameSession : IGameSession
    {
        private readonly RandomCardsProvider _randomCardsProvider;

        public GameSession(RandomCardsProvider randomCardsProvider)
        {
            _randomCardsProvider = randomCardsProvider;
        }

        private ICardFactory _cardFactory;
        public ICharacter Player1 { get; set; }

        public void StartGame()
        {
            Player1 = new TestCharacter();

            int cardsToDealCount = new Random().Next(Constants.MinCardsToDeal, Constants.MaxCardsInHandExclusive);
            Player1.DealCards(_randomCardsProvider.GetRandomCards(cardsToDealCount));
        }
    }
}