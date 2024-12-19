using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class TitleButton : MonoBehaviour
{
    private int _count;
    private float _coolTime = 3;
    private float _startTime;
    private Coroutine _currentCoroutine;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _count++;

            if (_count == 2)
            {
                _startTime = Time.time;
                _currentCoroutine = StartCoroutine(Timer("GameScene"));
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
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

    private IEnumerator Timer(string sceneName)
    {
        while (true)
        {
            if (_startTime + _coolTime <= Time.time)
            {
                SceneManager.LoadScene("HBScene");
                yield break;
            }

            yield return null;
        }
    }
}