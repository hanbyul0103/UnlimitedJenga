using UnityEngine;

public class GameStartAction : MonoBehaviour
{
    [SerializeField] private WaveSystemSO waveSys;

// 시작하믄
    private void Start() {
        SoundManager.Instance.PlaySFX("TitleToInGame");
        waveSys.GoGame(); // 1부터 시작
    }
}
