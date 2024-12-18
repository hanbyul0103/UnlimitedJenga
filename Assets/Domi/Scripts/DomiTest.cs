using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomiTest : MonoBehaviour
{
    // [SerializeField] private PlayerControlSO control;

    // private bool downRotateR = false;
    // private bool downRotateL = false;


    // private void OnEnable() {
    //     control.RotateREvent += RotateRight;
    //     control.RotateLEvent += RotateLeft;
    // }

    // private void OnDisable() {
    //     control.RotateREvent -= RotateRight;
    //     control.RotateLEvent -= RotateLeft;
    // }

    // private void RotateRight(bool val) {
    //     downRotateR = val;
    // }

    // private void RotateLeft(bool val) {
    //     downRotateL = val;
    // }

    // private void Update() {
    //     Vector2 dir = control.GetMoveDirection();
    //     print($"move dir: {dir} / rotate L: {downRotateL} / rotate R: {downRotateR}");
    // }

    [SerializeField] private WaveSystemSO waveSystem;
    
    private void Awake() {
        StartCoroutine(Testtesttes());
    }

    IEnumerator Testtesttes() {
        yield return new WaitForSeconds(1f);
        waveSystem.SetWave(0);
        waveSystem.SetWave(3);

        waveSystem.HandleLineUp();
    }
}
