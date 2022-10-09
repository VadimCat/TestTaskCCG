using System;
using Utils;

namespace Models
{
    public class CreatureCard : ICard
    {
        public int Id { get; }
        public ReactiveProperty<int> ManaCost => ManacostProp;
        public ReactiveProperty<int> Health => HealthProp;
        public ReactiveProperty<int> Attack => AttackProp;

        private ModifiableReactiveProperty<int> ManacostProp; 
        private ModifiableReactiveProperty<int> AttackProp; 
        private ModifiableReactiveProperty<int> HealthProp; 
        
        public event Action<ICard> OnCardUse;

        public void KillCard()
        {
            ManacostProp = null;
            AttackProp = null;
            HealthProp = null;
        }

        public void ApplyModifier(Stats stat, IPropertyDecorator<int> modifier)
        {
            switch (stat)
            {
                case Stats.Health:
                    HealthProp.ApplyModifier(modifier);
                    break;
                case Stats.Attack:
                    AttackProp.ApplyModifier(modifier);
                    break;
                case Stats.Mana:
                    ManacostProp.ApplyModifier(modifier);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stat), stat, null);
            }
        }

        public CreatureCard(int id, int health, int attack, int manaCost)
        {
            Id = id;

            ManacostProp = new ModifiableReactiveProperty<int>(manaCost);
            AttackProp = new ModifiableReactiveProperty<int>(attack);
            HealthProp = new ModifiableReactiveProperty<int>(health);
        }

        public object Clone()
        {
            return new CreatureCard(Id, Health.Value, Attack.Value, ManaCost.Value);
        }
    }
}