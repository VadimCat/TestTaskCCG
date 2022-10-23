using System;
using System.Collections.Generic;
using Client;
using UnityEngine;
using Views;

namespace Presenters
{
    public class CardDragger
    {
        private CardView currentDrag;
        private Transform dragTransform;
        private Camera camera;
        private readonly MonoUpdater monoUpdater;

        private bool isDragEnabled = true;
        private List<IDropArea> dropAreas = new List<IDropArea>();

        public CardDragger(Camera camera, MonoUpdater monoUpdater)
        {
            this.camera = camera;
            this.monoUpdater = monoUpdater;
            monoUpdater.OnUpdate += Drag;
        }

        public void Disable()
        {
            isDragEnabled = false;
        }

        public void Enable()
        {
            isDragEnabled = true;
        }
        
        
        public void AddDropArea(IDropArea dropArea)
        {
            dropAreas.Add(dropArea);
        }
        
        public event Action<CardView> OnCardDrop;
        public event Action OnCardDropFail;

        public void RegisterCard(CardView cardView)
        {
            cardView.OnHoldStart += StartDrag;
            cardView.OnHoldEnd += StopDrag;
        }

        private void Drag()
        {
            if (currentDrag != null && isDragEnabled)
            {
                var z = currentDrag.transform.position.z;
                var screenToWorldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
                screenToWorldPoint.z = z;
                currentDrag.transform.localPosition = Input.mousePosition - new Vector3((float)camera.pixelWidth / 2,
                    (float)camera.pixelHeight / 2 - 300);
            }
        }

        private void StopDrag(CardView obj)
        {
            if (currentDrag == obj && isDragEnabled)
            {
                obj.EnableDragStyle(false);
                foreach (var area in dropAreas)
                {
                    if (area.TryDrop(obj.transform))
                    {
                        UnregisterCard(obj);
                        OnCardDrop?.Invoke(obj);
                        break;
                    }
                }
                OnCardDropFail?.Invoke();
                
                currentDrag = null;
            }
            else if(currentDrag == obj && !isDragEnabled)
            {
                currentDrag = null;
                OnCardDropFail?.Invoke();
            }
        }

        private void StartDrag(CardView obj)
        {
            if (currentDrag == null && isDragEnabled)
            {
                obj.EnableDragStyle(true);
                currentDrag = obj;
            }
        }

        public void UnregisterCard(CardView cardView)
        {
            cardView.OnHoldStart -= StartDrag;
            cardView.OnHoldEnd -= StopDrag;
        }
    }
}