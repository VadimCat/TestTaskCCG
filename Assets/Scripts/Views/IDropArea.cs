using UnityEngine;

namespace Views
{
    public interface IDropArea
    {
        bool TryDrop(Transform drop);
    }
}