using System;
using Client;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{
    [SerializeField] private CanvasGroup rootCanvas;
    [SerializeField] private CardViewAnimationConfig _animationConfig;
    [SerializeField] private Image art;
    [SerializeField] private TMP_Text mana;
    [SerializeField] private TMP_Text health;
    [SerializeField] private TMP_Text attack;
    [SerializeField] private TMP_Text tittle;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Image backShine;

    public event Action<CardView> OnHoldStart;
    public event Action<CardView> OnHold;
    public event Action<CardView> OnHoldEnd;
    public event Action OnAnimationComplete;

    public void LocalMoveTo(Vector3 pos)
    {
        transform.DOLocalMove(pos, _animationConfig.AnimationStepTime);
    }

    public void RotateTo(Quaternion quaternion)
    {
        transform.DOLocalRotateQuaternion(quaternion, _animationConfig.AnimationStepTime);
    }

    public void SetViewData(CardViewData viewData)
    {
        art.sprite = viewData.Art;
        tittle.text = viewData.Title;
        description.text = viewData.Description;
    }

    public void SetMana(int value, int prevValue)
    {
        AnimateElementValue(mana, value, prevValue);
    }


    public void SetAttack(int value, int prevValue)
    {
        AnimateElementValue(attack, value, prevValue);
    }

    public void SetHealth(int value, int prevValue)
    {
        AnimateElementValue(health, value, prevValue);
    }

    private void AnimateElementValue(TMP_Text element, int value, int prevValue)
    {
        int val = 0;
        var seq = DOTween.Sequence();
        seq.Append(
            transform.DOScale(Vector3.one * _animationConfig.CardPaunchScale, _animationConfig.AnimationStepTime));

        seq.Join(element.transform.DOScale(Vector3.one * _animationConfig.ValueCountDownPaunchScale,
            _animationConfig.AnimationStepTime));

        seq.Join(DOTween.To(x => val = (int)x, prevValue, value, _animationConfig.AnimationStepTime)
            .OnUpdate(() => element.text = val.ToString()));

        seq.Append(element.transform.DOScale(Vector3.one, _animationConfig.AnimationStepTime));
        seq.Append(transform.DOScale(Vector3.one, _animationConfig.AnimationStepTime));

        seq.onComplete += InvokeAnimationComplete;
        seq.Play();
    }

    public void EnableDragStyle(bool isDrag)
    {
        var seq = DOTween.Sequence();
        seq.Join(backShine.DOColor(isDrag ? Color.white : Color.green, _animationConfig.AnimationStepTime))
            .Join(transform.DOScale(isDrag ? .75f : 1f, _animationConfig.AnimationStepTime))
            .Join(transform.DORotateQuaternion(quaternion.identity, _animationConfig.AnimationStepTime))
            .onComplete += InvokeAnimationComplete;

        seq.Play();
    }

    public void AnimateDeath()
    {
        rootCanvas.interactable = false;
        var seq = DOTween.Sequence();
        seq.Join(rootCanvas.DOFade(0, _animationConfig.DeathTime));
        seq.Join(transform.DOLocalMove(transform.localPosition + _animationConfig.DeathDelta,
            _animationConfig.DeathTime));
        seq.onComplete += InvokeAnimationComplete;
        seq.onComplete += () => { Destroy(gameObject); };
        seq.Play();
    }

    private void InvokeAnimationComplete()
    {
        OnAnimationComplete?.Invoke();
    }

    private void OnDestroy()
    {
        OnAnimationComplete = null;
        OnHoldStart = null;
        OnHold = null;
        OnHoldEnd = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnHoldStart?.Invoke(this);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        OnHold?.Invoke(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnHoldEnd?.Invoke(this);
    }
}