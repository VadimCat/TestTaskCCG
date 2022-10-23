using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Views
{
    public class InHandCardsView : MonoBehaviour
    {
        private int MaxCardsInHand => Constants.MaxCardsInHandExclusive - 1;

        public Transform CardsRoot => transform;

        private const int ArcWidth = 600;
        private const int ArcHeight = 350;

        private List<CardView> _cards = new(Constants.MaxCardsInHandExclusive - 1);


        public void AddCard(CardView newObj)
        {
            _cards.Add(newObj);
            var objTransform = newObj.transform;
            objTransform.SetParent(CardsRoot);
            objTransform.localScale = Vector3.one;

            UpdatePoses();
        }

        public void RemoveCard(CardView cardView)
        {
            _cards.Remove(cardView);
            UpdatePoses();
        }

        public void UpdatePoses()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                var pos = GetCardPosByNumber(i);
                var rot = GetRotationByPos(pos);

                _cards[i].LocalMoveTo(pos);
                _cards[i].RotateTo(rot);
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
            
            float xNormalPos = (pos - centerElement) / MaxCardsInHand;
            float yNormalPos = -Mathf.Pow(xNormalPos, 2);
            
            return new Vector2(ArcWidth * xNormalPos, ArcHeight * yNormalPos);
        }

        private Quaternion GetRotationByPos(Vector2 pos)
        {
            float zRot = 0;
            if (!Mathf.Approximately(pos.x, 0))
            {
                zRot = Mathf.Atan(pos.y / pos.x) * Mathf.Rad2Deg;
            }

            return Quaternion.Euler(0, 0, zRot);
        }
    }
}