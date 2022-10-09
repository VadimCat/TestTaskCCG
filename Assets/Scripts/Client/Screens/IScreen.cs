using System.Threading.Tasks;

namespace Client
{
    public interface IScreen
    {
        public Task Show();
        public Task Hide();
    }
}