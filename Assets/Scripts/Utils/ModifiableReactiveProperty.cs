using System.Collections.Generic;

namespace Utils
{
    public class ModifiableReactiveProperty<T> : ReactiveProperty<T>
    {
        private T baseValue;

        public List<IPropertyDecorator<T>> modifiers = new();

        public ModifiableReactiveProperty(T initialValue)
        {
            Value = baseValue = initialValue;
        }

        public void ApplyModifier(IPropertyDecorator<T> propertyDecorator)
        {
            modifiers.Add(propertyDecorator);
            UpdateDecoratedValue();
        }

        private void UpdateDecoratedValue()
        {
            var value = baseValue;
            foreach (var modifier in modifiers)
            {
                value = modifier.Decorate(value);
            }

            Value = value;
        }
    }
}