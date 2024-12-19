using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ShcoolLogoEntity : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer image;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private bool activeSound = false;
    [SerializeField] private AudioClip[] throwAudioClips;

    private AudioSource audioSource;
    private float animDuration = 1.5f;
    private float audioFadeoutDuration = 1f;
    
    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        image = GetComponentInChildren<SpriteRenderer>();

        if (activeSound)
            audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        StartCoroutine(DestroyAfterTime());
    }
    
    public void SetDirection(Vector2 dir) {
        rigid.AddForce(dir * speed, ForceMode2D.Impulse);
        if (activeSound)
            AudioPlay();
    }

    IEnumerator DestroyAfterTime() {
        
        yield return new WaitForSeconds(lifeTime - animDuration);

        Tween tween = image.DOFade(0, animDuration).SetEase(Ease.Linear);
        yield return tween.WaitForCompletion();

        Destroy(gameObject);
    }
    
    private void AudioPlay() {
        audioSource.clip = throwAudioClips[Random.Range(0, throwAudioClips.Length)];
        audioSource.Play();
    }

    Tween audioFade;
    private void Update() {
        if (activeSound && audioFade == null && audioSource.isPlaying && audioSource.time >= audioSource.clip.length - audioFadeoutDuration) {
            audioFade = audioSource.DOFade(0, audioFadeoutDuration);
        }
    }
}
