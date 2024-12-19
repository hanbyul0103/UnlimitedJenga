using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [SerializeField] private Transform targetCam;
    [SerializeField] private Vector2 ratio = new Vector2(1, 9);
    [SerializeField] private float duration = 1f;
    
    private MeshRenderer render;
    private Material material;

    private void Awake() {
        render = GetComponent<MeshRenderer>();
        material = render.material;
    }

    private void Update() {
        Vector3 targetPos = targetCam.position;
        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);

        float leftX = Camera.main.ViewportToWorldPoint(new Vector3(Camera.main.rect.xMin, 0, 0)).x;
        float rightX = Camera.main.ViewportToWorldPoint(new Vector3(Camera.main.rect.xMax, 0, 0)).x;

        float width = Mathf.Abs(leftX - rightX);
        float height = width * ratio.y / ratio.x;

        transform.localScale = new Vector3(width, height, 1);
        material.SetTextureOffset("_BaseMap", new Vector2(0, targetPos.y * duration));
    }
}
