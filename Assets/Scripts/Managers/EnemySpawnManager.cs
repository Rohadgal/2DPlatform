using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform[] spawnPoints;

    //public ParticleSystem spawnParticles;

    [SerializeField] int objectPoolLimit;

    private AudioSource audioSource;
    //private float secondsToWait = 4;

    Queue<GameObject> pipePool;
    bool canInstantiate = true;

    int previousPosition;
    int spawnPosition;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        //spawnParticles = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        if (spawnPoints != null && enemyPrefab != null) {
            StartCoroutine(SpawnEnemies());
        }
        pipePool = new Queue<GameObject>();
    }

    IEnumerator SpawnEnemies()
    {
        //spawnPosition = Random.Range(0, spawnPoints.Length);
        while (GameManager.s_instance.getGameState() != GameState.GameOver) {

            switch(LevelManager.s_instance.getEnemySpawnArea()) { // Los casos se checan en pares porque el player tiene dos colliders.
                case 0:
                    while (spawnPosition == previousPosition) {
                        spawnPosition = Random.Range(0, 4);
                    }
                    break;
                case 1:
                    while (spawnPosition == previousPosition) {
                        spawnPosition = Random.Range(4, 8);
                    }
                    break;
                case 2:
                    while (spawnPosition == previousPosition) {
                        spawnPosition = Random.Range(8, 12);
                    }
                    break;
                case 3:
                    while (spawnPosition == previousPosition) {
                        spawnPosition = Random.Range(12, 16);
                    }
                    break;
                default:
                    throw new UnityException("Invalid Game State");
            }
            yield return new WaitForSeconds(LevelManager.s_instance.getSecondsToWait()-2);

            if (audioSource != null) {
                audioSource.Play();
            }

            if(canInstantiate) {
                pipePool.Enqueue(Instantiate(enemyPrefab, spawnPoints[spawnPosition].transform.position, Quaternion.identity));
                if (pipePool.Count > objectPoolLimit) {
                    canInstantiate = false;
                } 
            } else {
                GameObject tempGO = pipePool.Dequeue();
                tempGO.transform.position = spawnPoints[spawnPosition].transform.position;
                tempGO.SetActive(true);
                pipePool.Enqueue(tempGO);
            }
            previousPosition = spawnPosition;
            //Debug.Log("SpawnPos: " + LevelManager.s_instance.getEnemySpawnArea());
            //if (spawnParticles != null) {
            //    spawnParticles.Play();
            //}
        }
    }
}
