using DG.Tweening;
using UnityEngine;

public class TrashPanelUI : MonoBehaviour, IPopup
{
    [SerializeField] private float _originPositionX;
    [SerializeField] private float _targetPositionX;

    private bool _isTweening;
    [SerializeField] private int _count;

    [SerializeField] private TrashCanUI _canUI;

    public void OpenPopup(float _duration)
    {
        if (_isTweening) transform.DOKill();

        SoundManager.Instance.PlaySFX("PanelOpen");

        _isTweening = true;

        transform.DOMoveX(_targetPositionX, _duration)
            .OnComplete(() =>
            {
                _isTweening = false;
                _canUI.canDestroy = true;
                Debug.Log("asdf");
            });
    }

    public void ClosePopup(float _duration)
    {
        if (_isTweening) transform.DOKill();

        SoundManager.Instance.PlaySFX("PanelClose");

        _isTweening = true;
        _canUI.canDestroy = false;

        transform.DOMoveX(_originPositionX, _duration)
            .OnComplete(() => _isTweening = false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _count++;

            if (_count >= 0)
                OpenPopup(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _count--;

            if (_count == 0)
                ClosePopup(1);
        }
    }
}
