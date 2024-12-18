using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GoalLine : MonoBehaviour
{
    [SerializeField] private WaveSystemSO waveSys;
    [SerializeField] private TextMeshPro heightT;
    
    GroundBox groundBox;

    private void Awake() {
        groundBox = FindAnyObjectByType<GroundBox>();
        waveSys.OnChangeWaveCount += HandleChangeWaveCount;
    }

    private void OnDestroy() {
        waveSys.OnChangeWaveCount -= HandleChangeWaveCount;
    }

    private void HandleChangeWaveCount()
    {
        // transform.position = new Vector3(0, waveSys.AttackStartHeight, 0);
        transform.DOMoveY(waveSys.AttackStartHeight, 0.5f).SetEase(Ease.OutQuad);
        heightT.text = $"{waveSys.AttackStartHeight}m";
    }

    private void Update() {
        if (waveSys.IsAttack) return;

        float height = groundBox.GetMaxHeight();
        if (height > waveSys.AttackStartHeight) {
            waveSys.HandleLineUp();
        }
    }
}
