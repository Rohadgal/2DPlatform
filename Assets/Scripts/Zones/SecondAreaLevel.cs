using UnityEngine;

public class SecondAreaLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            LevelManager.s_instance.setEnemySpawnArea(1);
            Debug.Log("Segunda area");
        }
    }
}
