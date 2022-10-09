using System;
using Utils;

namespace Models
{
    public interface ICard : ICloneable
    {
        int Id { get; }
        ReactiveProperty<int> ManaCost { get; }
        ReactiveProperty<int> Health { get; }
        ReactiveProperty<int> Attack { get; }

        event Action<ICard> OnCardUse;

        public void KillCard();
        public void ApplyModifier(Stats stat, IPropertyDecorator<int> modifier);
    }

    public interface IGameSession
    {
        public ICharacter Player1 { get; }

        public void StartGame();
    }
}