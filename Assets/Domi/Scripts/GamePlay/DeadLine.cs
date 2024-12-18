using System;
using TMPro;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    [SerializeField] private WaveSystemSO waveSys;
    [SerializeField] private TextMeshPro heightT;
    
    GroundBox groundBox;

    private void Awake() {
        groundBox = FindAnyObjectByType<GroundBox>();
        waveSys.OnBeforeAttackStart += HandleAttackStart;
        waveSys.OnAttackFinish += HandleAttackFinish;
    }

    private void OnDestroy() {
        waveSys.OnBeforeAttackStart -= HandleAttackStart;
        waveSys.OnAttackFinish -= HandleAttackFinish;
    }

    private void HandleAttackStart(Vector2 vector)
    {
        transform.position = new Vector3(0, waveSys.DeadHeight, 0);
        heightT.text = $"{waveSys.DeadHeight}m";
        SetShow(true);
    }

    private void HandleAttackFinish()
    {
        SetShow(false);
    }

    private void Update() {
        if (!waveSys.IsAttack) return;

        float height = groundBox.GetMaxHeight();
        if (height < waveSys.DeadHeight) {
            waveSys.HandleDeadLineLow();
        }
    }

    private void SetShow(bool value) {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(value);
        }
    }
}
