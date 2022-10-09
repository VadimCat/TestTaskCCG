namespace Utils
{
    public abstract class RandomDecorator<T> : IPropertyDecorator<T>
    {
        protected T randomValue;

        protected RandomDecorator(T min, T max)
        {
            SaveRandomValue(min, max);
        }

        protected abstract void SaveRandomValue(T min, T max);

        public T Decorate(T value)
        {
            return randomValue;
        }
    }
}