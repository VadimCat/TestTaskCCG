using UnityEngine;
using UnityEngine.UI;
using Views;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Button _abilityButton;
    [SerializeField] private InHandCardsView _inHandCardsView;
    [SerializeField] private BoardCardsView _boardCardsView;

    public InHandCardsView InHandCardsView => _inHandCardsView;

    public BoardCardsView BoardCardsView => _boardCardsView;

    public Button AbilityButton => _abilityButton;
}

public class BoardCardsView : MonoBehaviour
{
}