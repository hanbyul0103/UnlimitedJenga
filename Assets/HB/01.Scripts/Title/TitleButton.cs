using System;
using System.Collections;
using UnityEngine;

public abstract class TitleButton : MonoBehaviour
{
    private int _count;
    protected float _coolTime = 3;
    protected float _startTime;
    private Coroutine _currentCoroutine;

    public event Action<float> OnTimerEndEvent;

    public virtual void Awake()
    {
        OnTimerEndEvent += HandleOnTimerEndEvent;
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            if (_startTime + _coolTime <= Time.time)
            {
                OnTimerEndEvent?.Invoke(1);
                yield break;
            }

            yield return null;
        }
    }

    protected abstract void HandleOnTimerEndEvent(float duration);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _count++;

            if (_count == 2)
            {
                _startTime = Time.time;
                _currentCoroutine = StartCoroutine(Timer());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _count--;

            if (_count < 2 && _currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
        }
    }

    private void OnDestroy()
    {
        OnTimerEndEvent -= HandleOnTimerEndEvent;
    }
}