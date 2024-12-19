using UnityEngine;

public class SchoolAttackWave : WaveAttackBase
{
    [SerializeField] private ShcoolLogoEntity[] prefabs;
    [SerializeField] private float xMargin = 5f;
    [SerializeField] private float yMin = 2f;
    [SerializeField] private Vector2 spawnDelayRange = new Vector2(1f, 5f);

    private Vector2 rangeX;
    private Vector2 rangeY;
    private float currentTime;

    private void Awake() {
        currentTime = GetRandomTime();
    }

    public override void AttackStart(Vector2 range)
    {
        rangeY = range;
    }

    private float GetRandomTime() {
        return Random.Range(spawnDelayRange.x, spawnDelayRange.y);
    }

    private ShcoolLogoEntity GetRandomPrefab() {
        return prefabs[Random.Range(0, prefabs.Length)];
    }

    private void Update() {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0) {
            currentTime = GetRandomTime();
            SpawnEntity();
        }
    }

    private void SpawnEntity() {
        ReloadSizeX();

        bool left = Random.Range(0, 2) == 0;
        float height = Random.Range(rangeY.x, rangeY.y);
        height = Mathf.Max(height, yMin);
        print($"{rangeY} / {height}");

        Vector3 pos = new Vector3(left ? rangeX.x : rangeX.y, height, 0);
        ShcoolLogoEntity logoEntity = Instantiate(GetRandomPrefab(), pos, Quaternion.identity);

        logoEntity.SetDirection(left ? Vector2.right : Vector2.left);
    }

    private void ReloadSizeX() {
        float leftX = Camera.main.ViewportToWorldPoint(new Vector3(Camera.main.rect.xMin, 0, 0)).x - xMargin;
        float rightX = Camera.main.ViewportToWorldPoint(new Vector3(Camera.main.rect.xMax, 0, 0)).x + xMargin;

        rangeX = new Vector2(leftX, rightX);
    }
}
