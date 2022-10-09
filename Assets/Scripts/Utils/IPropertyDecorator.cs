namespace Utils
{
    public interface IPropertyDecorator<T>
    {
        public T Decorate(T value);
    }
}