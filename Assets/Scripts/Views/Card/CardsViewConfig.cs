using UnityEngine;

[CreateAssetMenu]
public class CardsViewConfig : ScriptableObject
{
    [SerializeField] private CardView cardViewPrefab;

    public CardView CardViewPrefab => cardViewPrefab;
}