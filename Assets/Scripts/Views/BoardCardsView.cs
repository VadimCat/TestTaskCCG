using System.Collections.Generic;
using Models;
using UnityEngine;
using Utils;

namespace Views
{
    public class BoardCardsView : MonoBehaviour
    {
        [SerializeField] private RectTransform area;
        
        private const float Width = 800;

        private Rect Rect => area.rect;

        private List<CardView> _cards = new(Constants.MaxCardsOnBoard);

        public void ClearCardIndexes()
        {
            _cards.Clear();
        }
        
        public int GetCardIndexByPos(Transform obj)
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                if (obj.position.x < _cards[i].transform.position.x)
                    return i;
            }
            return _cards.Count;
        }

        public bool InArea(Transform obj)
        {
            return obj.position.IsInRange(area);
        }
        
        public void SetCardAtInd(CardView cardView, int ind)
        {
            if (!_cards.Contains(cardView))
            {
                _cards.Insert(ind, cardView);
                cardView.transform.SetParent(transform);
            }
            UpdatePoses();
        }
        
        private void UpdatePoses()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                var pos = GetCardPosByNumber(i);

                _cards[i].LocalMoveTo(pos);
            }

            for (int i = 0; i < _cards.Count; i++)
            {
                if (_cards[i] == null)
                {
                    _cards.RemoveAt(i);
                    i--;
                }
            }
        }
        
        private Vector3 GetCardPosByNumber(int pos)
        {
            float centerElement = ((float)_cards.Count - 1) / 2; 
            
            float xNormalPos = (pos - centerElement) / Constants.MaxCardsOnBoard;
            
            return new Vector2(Width * xNormalPos, 0);
        }
    }
}