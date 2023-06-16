using UnityEngine;

public class FourthAreaComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            LevelManager.s_instance.setEnemySpawnArea(3);
            Debug.Log("last trigger");
        }
    }
}
