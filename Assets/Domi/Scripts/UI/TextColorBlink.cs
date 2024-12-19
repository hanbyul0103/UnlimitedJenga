using UnityEngine;

public class TextColorBlink : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    private Color originColor;
    private Color targetColor;
    private float duration = 0.5f;
    private float time = 0f;
    private bool isBlink = false;

    private void Awake() {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        originColor = text.color;
        targetColor = new Color(originColor.r, originColor.g, originColor.b, 0.5f);

        StartBlink();
    }

    public void StartBlink() {
        isBlink = true;
        time = 0f;
    }

    public void StopBlink() {
        isBlink = false;
        text.color = originColor;
    }

    private void Update() {
        if (!isBlink) return;

        time += Time.unscaledDeltaTime;
        float t = time / duration;
        text.color = Color.Lerp(originColor, targetColor, t);

        if (t >= 1f) {
            time = 0f;
            Color temp = originColor;
            originColor = targetColor;
            targetColor = temp;
        }
    }
}
