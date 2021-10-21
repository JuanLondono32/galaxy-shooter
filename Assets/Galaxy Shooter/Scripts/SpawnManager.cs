using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;
    private GameManager _gameManager;


    // Use this for initialization
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(SpawnEnemy());
        StartCoroutine(PowerupSpawn());
    }

    public void StartSpawnRoutines()
    {
        StartCoroutine(PowerupSpawn());
        StartCoroutine(SpawnEnemy());
    }

    public IEnumerator SpawnEnemy()
    {       
        while (_gameManager.gameOver ==false)
        {
                Instantiate(enemyShipPrefab, new Vector3(Random.Range(-7.74f, 7.74f), 7f, 0), Quaternion.identity);
                yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator PowerupSpawn()
    {
        while (_gameManager.gameOver ==false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-7.74f, 7.74f), 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(10f);
        }
    }
}
