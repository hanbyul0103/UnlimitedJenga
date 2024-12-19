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
    private float animDuration = 1.5f;
    
    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        image = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start() {
        StartCoroutine(DestroyAfterTime());
    }
    
    public void SetDirection(Vector2 dir) {
        rigid.AddForce(dir * speed, ForceMode2D.Impulse);
    }

    IEnumerator DestroyAfterTime() {
        yield return new WaitForSeconds(lifeTime - animDuration);

        Tween tween = image.DOFade(0, animDuration).SetEase(Ease.Linear);
        yield return tween.WaitForCompletion();

        Destroy(gameObject);
    }
}
