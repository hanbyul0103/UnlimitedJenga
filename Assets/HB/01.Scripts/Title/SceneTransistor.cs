using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneTransistor : MonoBehaviour
{
    [SerializeField] private List<Transform> _blockPos;
    [SerializeField] private List<Transform> _originPos;
    [SerializeField] private List<Transform> _targetPos;

    private void Start()
    {
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < _blockPos.Count; i++)
        {
            _originPos[i].position = _blockPos[i].position;
        }

        for (int i = 0; i < _blockPos.Count; i++)
        {
            seq.Insert(Random.Range(0.1f, .7f), _blockPos[i].DOMove(_targetPos[i].position, 0.5f));
        }

        seq.Play();
    }

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            if (SceneManager.GetActiveScene().buildIndex != 0) return;

            for (int i = 0; i < _blockPos.Count; i++)
            {
                _blockPos[i].DOKill();
            }

            Sequence seq = DOTween.Sequence();

            for (int i = 0; i < _blockPos.Count; i++)
            {
                seq.Insert(Random.Range(0.1f, .7f), _blockPos[i].DOMove(_originPos[i].position, 0.5f));
            }

            seq.Play().OnComplete(() => SceneManager.LoadScene("TitleScene"));
        }
    }
}