using System;
using System.Collections.Generic;
using UnityEngine;

namespace Presenters
{
    public class ModelAnimationProxy
    {
        private readonly Queue<Action> updatesQueue = new();

        private readonly List<CardView> _animationItems = new();

        private bool _isAnimating;

        public event Action OnAnimationStepStart; 
        public event Action OnAnimationStepComplete; 

        public bool IsAnimating => _isAnimating;

        public void AddAnimatableItem(CardView cardView)
        {
            _animationItems.Add(cardView);
            cardView.OnAnimationComplete += () =>
            {
                _isAnimating = false;
                TryPlayNext();
                OnAnimationStepComplete?.Invoke();
            };
        }

        public void RemoveAnimatableItem(CardView cardView)
        {
            _animationItems.Remove(cardView);
        }

        public void EnqueueAction(Action action)
        {
            updatesQueue.Enqueue(action);
            TryPlayNext();
        }

        private void TryPlayNext()
        {
            if (_isAnimating)
                return;

            OnAnimationStepStart?.Invoke();

            if (updatesQueue.TryDequeue(out var action))
            {
                action.Invoke();
                _isAnimating = true;
            }
        }
    }
}