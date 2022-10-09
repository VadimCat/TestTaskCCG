using UnityEngine;

[CreateAssetMenu]
public class CardViewAnimationConfig : ScriptableObject
{
    [SerializeField] private float animationStepTime = .5f;
    [SerializeField] private float deathTime = 1f;
    [SerializeField] private float cardPaunchScale = 1.1f;
    [SerializeField] private float valueCountDownPaunchScale = 1.5f;
    [SerializeField] private Vector3 deathDelta = Vector3.up * 150;
    
    public float AnimationStepTime => animationStepTime;

    public float CardPaunchScale => cardPaunchScale;

    public float ValueCountDownPaunchScale => valueCountDownPaunchScale;

    public float DeathTime => deathTime;

    public Vector3 DeathDelta => deathDelta;
}