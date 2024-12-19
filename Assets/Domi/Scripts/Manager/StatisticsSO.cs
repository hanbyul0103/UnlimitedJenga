using UnityEngine;

[CreateAssetMenu(fileName = "StatisticsSO", menuName = "SO/Statistics")]
public class StatisticsSO : ScriptableObject
{
    public float maxHeight = 0;
    public int fallBlock = 0;

    private void OnEnable() {
        maxHeight = 0;
        fallBlock = 0;
    }
}
