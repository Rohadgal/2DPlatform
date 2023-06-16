using UnityEngine;

public class ThirdAreaLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            LevelManager.s_instance.setEnemySpawnArea(2);
            Debug.Log("Third area");
        }
    }
}
