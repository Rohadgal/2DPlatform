using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            LevelManager.s_instance.changeLevelState(LevelState.LevelFinished);
        }
    }
}
