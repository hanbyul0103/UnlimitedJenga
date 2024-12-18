using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopPanelUI : MonoBehaviour, IPopup
{
    [SerializeField] private BlockContainerSO _blockContainerSO;

    public List<Block> blocks = new List<Block>(4);
    public List<Transform> points = new List<Transform>(4);

    [SerializeField] private float _originPositionX;
    [SerializeField] private float _targetPositionX;

    private bool _isTweening;

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
            OpenPopup(1);
    }

    public void OpenPopup(float _duration)
    {
        if (_isTweening) return; // 이전에 있던 트윈 킬하고 열기

        _isTweening = true;

        SettingBlock();

        transform.DOMoveX(_targetPositionX, _duration)
            .OnComplete(() => _isTweening = false);
    }

    public void ClosePopup(float _duration)
    {
        if (_isTweening) return; // 이전에 있던 트윈 킬하고 닫기

        _isTweening = true;
        transform.DOMoveX(_originPositionX, _duration)
            .OnComplete(() => _isTweening = false);
    }

    private void SettingBlock()
    {
        for (int i = 0; i < 4; i++)
        {
            blocks.Add(_blockContainerSO.PickRandomBlock());
        }

        for (int i = 0; i < 4; i++)
        {
            Block block = Instantiate(blocks[i], points[i].position, Quaternion.identity);
            block.transform.SetParent(points[i], true);
        }
    }
}
