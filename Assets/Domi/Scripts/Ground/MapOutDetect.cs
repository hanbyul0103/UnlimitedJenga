using UnityEngine;

public class MapOutDetect : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other) {
        if (other.TryGetComponent(out IBlockOutHandler blockOutHandler)) {
            blockOutHandler.OnMapOut();
        }
    }
}
