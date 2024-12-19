using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class EarthquakeAttackWave : WaveAttackBase
{
    [SerializeField] float shakeStrength = 5;
    [SerializeField] private float shakeSpeed = 2f;
    private WaveAttackCam cam;
    private float currentStrength = 0.0f;

    Rigidbody2D[] shakeBlocks;

    private void Awake() {
        cam = FindAnyObjectByType<WaveAttackCam>();
    }
    
    public override void AttackStart(Vector2 rangeY)
    {
        GroundDetectBlock[] blocks = FindObjectsByType<GroundDetectBlock>(FindObjectsSortMode.None);

        shakeBlocks = blocks.Where(v => v.transform.position.y >= rangeY.x).Select(v => v.GetComponent<Rigidbody2D>()).ToArray();

        currentStrength = shakeStrength;
        StartCoroutine(EarthquakeEffect());
    }
    
    IEnumerator EarthquakeEffect() {
        yield return new WaitForSeconds(18f);
        DOTween.To(() => currentStrength, x => currentStrength = x, 0, 2f);
    }

    private void Update() {
        cam.SetNoise(currentStrength, currentStrength);
        float x = Mathf.Cos(Time.time * 360 * Mathf.Deg2Rad * shakeSpeed) * (currentStrength * 20);

        foreach (var block in shakeBlocks) {
            if (block != null)
                block.AddForce(Vector2.right * x);
        }
    }
}
