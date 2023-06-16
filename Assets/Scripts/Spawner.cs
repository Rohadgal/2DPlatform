using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float xStart, minY, maxY;
    [SerializeField] int objectPoolLimit;

    Queue<GameObject> pipePool;
    bool canInstantiate = true;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(Spawning());
        pipePool = new Queue<GameObject>();
    }

    IEnumerator Spawning() {
        while (GameManager.s_instance.getGameState() != GameState.GameOver) {
            yield return new WaitForSeconds(LevelManager.s_instance.getTime());
            if (GameManager.s_instance.getGameState() != GameState.Playing) {
                continue;
            }
            float rndY = Random.Range(minY, maxY);
            Vector2 startPos = new Vector2(xStart, rndY);
            if (canInstantiate) {
                pipePool.Enqueue(Instantiate(enemy, startPos, Quaternion.identity));
                if (pipePool.Count > objectPoolLimit) {
                    canInstantiate = false;
                }
            } else {
                GameObject tempGO = pipePool.Dequeue();
                tempGO.transform.position = startPos;
                tempGO.SetActive(true);
                pipePool.Enqueue(tempGO);
            }
        }
    }
}

