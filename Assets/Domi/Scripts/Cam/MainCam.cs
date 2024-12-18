using Unity.Cinemachine;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    [SerializeField] private Transform hitTarget;
    [SerializeField] private float padding = 0.2f;
    [SerializeField] private float upSize = 5f;
    [SerializeField] private float overHeight = 5f;
    [SerializeField] private float minHeight = 4.85f;
    [SerializeField] private float testHeight;

    GroundBox groundBox;
    CinemachineRotationComposer composer;

    private void Awake() {
        groundBox = FindAnyObjectByType<GroundBox>();
        composer = GetComponent<CinemachineRotationComposer>();
    }

    private void Update()
    {
        float blockMaxY = groundBox.GetMaxHeight();
        // float blockMaxY = testHeight;

        if (Vector2.Distance(hitTarget.position, transform.position) > 0.1f) { // 이거 아직 안따라옴
             float diff = Mathf.Abs(blockMaxY - transform.position.y);
            // Vector3 camCenter = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));

            // 차이가 너무 큼
            // print($"{diff} > {overHeight}");
            if (diff > overHeight) {
                Vector3 origin = hitTarget.position;
                hitTarget.position = new Vector3(origin.x, Mathf.Max(minHeight, blockMaxY), origin.z);
            }
             return;
        }
        
        float top = Camera.main.rect.yMax - padding;
        float bottom = Camera.main.rect.yMin + padding;
        Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(0, top, 0));
        Vector3 pointBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, bottom, 0));


        // 너무 높다
        if (point.y < blockMaxY) {
            // print($"High!!!! blockMaxY: {blockMaxY} point.y: {point.y}");
            hitTarget.position += Vector3.up * upSize;
        } else if (pointBottom.y > blockMaxY) {
            hitTarget.position -= Vector3.up * upSize;
        }

        if (hitTarget.position.y < minHeight) {
            hitTarget.position = new Vector3(hitTarget.position.x, minHeight, hitTarget.position.z);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(0, testHeight, 0), new Vector3(10, 0.1f, 1));

        Gizmos.color = Color.red;

        // Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(0, Camera.main.rect.size.y / 2f, 0));
        Vector3 point = Camera.main.ViewportToScreenPoint(new Vector3(0, Camera.main.rect.size.y / 2f, 0));
        float y = hitTarget.transform.position.y + point.y;
        Gizmos.DrawWireSphere(new Vector3(0, y, 0), 1f);
    }
}
