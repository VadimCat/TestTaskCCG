using UnityEngine;

namespace Utils
{
    public static class MathExtensions
    {
        public static bool IsInRange(this Vector3 pos, RectTransform area)
        {
            var position = area.position;
            var rect = area.rect;
            return pos.x >= position.x - rect.width / 2 &&
                   pos.x <= position.x + rect.width / 2 &&
                   pos.y >= position.y - rect.height / 2 &&
                   pos.y <= position.y + rect.height / 2;
        }
    }
}