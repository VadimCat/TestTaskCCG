using UnityEngine;

public interface ICardViewFactory
{
    CardView cardPrefab { get; }
    public CardView Create(int cardId, Transform transform);
}