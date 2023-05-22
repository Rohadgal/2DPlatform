using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform[] spawnPoints;

    public float secondsToWait = 4;

   public ParticleSystem spawnParticles;
    private AudioSource audioSource;


    int previousPosition;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        spawnParticles = GetComponent<ParticleSystem>();
    }

    
    void Start()
    {
        if(spawnPoints !=null && enemyPrefab != null) StartCoroutine(SpawnEnemies());
        
    }

    IEnumerator SpawnEnemies()
    {
        int position = Random.Range(0, spawnPoints.Length);
            while (position == previousPosition){
                position = Random.Range(0, spawnPoints.Length);}
        yield return new WaitForSeconds(secondsToWait);
        if(audioSource != null) audioSource.Play();
        //if(spawnParticles != null) spawnParticles.Play();
        Instantiate(enemyPrefab, spawnPoints[position].transform.position, Quaternion.identity);
        Debug.Log("random num: " +Random.Range(0, spawnPoints.Length));
        previousPosition = position;
        StartCoroutine(SpawnEnemies());
    }
}
