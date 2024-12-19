using DG.Tweening;
using System;
using UnityEngine;

public class WhoMadeThisButton : TitleButton
{
    public event Action<float> OnExitEvent;

    [SerializeField] private Transform _originPos;
    [SerializeField] private Transform _targetPos;
    [SerializeField] private TitlePanel _target;

    private bool _isTweening = false;

    public override void Awake()
    {
        base.Awake();

        _target.OnExitPanelEvent += HandleOnExitEvent;
    }

    private void Start()
    {
        _target.gameObject.transform.position = _originPos.position;
    }

    protected override void HandleOnTimerEndEvent(float duration)
    {
        if (_isTweening) _target.gameObject.transform.DOKill();

        _isTweening = true;
        _target.gameObject.transform.DOMove(_targetPos.position, duration);
        _isTweening = false;
    }

    private void HandleOnExitEvent(float duration)
    {
        if (_isTweening) _target.gameObject.transform.DOKill();

        _isTweening = true;
        _target.gameObject.transform.DOMove(_originPos.position, duration);
        _isTweening = false;
    }

    private void OnDestroy()
    {
        _target.OnExitPanelEvent -= HandleOnExitEvent;
    }
}
