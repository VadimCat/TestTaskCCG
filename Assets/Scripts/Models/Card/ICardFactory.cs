using Models;

namespace Client
{
    public interface ICardFactory
    {
        public ICard Create(int id);
    }
}