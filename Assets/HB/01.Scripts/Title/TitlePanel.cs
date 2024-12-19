using System;
using UnityEngine;

public class TitlePanel : MonoBehaviour
{
    public event Action<float> OnExitPanelEvent;
    [SerializeField] private int _count;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _count++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _count--;

            if (_count == 0)
                OnExitPanelEvent?.Invoke(1);
        }
    }
}
