using System;
using UnityEngine;

namespace Client
{
    public class MonoUpdater : MonoBehaviour
    {
        public event Action OnUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void OnDestroy()
        {
            OnUpdate = null;
        }
    }
}