using UnityEngine;

public class MapOutDetect : MonoBehaviour
{
    [SerializeField] private StatisticsSO statistics;

    private void OnTriggerExit2D(Collider2D other) {
        if (other.TryGetComponent(out IBlockOutHandler blockOutHandler)) {
            blockOutHandler.OnMapOut();
            statistics.fallBlock++;
        }
    }
}
