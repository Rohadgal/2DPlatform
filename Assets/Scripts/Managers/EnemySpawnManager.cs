using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour, ISpawnable
{
    public GameObject enemyPrefab;

    public Transform[] spawnPoints;

    public string enemyType = "Bat";

    //public ParticleSystem spawnParticles;

    //[SerializeField]
    int objectPoolLimit = 1;

    private AudioSource audioSource;
    //private float secondsToWait = 4;

    Queue<GameObject> enemyPool;
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
        enemyPool = new Queue<GameObject>();

        EventManager.MyEvent += changeObjectPoolLimit;
    }

    public GameObject spawnEnemy(Vector3 position) {
        GameObject enemyPrefab = EnemyFactory.createEnemy(enemyType);
        if (enemyPrefab != null) {
            return Instantiate(enemyPrefab, position, Quaternion.identity);
        } else {
            Debug.LogError("Failed to spawn enemy. Enemy prefab is null.");
            return null;
        }
    }

    IEnumerator SpawnEnemies()
    {
        
        //spawnPosition = Random.Range(0, spawnPoints.Length);
        while (GameManager.s_instance.getGameState() != GameState.GameOver) {
            if (GameManager.s_instance.getGameState() == GameState.GameFinished) {
                break;
            }
            switch (LevelManager.s_instance.getEnemySpawnArea()) { 
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
                enemyPool.Enqueue(spawnEnemy(spawnPoints[spawnPosition].transform.position));
               // enemyPool.Enqueue(Instantiate(enemyPrefab, spawnPoints[spawnPosition].transform.position, Quaternion.identity));
                if (enemyPool.Count >= objectPoolLimit) {
                    canInstantiate = false;
                } 
            } else {
                GameObject tempGO = enemyPool.Dequeue();
                tempGO.transform.position = spawnPoints[spawnPosition].transform.position;
                tempGO.SetActive(true);
                enemyPool.Enqueue(tempGO);
            }
            previousPosition = spawnPosition;
            //Debug.Log("SpawnPos: " + LevelManager.s_instance.getEnemySpawnArea());
            //if (spawnParticles != null) {
            //    spawnParticles.Play();
            //}
        }
    }

    private void OnDestroy() {
        // Asegúrate de desuscribir el método cuando el objeto se destruye para evitar fugas de memoria
        EventManager.MyEvent -= changeObjectPoolLimit;
    }

    void changeObjectPoolLimit() {
        if (objectPoolLimit < 4) {
            objectPoolLimit++;
            Debug.Log(objectPoolLimit);
            canInstantiate = true;
        }
    }
}
