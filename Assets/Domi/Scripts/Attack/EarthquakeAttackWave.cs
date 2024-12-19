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
    [SerializeField] private AudioClip effectSound;
    private AudioSource audioSource;


    Rigidbody2D[] shakeBlocks;

    private void Awake() {
        cam = FindAnyObjectByType<WaveAttackCam>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = effectSound;
    }
    
    public override void AttackStart(Vector2 rangeY)
    {
        GroundDetectBlock[] blocks = FindObjectsByType<GroundDetectBlock>(FindObjectsSortMode.None);

        shakeBlocks = blocks.Where(v => v.transform.position.y >= rangeY.x).Select(v => v.GetComponent<Rigidbody2D>()).ToArray();

        currentStrength = shakeStrength;
        StartCoroutine(EarthquakeEffect());
    }

    private void OnDestroy() {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
    
    IEnumerator EarthquakeEffect() {
        audioSource.time = 0;
        audioSource.volume = 1;
        audioSource.Play();

        yield return new WaitForSeconds(18f);

        DOTween.To(() => currentStrength, x => currentStrength = x, 0, 2f);
        audioSource.DOFade(0, 2f);
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
