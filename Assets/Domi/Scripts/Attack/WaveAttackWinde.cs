using System.Collections;
using DG.Tweening;
using UnityEngine;

public class WaveAttackWinde : WaveAttackBase
{
    [SerializeField] private ParticleSystem windEffect;
    [SerializeField] private LayerMask blockLayer;
    [SerializeField] private float pushValue = 50f;
    [SerializeField] private AudioClip windSound;

    ParticleSystem particle;
    Vector2 rangeY;
    private Vector2 direction;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = windSound;
    }

    public override void AttackStart(Vector2 val)
    {
        rangeY = val;
        particle = Instantiate(windEffect, Vector3.zero, Quaternion.identity);
        StartCoroutine(WineEffect());
    }

    private void OnDestroy() {
        if (particle)
            Destroy(particle.gameObject);
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    IEnumerator WineEffect() {
        int loop = 3;
        float timer;
        
        while (loop-- > 0) {
            yield return new WaitForSeconds(3f); // 3초 wait..
            SetRandomDirectionAndPos();
            particle.Play();

            audioSource.time = 0;
            audioSource.volume = 1;
            audioSource.Play();

            timer = 7f; // 7초동안
            while (timer > 0) {
                timer -= Time.deltaTime;
                BlocksAddForce();
                yield return null;
            }

            audioSource.DOFade(0, 0.5f);

            particle.Stop();
            particle.Clear();
        }
    }

    private void SetRandomDirectionAndPos() {
        bool left = Random.Range(0, 2) == 0;
        Vector2 rangeX = GetScreenSideX(3f);
        Vector2 rangeCenter = new Vector2((rangeX.x + rangeX.y) / 2, (rangeY.x + rangeY.y) / 2);

        Vector2 pos = new Vector2(left ? rangeX.x : rangeX.y, Random.Range(rangeY.x, rangeY.y));
        direction = (rangeCenter - pos).normalized;

        particle.transform.position = pos;
        particle.transform.rotation = LookRotation2D(direction);
    }
    
    private void BlocksAddForce() {
        float size = Mathf.Abs(rangeY.y - rangeY.x);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(particle.transform.position, new Vector2(size, 10), particle.transform.eulerAngles.z, direction, 999f, blockLayer);
        print($"hits: {hits.Length}");
        
        foreach (var hit in hits) {
            Rigidbody2D rb = hit.collider.GetComponent<Rigidbody2D>();
            if (rb) {
                rb.AddForce(direction * pushValue);
            }
        }
    }

    
}
